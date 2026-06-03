using CommunityToolkit.Maui.Markup;
using Example.Models;
using Microsoft.Extensions.Logging;
using SingleFinite.Mvvm.Maui;

namespace Example.App;

/// <summary>
/// The entry point for the application.
/// </summary>
public static class MauiProgram
{
    #region Methods

    /// <summary>
    /// Create the Maui app.
    /// </summary>
    /// <returns>The created Maui app.</returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkitMarkup()
            .UseSingleFiniteMvvm<IMainViewModel, MainViewModel>(appHostBuilder =>
            {
                appHostBuilder.AddExampleApp();
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FluentSystemIcons-Regular.ttf", "FluentUI");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    #endregion
}
