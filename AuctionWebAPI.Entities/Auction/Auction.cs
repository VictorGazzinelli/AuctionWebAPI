using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Entities.Auction
{
    public class Auction
    {
        public int AuctionId { get; set; }
        public string Name { get; set; }
        public double InitialValue { get; set; }
        public bool IsItemUsed { get; set; }
        public int ResponsibleId { get; set; }
        public string Owner { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime ClosedAt { get; set; }
    }
}
