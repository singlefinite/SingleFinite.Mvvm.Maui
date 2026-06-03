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
/// Implementation of <see cref="IMauiApp"/>.
/// </summary>
/// <typeparam name="TMainViewModel">
/// The type of view model to build for the main window.
/// </typeparam>
/// <param name="viewBuilder">Used to build main view.</param>
/// <param name="appHost">The app host.</param>
/// <param name="serviceProvider">The service provider.</param>
internal partial class MauiApp<TMainViewModel>(
    IViewBuilder viewBuilder,
    AppHost appHost,
    IServiceProvider serviceProvider
) :
    IMauiApp<TMainViewModel>,
    IDisposable
    where TMainViewModel : IViewModel
{
    #region Fields

    /// <summary>
    /// Set to true when the app has been started.
    /// </summary>
    private bool _isStarted = false;

    /// <summary>
    /// Set to true when the app has been disposed.
    /// </summary>
    private bool _isDisposed;

    #endregion

    #region Properties

    /// <inheritdoc/>
    public IView<TMainViewModel> View
    {
        get
        {
            return field ?? throw new InvalidOperationException(
                "The app has not been started yet."
            );
        }
        private set;
    }

    /// <inheritdoc/>
    public Window Window => View as Window ??
        throw new InvalidOperationException(
            "The view for the main view model must be a Window."
        );

    #endregion

    #region Methods

    /// <inheritdoc/>
    public void Start()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        if (_isStarted)
            return;

        _isStarted = true;

        appHost.Start(serviceProvider);

        var assembleResult = viewBuilder.Assemble<TMainViewModel>();
        View = assembleResult.View;
        assembleResult.Start();
    }

    /// <inheritdoc/>
    public void Activate()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);
        Application.Current?.ActivateWindow(Window);
    }

    /// <summary>
    /// Mark this object as disposed.
    /// </summary>
    public void Dispose()
    {
        _isDisposed = true;
    }

    #endregion
}
