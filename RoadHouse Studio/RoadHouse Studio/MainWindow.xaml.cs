using RoadHouse_Studio.Networking;
using RoadHouse_Studio.Pages;
using RoadHouse_Studio.Resources;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RoadHouse_Studio
{   

    public partial class MainWindow : Window
    {
        private readonly MainPage mainPage = new MainPage();
        private readonly AuctionPage auctionPage = new AuctionPage();
        private readonly SamplesPage samplesPage = new SamplesPage();

        public MainWindow()
        {
            InitializeComponent();

            NavigationFrame.Navigate(mainPage);

            InitViewerEvents();

            //TODO: Remove
            UriResponseBuilder r = new UriResponseBuilder(Strings.Twitch_OAuth_Protocol, Strings.Twitch_OAuth_Host, Strings.Twitch_OAuth_Path);
            Query q = new Query();
            Scopes s = new Scopes();
            s.Append("channel", "read", "redemptions");
            s.Append("channel", "manage", "redemptions");
            s.Append("channel", "read", "subscriptions", string.Empty);
            q.Init(new System.Collections.Generic.KeyValuePair<string, string>("response_type", "token"));
            q.Append('&', new System.Collections.Generic.KeyValuePair<string, string>("client_id", "<client_id>"));
            q.Append('&', new System.Collections.Generic.KeyValuePair<string, string>("redirect_uri", "http://localhost"));
            q.Append('&', new System.Collections.Generic.KeyValuePair<string, string>("scope", s.ToString()));

            r.BuildUri(q);
            Content c = new Content();
            c.Start();
            c.Append(new System.Collections.Generic.KeyValuePair<string, string>("title", "game analysis 1v1"));
            c.Append(new System.Collections.Generic.KeyValuePair<string, string>("cost", "50000"), string.Empty);
            c.End();

            Console.WriteLine(c.ToString());           

            //SocketSenderTest t = new SocketSenderTest();
            //t.TestConnection();
           
        }

        private void InitViewerEvents()
        {
            InitNavigationEvents();
            InitMenuEvents();
        }

        private void InitNavigationEvents()
        {
            InitNavigationButton(Nav_Main, mainPage);
            InitNavigationButton(Nav_Auction, auctionPage);
            InitNavigationButton(Nav_Samples, samplesPage);           
        }

        private void InitMenuEvents()
        {    
            InitNavigationMenuButton(Menu_Main, mainPage);
            InitNavigationMenuButton(Menu_Auction, auctionPage);
            InitNavigationMenuButton(Menu_Samples, samplesPage);

            Menu_ImportSamples.Click += ImportSamples;
        }

        private void ImportSamples(object sender, RoutedEventArgs args) => samplesPage.samples.Open();

        private void InitNavigationButton(Button button, Page page)
        => button.Click += (sender, EventArgs) => { Navigate(sender, EventArgs, page); };

        private void InitNavigationMenuButton(MenuItem item, Page page)
        => item.Click += (sender, EventArgs) => { Navigate(sender, EventArgs, page); };

        public void Navigate(object sender, RoutedEventArgs args, Page page) => NavigationFrame.Navigate(page);

        private void InitWindow(object sender, EventArgs args)
        {
            AppVersion.Content = Strings.App_Version;
            samplesPage.samples.Refresh();
        }
    }
}
