using AuctionWebAPI.Boundaries.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Boundaries.Services.Auction
{
    public class CreateAuctionResponse
    {
        public AuctionDTO Auction { get; set; }
    }
}
