using AuctionWebAPI.Boundaries.Dtos;
using AuctionWebAPI.Boundaries.Repositories;
using AuctionWebAPI.Boundaries.Services.Account;
using AuctionWebAPI.Models.Auction;
using AuctionWebAPI.Utils.Boundary;
using AuctionWebAPI.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuctionEntity = AuctionWebAPI.Entities.Auction.Auction;

namespace AuctionWebAPI.Services.Account
{
    public class ListAuctionService : IServiceWithoutRequest<ListAuctionResponse>
    {
        #region Repositories
        private readonly IAuctionRepository auctionRepository;
        #endregion

        #region Constructors
        public ListAuctionService(IAuctionRepository auctionRepository)
        {
            this.auctionRepository = auctionRepository;
        }
        #endregion

        public ListAuctionResponse Run()
        {
            IEnumerable<AuctionEntity> enumerableAuctionEntity = auctionRepository.RetrieveAll();

            return new ListAuctionResponse()
            {
                ArrAuction = enumerableAuctionEntity.Select(auctionEntity => new AuctionDTO(auctionEntity)).ToArray()
            };
        }
    }
}
