using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Boundaries.Services.Auction
{
    public class CreateAuctionRequest
    {
        public string Name { get; set; }
        public double InitialValue { get; set; }
        public bool IsItemUsed { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime ClosedAt { get; set; }
    }
}
