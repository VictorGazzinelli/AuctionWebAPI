﻿using AuctionWebAPI.Boundaries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebAPI.Models.Auction
{
    public class GetAuctionOutput
    {
        public AuctionDTO Auction { get; set; }
    }
}
