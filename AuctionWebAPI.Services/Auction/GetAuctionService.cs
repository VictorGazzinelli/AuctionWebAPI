using AuctionWebAPI.Boundaries.Dtos;
using AuctionWebAPI.Boundaries.Repositories;
using AuctionWebAPI.Boundaries.Services.Account;
using AuctionWebAPI.Models.Auction;
using AuctionWebAPI.Utils.Boundary;
using AuctionWebAPI.Utils.Exceptions;
using AuctionWebAPI.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using AuctionEntity = AuctionWebAPI.Entities.Auction.Auction;

namespace AuctionWebAPI.Services.Account
{
    public class GetAuctionService : IService<GetAuctionRequest, GetAuctionResponse>
    {
        #region Repositories
        private readonly IAuctionRepository auctionRepository;
        #endregion

        #region Constructors
        public GetAuctionService(IAuctionRepository auctionRepository)
        {
            this.auctionRepository = auctionRepository;
        }
        #endregion

        public GetAuctionResponse Run(GetAuctionRequest request)
        {
            AuctionEntity auctionEntity = auctionRepository.Retrieve(request.AuctionId);

            if (auctionEntity == null)
                throw new InvalidParamException("AuctionId", request.AuctionId);

            return new GetAuctionResponse()
            {
                Auction = new AuctionDTO(auctionEntity)
            };
        }
    }
}
