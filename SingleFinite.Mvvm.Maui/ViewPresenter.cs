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

using SingleFinite.Essentials;
using SingleFinite.Mvvm.Services.Presenters;

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// Presents views provided by a presenter.  This class also supports
/// animated transitions between views.
/// </summary>
public partial class ViewPresenter : TemplatedView
{
    #region Fields

    /// <summary>
    /// Observer for view changes on the source presenter.
    /// </summary>
    private IDisposable? _sourceViewObserver;

    /// <summary>
    /// Used to animate transitions between views.
    /// </summary>
    private readonly AnimatedContent _animatedContentPresenter = new();

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    public ViewPresenter()
    {
        Loaded += (_, _) => SubscribeToPresenter();
        Unloaded += (_, _) => UnsubscribeFromPresenter();
    }

    #endregion

    #region Properties

    /// <summary>
    /// The presenter whose views will be displayed in this control.
    /// </summary>
    public IPresenter? Source
    {
        get;
        set
        {
            if (field == value)
                return;

            UnsubscribeFromPresenter();

            field = value;

            SubscribeToPresenter();
        }
    }

    /// <summary>
    /// The animation to run when a view enters the control going forward.
    /// </summary>
    public IViewAnimation EnterForwardAnimation { get; set; } =
        IViewAnimation.FadeIn();

    /// <summary>
    /// The animation to run when a view enters the control going backward.
    /// </summary>
    public IViewAnimation EnterBackwardAnimation { get; set; } =
        IViewAnimation.FadeIn();

    /// <summary>
    /// The animation to run when a view exits the control going forward.
    /// </summary>
    public IViewAnimation ExitForwardAnimation { get; set; } =
        IViewAnimation.FadeOut();

    /// <summary>
    /// The animation to run when a view exits the control going backward.
    /// </summary>
    public IViewAnimation ExitBackwardAnimation { get; set; } =
        IViewAnimation.FadeOut();

    #endregion

    #region Methods

    /// <summary>
    /// Get the content presenter from the control template.
    /// </summary>
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        var contentPresenter = GetTemplateChild("ContentPresenter")
            as ContentPresenter
            ?? throw new ArgumentException($"The template for {nameof(ViewPresenter)} must contain a {nameof(ContentPresenter)} named 'ContentPresenter'.");
        contentPresenter?.Content = _animatedContentPresenter;
    }

    /// <summary>
    /// Observe changes to the current view of the presenter.
    /// </summary>
    private void SubscribeToPresenter()
    {
        if (_sourceViewObserver is not null)
            return;

        _sourceViewObserver = Source?.CurrentChanged
            ?.Observe()
            ?.OnEach(
                args => UpdateSourceContent(
                    view: args.View,
                    isNew: args.IsNew
                )
            );

        UpdateSourceContent(
            view: Source?.Current,
            isNew: true
        );
    }

    /// <summary>
    /// Stop observing changes to the current view of the presenter.
    /// </summary>
    private void UnsubscribeFromPresenter()
    {
        _sourceViewObserver?.Dispose();
        _sourceViewObserver = null;
    }

    /// <summary>
    /// Update the view that is being displayed.
    /// </summary>
    /// <param name="view">The view to display.</param>
    /// <param name="isNew">Indicates if the view was newly added.</param>
    private void UpdateSourceContent(IView? view, bool isNew)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
            await _animatedContentPresenter.SetContentAsync(
                view: view as View,
                enterAnimation: isNew ? EnterForwardAnimation : EnterBackwardAnimation,
                exitAnimation: isNew ? ExitForwardAnimation : ExitBackwardAnimation
            )
        );
    }

    #endregion
}
