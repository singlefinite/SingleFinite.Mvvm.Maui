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

using SingleFinite.Mvvm.Maui.Services;
using SingleFinite.Mvvm.Services;

namespace SingleFinite.Mvvm.Maui.Internal.Services;

/// <summary>
/// Custom AppHost for Maui.
/// </summary>
/// <typeparam name="TMainViewModel">
/// The type of view model to build for the main window.
/// </typeparam>
/// <param name="initializers">Initializers for the app host.</param>
internal partial class MauiAppHost<TMainViewModel>(
    IInitializerCollection initializers
) : AppHost(initializers), IMauiAppHost<TMainViewModel>
    where TMainViewModel : IViewModel
{
    #region Fields

    /// <summary>
    /// Holds the View.
    /// </summary>
    private IView<TMainViewModel>? _view = null;

    #endregion

    #region Properties

    /// <summary>
    /// If set, this service provider will be used to start the app host if the
    /// View is accessed before the app host has been started.
    /// </summary>
    public IServiceProvider? ServiceProvider { get; set; } = null;

    /// <inheritdoc/>
    IView IMauiAppHost.View => View;

    /// <inheritdoc/>
    public IView<TMainViewModel> View
    {
        get
        {
            if (_view is null && ServiceProvider is not null)
                Start(ServiceProvider);

            if (_view is null)
                throw new InvalidOperationException(
                    "The app host has not been started."
                );

            return _view;
        }
        private set
        {
            _view = value;
        }
    }

    /// <inheritdoc/>
    public Window Window => View as Window ??
        throw new InvalidOperationException(
            "The view for the main view model must be a Window."
        );

    #endregion

    #region Methods

    /// <inheritdoc/>
    public override void Start(IServiceProvider provider)
    {
        if (_view is not null)
            return;

        var viewBuilder = provider.GetRequiredService<IViewBuilder>();
        var assembleResult = viewBuilder.Assemble<TMainViewModel>();
        View = assembleResult.View;
        assembleResult.Start();

        base.Start(provider);
    }

    #endregion
}
