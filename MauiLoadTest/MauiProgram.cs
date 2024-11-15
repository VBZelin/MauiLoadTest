using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Telerik.Maui.Controls.Compatibility;

namespace MauiLoadTest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseTelerik()
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            Routing.RegisterRoute(nameof(WorsePage), typeof(WorsePage));
            Routing.RegisterRoute(nameof(WorsePageWithLazyView), typeof(WorsePageWithLazyView));
            Routing.RegisterRoute(nameof(BestPage), typeof(BestPage));
            Routing.RegisterRoute(nameof(EmptyPage), typeof(EmptyPage));

            return builder.Build();
        }
    }
}
