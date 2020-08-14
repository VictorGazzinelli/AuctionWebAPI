using System;
using System.Collections.Generic;
using System.Text;
using AuctionEntity = AuctionWebAPI.Entities.Auction.Auction;

namespace AuctionWebAPI.Boundaries.Repositories
{
    public interface IAuctionRepository
    {
        int Create(AuctionEntity auction);
        AuctionEntity Retrieve(int auctionId);
        IEnumerable<AuctionEntity> RetrieveAll();
        bool Delete(int auctionId);
        bool Edit(AuctionEntity auctionEntity);
    }
}
