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
    public class EditAuctionService : IService<EditAuctionRequest, EditAuctionResponse>
    {
        #region Repositories
        private readonly IAuctionRepository auctionRepository;
        #endregion

        #region Constructors
        public EditAuctionService(IAuctionRepository auctionRepository)
        {
            this.auctionRepository = auctionRepository;
        }
        #endregion

        public EditAuctionResponse Run(EditAuctionRequest request)
        {
            ValidateRequest(request);

            AuctionEntity auctionEntity = new AuctionEntity()
            {
                AuctionId = request.AuctionId,
                InitialValue = request.InitialValue,
                Name = request.Name,
                ClosedAt = request.ClosedAt,
                OpenedAt = request.OpenedAt,
                ResponsibleId = AccountHelper.GetAccountIdFromJWT(),
                Owner = AccountHelper.GetUsernameFromJWT(),
                IsItemUsed = request.IsItemUsed
            };

            auctionRepository.Edit(auctionEntity);

            return new EditAuctionResponse()
            {
                Auction = new AuctionDTO(auctionEntity)
            };
        }

        private void ValidateRequest(EditAuctionRequest request)
        {
            AuctionEntity auctionEntity = auctionRepository.Retrieve(request.AuctionId);

            if (auctionEntity == null)
                throw new InvalidParamException("AuctionId", request.AuctionId);

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new InvalidParamException("Name", request.Name);

            if (request.InitialValue <= 0)
                throw new BadRequestException("O valor inicial não pode ser inferior ou igual a zero");

            if (request.ClosedAt <= request.OpenedAt)
                throw new BadRequestException("A data final não pode ser anterior ou igual a data de abertura");
        }
    }
}
