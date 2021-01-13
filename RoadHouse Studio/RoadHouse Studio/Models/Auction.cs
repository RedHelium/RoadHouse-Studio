using System;

namespace RoadHouse_Studio.Models
{
    public sealed class Auction
    {

        public EventHandler OpenAuctionEvent;
        public EventHandler CloseAuctionEvent;

        public void OpenAuction() => OpenAuctionEvent?.Invoke(this, EventArgs.Empty);
        public void CloseAuction() => CloseAuctionEvent?.Invoke(this, EventArgs.Empty);
    }
}
