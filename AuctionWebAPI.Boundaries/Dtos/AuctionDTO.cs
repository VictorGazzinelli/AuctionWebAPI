using System;
using System.Collections.Generic;
using System.Text;
using AuctionEntity = AuctionWebAPI.Entities.Auction.Auction;

namespace AuctionWebAPI.Boundaries.Dtos
{
    public class AuctionDTO
    {
        public int AuctionId { get; set; }
        public string Name { get; set; }
        public double InitialValue { get; set; }
        public bool IsItemUsed { get; set; }
        public int ResponsibleId { get; set; }
        public string Owner { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime ClosedAt { get; set; }

        public AuctionDTO(AuctionEntity auction)
        {
            if (auction == null)
                return;
            this.AuctionId = auction.AuctionId;
            this.Name = auction.Name;
            this.InitialValue = auction.InitialValue;
            this.IsItemUsed = auction.IsItemUsed;
            this.ResponsibleId = auction.ResponsibleId;
            this.Owner = auction.Owner;
            this.OpenedAt = auction.OpenedAt;
            this.ClosedAt = auction.ClosedAt;
        }
    }
}
