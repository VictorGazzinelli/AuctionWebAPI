using AuctionWebAPI.Boundaries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebAPI.Models.Auction
{
    public class ListAuctionResponse
    {
        public AuctionDTO[] ArrAuction { get; set; }
    }
}
