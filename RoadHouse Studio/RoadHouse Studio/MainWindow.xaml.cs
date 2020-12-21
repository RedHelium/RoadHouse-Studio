using RoadHouse_Studio.Pages;
using System.Windows;
using System.Windows.Controls;

namespace RoadHouse_Studio
{   

    public partial class MainWindow : Window
    {
        private readonly Main mainPage = new Main();
        private readonly Auction auctionPage = new Auction();
        private readonly Samples samplesPage = new Samples();

        public MainWindow()
        {
            InitializeComponent();

            NavigationFrame.Navigate(mainPage);

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
        }

        private void InitNavigationButton(Button button, Page page)
        => button.Click += (sender, EventArgs) => { Navigate(sender, EventArgs, page); };

        private void InitNavigationMenuButton(MenuItem item, Page page)
        => item.Click += (sender, EventArgs) => { Navigate(sender, EventArgs, page); };

        public void Navigate(object sender, RoutedEventArgs args, Page page) => NavigationFrame.Navigate(page);
        
    }
}
