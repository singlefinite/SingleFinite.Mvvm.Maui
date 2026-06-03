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

using SingleFinite.Mvvm.Maui.Internal.Services;
using SingleFinite.Mvvm.Maui.Services;
using SingleFinite.Mvvm.Services;

namespace SingleFinite.Mvvm.Maui;

/// <summary>
/// Extensions for the <see cref="AppHostBuilder"/> class.
/// </summary>
public static class AppHostBuilderExtensions
{
    #region Methods

    /// <summary>
    /// Add Maui services to the app.
    /// </summary>
    /// <typeparam name="TMainViewModelInterface">
    /// The interface that will be used to register the view model with for
    /// dependency injection.
    /// </typeparam>
    /// <typeparam name="TMainViewModelImplementation">
    /// The type of view model that will be built as the entry point for the
    /// app.  The view registered for the view model must be of type 
    /// <see cref="Window"/>.
    /// </typeparam>
    /// <param name="builder">The builder to extend.</param>
    /// <returns>The builder that was passed in.</returns>
    public static AppHostBuilder AddMaui<TMainViewModelInterface, TMainViewModelImplementation>(
        this AppHostBuilder builder
    )
        where TMainViewModelInterface : class
        where TMainViewModelImplementation : class, TMainViewModelInterface, IViewModel => builder
            .AddServices(
                services =>
                {
                    services.AddSingleton<TMainViewModelInterface>(
                        serviceProvider => serviceProvider.GetRequiredService<IMauiApp<TMainViewModelImplementation>>().View.ViewModel
                    );
                }
            )
            .AddMaui<TMainViewModelImplementation>();

    /// <summary>
    /// Add Maui services to the app.
    /// </summary>
    /// <typeparam name="TMainViewModel">
    /// The type of view model that will be built as the entry point for the
    /// app.  The view registered for the view model must be of type 
    /// <see cref="Window"/>.
    /// </typeparam>
    /// <param name="builder">The builder to extend.</param>
    /// <returns>The builder that was passed in.</returns>
    public static AppHostBuilder AddMaui<TMainViewModel>(
        this AppHostBuilder builder
    )
        where TMainViewModel : IViewModel => builder
            .AddServices(
                services =>
                {
                    services
                        .AddSingleton<IMainDispatcher, DispatcherMain>()
                        .AddSingleton<IMauiApp<TMainViewModel>, MauiApp<TMainViewModel>>()
                        .AddSingleton<IMauiApp>(serviceProvider =>
                            serviceProvider.GetRequiredService<IMauiApp<TMainViewModel>>()
                        );
                }
            );

    #endregion
}
