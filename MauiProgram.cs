using Microsoft.Maui.LifecycleEvents;

public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .ConfigureLifecycleEvents(events =>
        {
#if ANDROID
            events.AddAndroid(android => android.OnNewIntent((activity, intent) =>
            {
                var data = intent.Data?.ToString();
                if (!string.IsNullOrEmpty(data))
                {
                    DeepLinkHandler.HandleDeepLink(data);
                }
            }));
#elif IOS
            events.AddiOS(ios => ios.ContinueUserActivity((app, userActivity, completionHandler) =>
            {
                if (userActivity.WebPageUrl != null)
                {
                    DeepLinkHandler.HandleDeepLink(userActivity.WebPageUrl.ToString());
                }
                return true;
            }));
#endif
        });

    return builder.Build();
}
