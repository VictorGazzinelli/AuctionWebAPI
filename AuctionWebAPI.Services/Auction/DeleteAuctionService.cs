using AuctionWebAPI.Boundaries.Dtos;
using AuctionWebAPI.Boundaries.Repositories;
using AuctionWebAPI.Boundaries.Services.Auction;
using AuctionWebAPI.Models.Auction;
using AuctionWebAPI.Utils.Boundary;
using AuctionWebAPI.Utils.Exceptions;
using AuctionWebAPI.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using AuctionEntity = AuctionWebAPI.Entities.Auction.Auction;

namespace AuctionWebAPI.Services.Auction
{
    public class DeleteAuctionService : IService<DeleteAuctionRequest, DeleteAuctionResponse>
    {
        #region Repositories
        private readonly IAuctionRepository auctionRepository;
        #endregion

        #region Constructors
        public DeleteAuctionService(IAuctionRepository auctionRepository)
        {
            this.auctionRepository = auctionRepository;
        }
        #endregion

        public DeleteAuctionResponse Run(DeleteAuctionRequest request)
        {
            AuctionEntity auctionEntity = ValidateRequest(request);

            if (!auctionRepository.Delete(request.AuctionId))
                throw new InvalidParamException("AuctionId", request.AuctionId);

            return new DeleteAuctionResponse()
            {
                Auction = new AuctionDTO(auctionEntity)
            };
        }

        private AuctionEntity ValidateRequest(DeleteAuctionRequest request)
        {
            AuctionEntity auctionEntity = auctionRepository.Retrieve(request.AuctionId);

            if (auctionEntity == null)
                throw new InvalidParamException("AuctionId", request.AuctionId);

            return auctionEntity;
        }
    }
}
