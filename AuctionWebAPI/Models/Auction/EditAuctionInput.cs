using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebAPI.Models.Auction
{
    public class EditAuctionInput
    {
        public int AuctionId { get; set; }
        public string Name { get; set; }
        public double InitialValue { get; set; }
        public bool IsItemUsed { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime ClosedAt { get; set; }
    }
}
