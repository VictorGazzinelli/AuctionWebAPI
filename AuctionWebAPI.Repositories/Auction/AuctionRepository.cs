using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuctionWebAPI.Boundaries.Repositories;
using AuctionEntity = AuctionWebAPI.Entities.Auction.Auction;

namespace AuctionWebAPI.Repositories.Game
{
    public class AuctionRepository : IAuctionRepository
    {
        static List<AuctionEntity> list_Auctions_In_Memory = new List<AuctionEntity>();

        public int Create(AuctionEntity auction)
        {
            int auctionId = list_Auctions_In_Memory.Count() + 1;
            auction.AuctionId = auctionId;
            list_Auctions_In_Memory.Add(auction);

            return auctionId;
        }

        public AuctionEntity Retrieve(int auctionId)
        {
            return list_Auctions_In_Memory.Where(auction => String.Equals(auction.AuctionId, auctionId))
                .FirstOrDefault();
        }

        public IEnumerable<AuctionEntity> RetrieveAll()
        {
            return list_Auctions_In_Memory;
        }

        public bool Edit(AuctionEntity auctionEntity)
        {
            AuctionEntity auctionEntityResponse =
                list_Auctions_In_Memory.Where(auction => String.Equals(auction.AuctionId, auctionEntity.AuctionId))
                    .FirstOrDefault();

            if (auctionEntity == null)
                return false;

            list_Auctions_In_Memory.Remove(list_Auctions_In_Memory.First(auction => auction.AuctionId == auctionEntity.AuctionId));
            list_Auctions_In_Memory.Add(auctionEntityResponse);

            return true;
        }

        public bool Delete(int auctionId)
        {
            list_Auctions_In_Memory = list_Auctions_In_Memory.Where(Auction => Auction.AuctionId != auctionId).ToList();
            return !list_Auctions_In_Memory.Any(account => account.AuctionId == auctionId);
        }
    }
}
