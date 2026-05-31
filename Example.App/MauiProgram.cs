using CommunityToolkit.Maui.Markup;
using Example.Models;
using Microsoft.Extensions.Logging;
using SingleFinite.Mvvm;
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
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("FluentSystemIcons-Regular.ttf", "FluentUI");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var appHost = new AppHostBuilder()
            .AddMaui<IMainViewModel, MainViewModel>()
            .AddExampleApp()
            .Build(builder.Services);

        var mauiApp = builder.Build();

        appHost.Start(mauiApp.Services);

        return mauiApp;
    }

    #endregion
}
