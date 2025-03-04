using Microsoft.AspNetCore.Components.WebView.Maui;

public static class DeepLinkHandler
{
    public static void HandleDeepLink(string url)
    {
        var uri = new Uri(url);
        var page = uri.Host;  // Extract page name from deep link
        var query = uri.Query; // Extract query string

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var nav = App.Current.MainPage.Handler.MauiContext.Services.GetService<BlazorWebView>();
            if (nav != null)
            {
                nav.UrlLoading -= Nav_UrlLoading;
                nav.UrlLoading += Nav_UrlLoading;
                nav.Source = $"https://localhost/{page}{query}"; // Navigate to Blazor page whihc you have
            }
        });
    }

    private static void Nav_UrlLoading(object sender, UrlLoadingEventArgs e)
    {
        e.Handled = true;
    }
}
