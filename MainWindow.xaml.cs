using Microsoft.Web.WebView2.Core;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace MSP2TOOL.Desktop;

public partial class MainWindow : Window
{
    private const string GameUrl = "https://moviestarplanet2.com/";
    private CoreWebView2Environment? _environment;
    private CoreWebView2BrowserExtension? _extension;
    private bool _ready;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
        Browser.NavigationStarting += Browser_NavigationStarting;
        Browser.NavigationCompleted += Browser_NavigationCompleted;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            var userData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "MSP2TOOL", "WebView2");
            Directory.CreateDirectory(userData);

            var extensionPath = Path.Combine(AppContext.BaseDirectory, "Resources", "Extension");
            if (!File.Exists(Path.Combine(extensionPath, "manifest.json")))
                throw new FileNotFoundException("Clean extension manifest.json wurde nicht gefunden.", extensionPath);

            StartupStatus.Text = "WebView2-Profil wird initialisiert…";
            var options = new CoreWebView2EnvironmentOptions
            {
                AreBrowserExtensionsEnabled = true,
                EnableTrackingPrevention = true
            };

            _environment = await CoreWebView2Environment.CreateAsync(null, userData, options);
            await Browser.EnsureCoreWebView2Async(_environment);

            Browser.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true;
            Browser.CoreWebView2.Settings.AreDevToolsEnabled = true;
            Browser.CoreWebView2.Settings.IsStatusBarEnabled = false;
            Browser.CoreWebView2.Settings.IsPasswordAutosaveEnabled = false;
            Browser.CoreWebView2.Settings.IsGeneralAutofillEnabled = false;

            Browser.CoreWebView2.PermissionRequested += (_, args) =>
            {
                // Do not silently grant arbitrary permissions. MSP2's normal
                // web permissions remain controlled by WebView2 defaults.
                args.State = CoreWebView2PermissionState.Default;
            };

            StartupStatus.Text = "Clean MSP2TOOL-Extension wird installiert…";
            _extension = await Browser.CoreWebView2.Profile.AddBrowserExtensionAsync(extensionPath);

            StatusText.Text = $"Extension aktiv · {_extension.Name}";
            StartupStatus.Text = "MSP2 wird geöffnet…";
            Browser.Source = new Uri(GameUrl);
            _ready = true;
        }
        catch (Exception ex)
        {
            StartupStatus.Text = "Start fehlgeschlagen";
            StatusText.Text = ex.Message;
            MessageBox.Show(
                this,
                "MSP2TOOL konnte nicht gestartet werden.\n\n" + ex,
                "MSP2TOOL",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void Browser_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
    {
        StatusText.Text = "Lade: " + e.Uri;
    }

    private void Browser_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        if (e.IsSuccess)
        {
            AddressBox.Text = Browser.Source?.ToString() ?? GameUrl;
            StatusText.Text = "Bereit · " + (Browser.CoreWebView2.DocumentTitle ?? "MSP2");
            StartupOverlay.Visibility = Visibility.Collapsed;
        }
        else
        {
            StatusText.Text = $"Navigation fehlgeschlagen: {e.WebErrorStatus}";
        }
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        if (_ready && Browser.CanGoBack) Browser.GoBack();
    }

    private void Forward_Click(object sender, RoutedEventArgs e)
    {
        if (_ready && Browser.CanGoForward) Browser.GoForward();
    }

    private void Refresh_Click(object sender, RoutedEventArgs e)
    {
        if (_ready) Browser.Reload();
    }

    private void OpenGame_Click(object sender, RoutedEventArgs e)
    {
        if (!_ready) return;
        Browser.Source = new Uri(GameUrl);
    }

    private void Go_Click(object sender, RoutedEventArgs e) => NavigateAddress();

    private void AddressBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter) NavigateAddress();
    }

    private void NavigateAddress()
    {
        if (!_ready) return;
        var raw = AddressBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(raw)) return;
        if (!Uri.TryCreate(raw, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttps && uri.Scheme != Uri.UriSchemeHttp))
        {
            raw = "https://" + raw;
            if (!Uri.TryCreate(raw, UriKind.Absolute, out uri)) return;
        }
        Browser.Source = uri;
    }

    private async void ClearData_Click(object sender, RoutedEventArgs e)
    {
        if (!_ready) return;
        var result = MessageBox.Show(
            this,
            "Alle WebView2-Cookies, Cache und Website-Daten dieses MSP2TOOL-Profils löschen?\n\nDu wirst danach erneut eingeloggt werden.",
            "Daten löschen",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);
        if (result != MessageBoxResult.Yes) return;

        try
        {
            await Browser.CoreWebView2.Profile.ClearBrowsingDataAsync();
            StatusText.Text = "WebView2-Daten gelöscht. MSP2 wird neu geladen.";
            Browser.Reload();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Daten löschen", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        try { Browser.Dispose(); } catch { }
        base.OnClosed(e);
    }
}
