// MIT License
// Copyright (c) 2026 Single Finite
//
// Permission is hereby granted, free of charge, to any person obtaining a copy 
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights 
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
// copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in 
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// A page used to display dialog content.
/// </summary>
public partial class DialogViewPresenterPage : ContentPage
{
    #region Fields

    /// <summary>
    /// Set to true when this control is subscribed to the NavigationPage.
    /// </summary>
    private bool _isSubscribedToNavigationPage = false;

    /// <summary>
    /// Set to true when this control is subscribed to the DialogViewPresenter.
    /// </summary>
    private bool _isSubscribedToDialogViewPresenter = false;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    public DialogViewPresenterPage()
    {
        BackgroundColor = Colors.Transparent;
    }

    #endregion

    #region Properties

    /// <summary>
    /// The NavigationPage this page will be displayed on.
    /// </summary>
    internal NavigationPage? NavigationPage
    {
        get;
        set
        {
            if (value == field)
                return;

            field = value;

            if (value == null)
            {
                UnsubscribeFromNavigationPage();
                UnsubscribeFromDialogViewPresenter();
            }
            else
            {
                SubscribeToNavigationPage();
                SubscribeToDialogViewPresenter();
            }
        }
    }

    /// <summary>
    /// The dialog view presenter that will be displayed on this page.
    /// </summary>
    public DialogViewPresenter? DialogViewPresenter
    {
        get;
        set
        {
            if (value == field)
                return;

            UnsubscribeFromDialogViewPresenter();

            field = value;
            Content = value;

            // Make the background color for the scrim transparent as the page
            // will handle the scrim.
            //
            value?.ScrimBackgroundColor = Colors.Transparent;

            SubscribeToDialogViewPresenter();

            OnIsDialogOpenChanged(
                sender: this,
                isDialogOpen: value?.IsDialogOpen ?? false
            );
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Subscribe to the NavigationPage's Loaded and Unloaded events.
    /// </summary>
    private void SubscribeToNavigationPage()
    {
        if (_isSubscribedToNavigationPage)
            return;

        if (NavigationPage is NavigationPage navigationPage)
        {
            navigationPage.Loaded += OnNavigationPageLoaded;
            navigationPage.Unloaded += OnNavigationPageUnloaded;
            _isSubscribedToNavigationPage = true;
        }
    }

    /// <summary>
    /// Unsubscribe from the NavigationPage's Loaded and Unloaded events.
    /// </summary>
    private void UnsubscribeFromNavigationPage()
    {
        if (!_isSubscribedToNavigationPage)
            return;

        if (NavigationPage is NavigationPage navigationPage)
        {
            navigationPage.Loaded -= OnNavigationPageLoaded;
            navigationPage.Unloaded -= OnNavigationPageUnloaded;
        }

        _isSubscribedToNavigationPage = false;
    }

    /// <summary>
    /// Subscribe to the DialogViewPresenter's IsDialogOpenChanged event.
    /// </summary>
    private void SubscribeToDialogViewPresenter()
    {
        if (_isSubscribedToDialogViewPresenter)
            return;

        if (DialogViewPresenter is DialogViewPresenter presenter)
        {
            presenter.IsSubscribeManaged = true;
            presenter.IsDialogOpenChanged += OnIsDialogOpenChanged;
            _isSubscribedToDialogViewPresenter = true;
        }
    }

    /// <summary>
    /// Unsubscribe to the DialogViewPresenter's IsDialogOpenChanged event.
    /// </summary>
    private void UnsubscribeFromDialogViewPresenter()
    {
        if (!_isSubscribedToDialogViewPresenter)
            return;

        if (DialogViewPresenter is DialogViewPresenter presenter)
        {
            presenter.IsSubscribeManaged = false;
            presenter.IsDialogOpenChanged -= OnIsDialogOpenChanged;
        }

        _isSubscribedToDialogViewPresenter = false;
    }

    /// <summary>
    /// Subscribe to the DialogViewPresenter when the NavigationPage is loaded.
    /// </summary>
    /// <param name="sender">Not used.</param>
    /// <param name="e">Not used.</param>
    private void OnNavigationPageLoaded(object? sender, EventArgs e) =>
        SubscribeToDialogViewPresenter();

    /// <summary>
    /// Unsubscribe from the DialogViewPresenter when the NavigationPage is
    /// unloaded.
    /// </summary>
    /// <param name="sender">Not used.</param>
    /// <param name="e">Not used.</param>
    private void OnNavigationPageUnloaded(object? sender, EventArgs e) =>
        UnsubscribeFromDialogViewPresenter();

    /// <summary>
    /// Push or Pop this page on the NavigationPage based on if a dialog is open
    /// or not in the DialogViewPresenter.
    /// </summary>
    /// <param name="sender">Not used.</param>
    /// <param name="isDialogOpen">
    /// Indicates if a dialog is open in the DialogViewPresenter.
    /// </param>
    private void OnIsDialogOpenChanged(object? sender, bool isDialogOpen)
    {
        if (NavigationPage is not NavigationPage navigationPage)
            return;

        var index = navigationPage.Navigation.ModalStack.Count - 1;
        var currentDialog = index >= 0
            ? navigationPage.Navigation.ModalStack[index]
            : null;

        if (isDialogOpen && currentDialog != this)
        {
            navigationPage.Navigation.PushModalAsync(
                page: this,
                animated: false
            );
        }
        else if (!isDialogOpen && currentDialog == this)
        {
            navigationPage.Navigation.PopModalAsync(
                animated: false
            );
        }
    }

    #endregion
}
