using AuctionWebAPI.Boundaries.Dtos;
using AuctionWebAPI.Boundaries.Repositories;
using AuctionWebAPI.Boundaries.Services.Auction;
using AuctionWebAPI.Utils.Boundary;
using AuctionWebAPI.Utils.Exceptions;
using AuctionWebAPI.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using AuctionEntity = AuctionWebAPI.Entities.Auction.Auction;

namespace AuctionWebAPI.Services.Auction
{
    public class CreateAuctionService : IService<CreateAuctionRequest, CreateAuctionResponse>
    {
        #region Repositories
        private readonly IAuctionRepository auctionRepository;
        #endregion

        #region Constructors
        public CreateAuctionService(IAuctionRepository auctionRepository)
        {
            this.auctionRepository = auctionRepository;
        }
        #endregion

        public CreateAuctionResponse Run(CreateAuctionRequest request)
        {
            ValidateRequest(request);

            AuctionEntity auctionEntity = new AuctionEntity()
            {
                Name = request.Name,
                InitialValue = request.InitialValue,
                IsItemUsed = request.IsItemUsed,
                ResponsibleId = AccountHelper.GetAccountIdFromJWT(),
                Owner = AccountHelper.GetUsernameFromJWT(),
                ClosedAt = request.ClosedAt,
                OpenedAt = request.OpenedAt,
            };

            int auctionId = auctionRepository.Create(auctionEntity);

            auctionEntity.AuctionId = auctionId;

            return new CreateAuctionResponse()
            {
                Auction = new AuctionDTO(auctionEntity)
            };
        }

        private void ValidateRequest(CreateAuctionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new InvalidParamException("Name", request.Name);

            if (request.InitialValue <= 0)
                throw new BadRequestException("O valor inicial não pode ser inferior ou igual a zero");

            if (request.ClosedAt <= request.OpenedAt)
                throw new BadRequestException("A data final não pode ser anterior ou igual a data de abertura");
        }
    }
}
