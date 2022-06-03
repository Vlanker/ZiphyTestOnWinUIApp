using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.Web.WebView2.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZiphyTestOnWinUIApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            
            WebView.NavigationStarting += EnsureHttps;
        }

        private void EnsureHttps(WebView2 sender, CoreWebView2NavigationStartingEventArgs args)
        {
            var uri = args.Uri;
            
            if (!uri.StartsWith("https://"))
            {
                args.Cancel = true;
            }
            else
            {
                AddressBar.Text = uri;
            }
        }

        private void OnKeyUpEnter(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                GoToButton_Click(this, new RoutedEventArgs());
            }
        }

        private void GoToButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var targetUri = new Uri(AddressBar.Text);
                WebView.Source = targetUri;
            }
            catch (FormatException ex)
            {
                // Incorrect address entered.
            }
        }
    }
}