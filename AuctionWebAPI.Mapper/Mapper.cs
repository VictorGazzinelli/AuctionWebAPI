using AuctionWebAPI.Boundaries.Repositories;
using AuctionWebAPI.Boundaries.Services.Account;
using AuctionWebAPI.Boundaries.Services.Auction;
using AuctionWebAPI.Models.Auction;
using AuctionWebAPI.Repositories.Game;
using AuctionWebAPI.Services.Account;
using AuctionWebAPI.Services.Auction;
using AuctionWebAPI.Utils.Boundary;
using AuctionWebAPI.Utils.InversionControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace AuctionWebAPI.Mapper
{
    public class Mapper
    {
        private static Mapper _instance = new Mapper();
        public static Mapper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Mapper();
                return _instance;
            }
        }

        private Mapper() { }

        public void RegisterMappings() => DependencyResolver.Instance().LoadMappings(ListMappings());

        private Mapping[] ListMappings()
        {
            List<Mapping> mappingList = new List<Mapping>();

            #region Service Mapping

            #region Account

            mappingList.Add(new Mapping(typeof(IService<CreateAccountRequest, CreateAccountResponse>), typeof(CreateAccountService)));
            mappingList.Add(new Mapping(typeof(IService<DeleteAccountRequest, DeleteAccountResponse>), typeof(DeleteAccountService)));
            mappingList.Add(new Mapping(typeof(IService<LoginAccountRequest, LoginAccountResponse>), typeof(LoginAccountService)));

            #endregion

            #region Auction

            mappingList.Add(new Mapping(typeof(IService<CreateAuctionRequest, CreateAuctionResponse>), typeof(CreateAuctionService)));
            mappingList.Add(new Mapping(typeof(IService<DeleteAuctionRequest, DeleteAuctionResponse>), typeof(DeleteAuctionService)));
            mappingList.Add(new Mapping(typeof(IService<GetAuctionRequest, GetAuctionResponse>), typeof(GetAuctionService)));
            mappingList.Add(new Mapping(typeof(IService<EditAuctionRequest, EditAuctionResponse>), typeof(EditAuctionService)));
            mappingList.Add(new Mapping(typeof(IServiceWithoutRequest<ListAuctionResponse>), typeof(ListAuctionService)));

            #endregion

            #endregion

            #region Repository Mapping

            mappingList.Add(new Mapping(typeof(IAccountRepository), typeof(AccountRepository)));
            mappingList.Add(new Mapping(typeof(IAuctionRepository), typeof(AuctionRepository)));

            #endregion

            return mappingList.ToArray();
        }
    }
}
