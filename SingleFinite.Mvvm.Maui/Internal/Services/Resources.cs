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

namespace SingleFinite.Mvvm.Maui.Internal.Services;

/// <summary>
/// Implements <see cref="IResources"/>.
/// </summary>
/// <param name="application">The application.</param>
/// <exception cref="ArgumentException">
/// Thrown if the application parameter is not of type Application.
/// </exception>
internal class Resources(IApplication application) : IResources
{
    #region Fields

    /// <summary>
    /// Holds the application.
    /// </summary>
    private readonly Application _application = application as Application ??
        throw new ArgumentException(
            message: "Must be of type Application.",
            paramName: nameof(application)
        );

    #endregion

    #region Methods

    /// <inheritdoc/>
    public bool TryGetValue(string key, out object value) =>
        _application.Resources.TryGetValue(key, out value);

    #endregion
}
