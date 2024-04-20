using CommunityToolkit.Maui;
using Locked.Pages;
using Locked.Repositories;
using Locked.ViewModels;
using Microsoft.Extensions.Logging;
using The49.Maui.BottomSheet;

namespace Locked;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseBottomSheet()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
                fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
                fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIconsWebfont");
            });

        builder.Services.AddSingleton<INoteRepository, JsonNoteRepository>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<EditContentPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}