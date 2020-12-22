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
