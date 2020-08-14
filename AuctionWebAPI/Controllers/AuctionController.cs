using AuctionWebAPI.Boundaries.Services.Auction;
using AuctionWebAPI.Models.Auction;
using AuctionWebAPI.Utils;
using AuctionWebAPI.Utils.Boundary;
using AuctionWebAPI.Utils.InversionControl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionWebAPI.Controllers
{
    [Produces("application/json")]
    [RequireHttps]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        /// <summary>
        /// Creates a new auction
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Auction
        ///     {        
        ///       "Name": "MyAuction",
        ///       "InitialValue": 10.00,
        ///       "IsItemUsed": true,
        ///       "OpenedAt": "2020-08-09T23:18:21.830Z",
        ///       "ClosedAt": "2020-08-11T23:18:21.830Z"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200"> Returns the newly created auction </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPost]
        [Consumes("application/json")]
        [Route("api/[controller]")]
        [Authorize(Policy = "JWT")]
        [ProducesResponseType(typeof(CreateAuctionOutput), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 401)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        public CreateAuctionOutput CreateAuction(CreateAuctionInput input)
        {
            CreateAuctionResponse response = DependencyResolver.Instance()
                .GetInstanceOf<IService<CreateAuctionRequest, CreateAuctionResponse>>()
                .Run(new CreateAuctionRequest()
                {
                    Name = input.Name,
                    InitialValue = input.InitialValue,
                    IsItemUsed = input.IsItemUsed,
                    OpenedAt = input.OpenedAt,
                    ClosedAt = input.ClosedAt,
                });

            return new CreateAuctionOutput()
            {
                Auction = response.Auction
            };
        }

        /// <summary>
        /// Gets an auction by AuctionId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Auction?AuctionId=1   
        ///     
        /// </remarks>
        /// <response code="200"> Returns the requested auction </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet]
        [Consumes("application/json")]
        [Route("api/[controller]")]
        [Authorize(Policy = "JWT")]
        [ProducesResponseType(typeof(GetAuctionOutput), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 401)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        public GetAuctionOutput GetAuction(int AuctionId)
        {
            GetAuctionResponse response = DependencyResolver.Instance()
                .GetInstanceOf<IService<GetAuctionRequest, GetAuctionResponse>>()
                .Run(new GetAuctionRequest()
                {
                    AuctionId = AuctionId
                });

            return new GetAuctionOutput()
            {
                Auction = response.Auction
            };
        }

        /// <summary>
        /// Edit an auction
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/Auction
        ///     {        
        ///       "AuctionId": 1,
        ///       "Name": "MyAuction",
        ///       "InitialValue": 10.00,
        ///       "IsItemUsed": true,
        ///       "OpenedAt": "2020-08-12T23:18:21.830Z",
        ///       "ClosedAt": "2020-08-14T23:18:21.830Z"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200"> Returns the newly edited auction </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPut]
        [Consumes("application/json")]
        [Route("api/[controller]")]
        [Authorize(Policy = "JWT")]
        [ProducesResponseType(typeof(EditAuctionOutput), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 401)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        public EditAuctionOutput EditAuction(EditAuctionInput input)
        {
            EditAuctionResponse response = DependencyResolver.Instance()
                .GetInstanceOf<IService<EditAuctionRequest, EditAuctionResponse>>()
                .Run(new EditAuctionRequest()
                {
                    Name = input.Name,
                    InitialValue = input.InitialValue,
                    IsItemUsed = input.IsItemUsed,
                    OpenedAt = input.OpenedAt,
                    ClosedAt = input.ClosedAt,
                });

            return new EditAuctionOutput()
            {
                Auction = response.Auction
            };
        }

        /// <summary>
        /// Delete an auction
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Auction
        ///     {        
        ///       "AuctionId": 1,
        ///     }
        ///     
        /// </remarks>
        /// <response code="200"> Returns the recenlty deleted auction </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpDelete]
        [Consumes("application/json")]
        [Route("api/[controller]")]
        [Authorize(Policy = "JWT")]
        [ProducesResponseType(typeof(DeleteAuctionOutput), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 401)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        public DeleteAuctionOutput DeleteAuction(DeleteAuctionInput input)
        {
            DeleteAuctionResponse response = DependencyResolver.Instance()
                .GetInstanceOf<IService<DeleteAuctionRequest, DeleteAuctionResponse>>()
                .Run(new DeleteAuctionRequest()
                {
                    AuctionId = input.AuctionId
                });

            return new DeleteAuctionOutput()
            {
                Auction = response.Auction
            };
        }

        /// <summary>
        /// Lists all auctions
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Auction/all    
        ///     
        /// </remarks>
        /// <response code="200"> Returns the requested auction </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpGet]
        [Consumes("application/json")]
        [Route("api/[controller]/all")]
        [Authorize(Policy = "JWT")]
        [ProducesResponseType(typeof(ListAuctionOutput), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 401)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        public ListAuctionOutput ListAuction()
        {
            ListAuctionResponse response = DependencyResolver.Instance()
                .GetInstanceOf<IServiceWithoutRequest<ListAuctionResponse>>()
                .Run();

            return new ListAuctionOutput()
            {
                ArrAuction = response.ArrAuction
            };
        }
    }
}
