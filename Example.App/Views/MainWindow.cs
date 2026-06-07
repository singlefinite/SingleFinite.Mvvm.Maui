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

using Example.Models;
using Fonts;
using SingleFinite.Essentials;
using SingleFinite.Mvvm;
using SingleFinite.Mvvm.Maui;
using SingleFinite.Mvvm.Maui.Services;

namespace Example.App.Views;

/// <summary>
/// The main window for the app.
/// </summary>
public partial class MainWindow : Window, IView<MainViewModel>
{
    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="viewModel">The view model for this view.</param>
    public MainWindow(MainViewModel viewModel, IResources resources)
    {
        ViewModel = viewModel;

        Page = new NavigationPage
        (
            root: new ContentPage
            {
                ToolbarItems =
                {
                    // Toolbar button used to open the settings dialog.
                    //
                    new ToolbarItem
                    {
                        IconImageSource = new FontImageSource
                        {
                            FontFamily = FluentUI.FontFamily,
                            Glyph = FluentUI.settings_24_regular
                        },
                        Apply = it =>
                        {
                            it.Clicked += (_, _) => ViewModel.ShowSettings();
                        }
                    }
                },
                Content = new Grid
                {
                    // Use a ViewPresenter to display the content.
                    //
                    new ViewPresenter
                    {
                        Source = ViewModel.Content
                    }
                }
            }
        ).DialogPage(
            // Attach a DialogViewPresenterPage to the navigation page so that
            // it will display over the entire window including the toolbar.
            //
            new DialogViewPresenterPage
            {
                DialogViewPresenter = new DialogViewPresenter
                {
                    Source = ViewModel.Dialogs
                }
            }
        );
    }

    #endregion

    #region Properties

    /// <summary>
    /// The view model for this view.
    /// </summary>
    public MainViewModel ViewModel { get; }

    #endregion
}
