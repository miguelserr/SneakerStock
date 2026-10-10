using Microsoft.Extensions.Logging;

namespace SneakerStock;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if WINDOWS
        // Quita la caja y la línea que Windows dibuja dentro de cada Entry
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("SinBorde", (handler, view) =>
        {
            var box = handler.PlatformView;
            var transparent = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.Transparent);

            box.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            box.Background = transparent;

            box.Resources["TextControlBackground"] = transparent;
            box.Resources["TextControlBackgroundPointerOver"] = transparent;
            box.Resources["TextControlBackgroundFocused"] = transparent;
            box.Resources["TextControlBorderBrush"] = transparent;
            box.Resources["TextControlBorderBrushPointerOver"] = transparent;
            box.Resources["TextControlBorderBrushFocused"] = transparent;
            box.Resources["TextControlBorderThemeThickness"] = new Microsoft.UI.Xaml.Thickness(0);
            box.Resources["TextControlBorderThemeThicknessFocused"] = new Microsoft.UI.Xaml.Thickness(0);
        });
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}