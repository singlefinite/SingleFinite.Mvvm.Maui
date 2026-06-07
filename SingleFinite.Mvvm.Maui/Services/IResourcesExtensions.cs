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

using System.Diagnostics.CodeAnalysis;

namespace SingleFinite.Mvvm.Maui.Services;

/// <summary>
/// Extension members for the <see cref="IResources"/> service."/>
/// </summary>
public static class IResourcesExtensions
{
    extension(IResources resources)
    {
        /// <summary>
        /// Try to get the resource with the specified key and type.
        /// </summary>
        /// <typeparam name="TType">The resource type.</typeparam>
        /// <param name="key">The resource key.</param>
        /// <param name="value">
        /// The parameter that is assigned the value if it's found.
        /// </param>
        /// <returns>True if the resource is found; otherwise, false.</returns>
        public bool TryGetValue<TType>(string key, [MaybeNullWhen(false)] out TType value)
        {
            if (
                resources.TryGetValue(key, out var resourceValue) &&
                resourceValue is TType typedValue
            )
            {
                value = typedValue;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Try to get the resource with the specified key.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>The resource if it's found; otherwise, null.</returns>
        public object? TryGet(string key)
        {
            if (resources.TryGetValue(key, out var value))
                return value;

            return null;
        }

        /// <summary>
        /// Try to get the resource with the specified key and type.
        /// </summary>
        /// <typeparam name="TType">The resource type.</typeparam>
        /// <param name="key">The resource key.</param>
        /// <returns>
        /// The resource if it's found and is of the given type; otherwise, the
        /// default value of the type.
        /// </returns>
        public TType? TryGet<TType>(string key)
        {
            if (resources.TryGetValue<TType>(key, out var value))
                return value;

            return default;
        }

        /// <summary>
        /// Get the resource with the specified key and cast it to the specified
        /// type.
        /// </summary>
        /// <typeparam name="TType">
        /// The type to cast the resource to.
        /// </typeparam>
        /// <param name="key">The resource key.</param>
        /// <returns>The resource cast to the specified type.</returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the resource with the specified key was not found.
        /// </exception>
        public TType Get<TType>(string key)
        {
            if (resources.TryGetValue(key, out var value))
                return (TType)value;

            throw new KeyNotFoundException(
                $"The resource with key '{key}' was not found."
            );
        }

        /// <summary>
        /// Set the app theme for the bindable object property.
        /// </summary>
        /// <param name="key">
        /// This must be a key for an <see cref="AppThemeInfo"/> resource.
        /// </param>
        /// <param name="targetObject">The object to set the theme for.</param>
        /// <param name="targetProperty">
        /// The property to set the theme for.
        /// </param>
        public void SetAppTheme(
            string key,
            BindableObject targetObject,
            BindableProperty targetProperty
        )
        {
            var appThemeInfo = resources.Get<AppThemeInfo>(key);
            targetObject.SetAppTheme(
                targetProperty: targetProperty,
                light: appThemeInfo.Light,
                dark: appThemeInfo.Dark
            );
        }
    }
}
