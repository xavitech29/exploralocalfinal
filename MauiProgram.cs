using CommunityToolkit.Maui;
using exploralocalfinal.Servicios;
using Microsoft.Extensions.Logging;


namespace exploralocalfinal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

           // builder.Services.AddSingleton<IResenaRepository, ResenaService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.UseMauiCommunityToolkit();

            return builder.Build();
        }
    }
}
