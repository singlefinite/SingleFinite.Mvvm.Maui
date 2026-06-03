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
/// Extensions for the <see cref="MauiAppBuilder"/> class."/>
/// </summary>
public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Adds SingleFinite.Mvvm support to the Maui app.
    /// </summary>
    /// <typeparam name="TMainViewModelInterface">
    /// The interface the main view model will be registered as.
    /// </typeparam>
    /// <typeparam name="TMainViewModelImplementation">
    /// The main view model type.
    /// </typeparam>
    /// <param name="mauiAppBuilder">
    /// The Maui app builder being extended.
    /// </param>
    /// <param name="configure">
    /// Optional action that can be used to confgure SingleFinite Mvvm.
    /// </param>
    /// <returns>The Maui app builder that was extended.</returns>
    public static MauiAppBuilder UseSingleFiniteMvvm<TMainViewModelInterface, TMainViewModelImplementation>(
        this MauiAppBuilder mauiAppBuilder,
        Action<AppHostBuilder>? configure = default)
        where TMainViewModelInterface : class
        where TMainViewModelImplementation : class, TMainViewModelInterface, IViewModel
    {
        var appHostBuilder = new AppHostBuilder();
        appHostBuilder.AddMaui<TMainViewModelInterface, TMainViewModelImplementation>();

        configure?.Invoke(appHostBuilder);

        appHostBuilder.Build(mauiAppBuilder.Services);

        return mauiAppBuilder;
    }

    /// <summary>
    /// Adds SingleFinite.Mvvm support to the Maui app.
    /// </summary>
    /// <typeparam name="TMainViewModel">
    /// The main view model type.
    /// </typeparam>
    /// <param name="mauiAppBuilder">
    /// The Maui app builder being extended.
    /// </param>
    /// <param name="configure">
    /// Optional action that can be used to confgure SingleFinite Mvvm.
    /// </param>
    /// <returns>The Maui app builder that was extended.</returns>
    public static MauiAppBuilder UseSingleFiniteMvvm<TMainViewModel>(
        this MauiAppBuilder mauiAppBuilder,
        Action<AppHostBuilder>? configure = default)
        where TMainViewModel : IViewModel
    {
        var appHostBuilder = new AppHostBuilder();
        appHostBuilder.AddMaui<TMainViewModel>();

        configure?.Invoke(appHostBuilder);

        appHostBuilder.Build(mauiAppBuilder.Services);

        return mauiAppBuilder;
    }
}
