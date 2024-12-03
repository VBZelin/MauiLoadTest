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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            Routing.RegisterRoute(nameof(WorsePage), typeof(WorsePage));
            Routing.RegisterRoute(nameof(BestPage), typeof(BestPage));
            Routing.RegisterRoute(nameof(EmptyPage), typeof(EmptyPage));
            Routing.RegisterRoute(nameof(TabViewWithCollectionView), typeof(TabViewWithCollectionView));

            return builder.Build();
        }
    }
}
