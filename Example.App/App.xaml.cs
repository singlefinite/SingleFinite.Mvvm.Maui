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

namespace Example.App;

/// <summary>
/// The application class which holds the app host.
/// </summary>
public partial class App : Application
{
    #region Fields

    /// <summary>
    /// Holds the app.
    /// </summary>
    private readonly IMauiApp _app;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="app">The app.</param>
    public App(IMauiApp app)
    {
        _app = app;
        InitializeComponent();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Create the app host and its window.
    /// </summary>
    /// <param name="activationState">Not used.</param>
    /// <returns>The window for the app host.</returns>
    protected override Window CreateWindow(IActivationState? activationState)
    {
        _app.Start();
        return _app.Window;
    }

    #endregion
}
