using RoadHouse_Studio.Models;
using RoadHouse_Studio.Resources;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RoadHouse_Studio.Pages
{
    
    public partial class AuctionPage : Page
    {
        private Auction auction;

        public AuctionPage()
        {
            InitializeComponent();

            Init();
            InitEvents();
            InitViewerEvents();
        }

        private void Init()
        {
            auction = new Auction();
        }

        private void InitEvents()
        {
            auction.OpenAuctionEvent += ActiveSwitchAuctionLabel;
            auction.CloseAuctionEvent += InactiveSwitchAuctionLabel;
        }

        private void InitViewerEvents()
        {
            SwitchAuctionState.Click += SwitchAuction;
        }

        private void ActiveSwitchAuctionLabel(object sender, EventArgs args) => ChangeAuctionStateLabel(true, SwitchAuctionStateLabel);
        private void InactiveSwitchAuctionLabel(object sender, EventArgs args) => ChangeAuctionStateLabel(false, SwitchAuctionStateLabel);

        private void ChangeAuctionStateLabel(bool state, Label label)
        {
            if (state) label.Content = Strings.On_State;
            else label.Content = Strings.Off_State;
        }
        private void SwitchAuction(object sender, RoutedEventArgs args)
        {
            bool? isChecked = (sender as ToggleButton).IsChecked;

            if (isChecked ?? false) auction.OpenAuction();
            else
                auction.CloseAuction();

        }
    }
}
