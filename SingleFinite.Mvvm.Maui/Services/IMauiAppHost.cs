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

namespace SingleFinite.Mvvm.Maui.Services;

/// <summary>
/// The root level application host.
/// </summary>
public interface IMauiAppHost
{
    /// <summary>
    /// The main view for the app.
    /// </summary>
    IView View { get; }

    /// <summary>
    /// The main window for the app.
    /// </summary>
    Window Window { get; }

    /// <summary>
    /// Start the app host.
    /// </summary>
    /// <param name="serviceProvider">
    /// The service provider to use with the app host.
    /// </param>
    void Start(IServiceProvider serviceProvider);
}

/// <summary>
/// The root level application host.
/// </summary>
/// <typeparam name="TViewModel">The main view model type.</typeparam>
public interface IMauiAppHost<TViewModel> : IMauiAppHost
    where TViewModel : IViewModel
{
    /// <summary>
    /// The main view for the app.
    /// </summary>
    new IView<TViewModel> View { get; }
}
