using AuctionWebAPI.Boundaries.Services.Account;
using AuctionWebAPI.Models.Account;
using AuctionWebAPI.Utils;
using AuctionWebAPI.Utils.Boundary;
using AuctionWebAPI.Utils.InversionControl;
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
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Account
        ///     {        
        ///       "Username": "Admin",
        ///       "Password": "administrator",
        ///       "PasswordConfirmation": "administrator"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200"> Returns the newly created account's username and authToken </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPost]
        [Consumes("application/json")]
        [Route("api/[controller]")]
        [ProducesResponseType(typeof(CreateAccountOutput), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        public CreateAccountOutput CreateAccount(CreateAccountInput input)
        {
            CreateAccountResponse response = DependencyResolver.Instance()
                .GetInstanceOf<IService<CreateAccountRequest, CreateAccountResponse>>()
                .Run(new CreateAccountRequest()
                {
                    Username = input.Username,
                    Password = input.Password,
                    PasswordConfirmation = input.PasswordConfirmation,
                });

            return new CreateAccountOutput()
            {
                Account = response.Account,
                AuthToken = response.AuthToken
            };
        }

        /// <summary>
        /// Login to existing account
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Account/Login
        ///     {        
        ///       "Username": "Admin",
        ///       "Password": "administrator"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200"> Returns the username and authtoken of the user </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpPost]
        [Consumes("application/json")]
        [Route("api/[controller]/login")]
        [ProducesResponseType(typeof(CreateAccountOutput), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        public LoginAccountOutput LoginAccount(LoginAccountInput input)
        {
            LoginAccountResponse response = DependencyResolver.Instance()
                .GetInstanceOf<IService<LoginAccountRequest, LoginAccountResponse>>()
                .Run(new LoginAccountRequest()
                {
                    Username = input.Username,
                    Password = input.Password,
                });

            return new LoginAccountOutput()
            {
                Account = response.Account,
                AuthToken = response.AuthToken
            };
        }

        /// <summary>
        /// Deletes an account
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Account
        ///     {        
        ///       "AccountId": 1
        ///     }
        ///     
        /// </remarks>
        /// <response code="200"> Returns the AccountId of the requested account deleted </response>
        /// <response code="400"> Bad Request </response>
        /// <response code="500"> Internal Server Error </response>
        [HttpDelete]
        [Consumes("application/json")]
        [Route("api/[controller]")]
        [ProducesResponseType(typeof(DeleteAccountOutput), 200)]
        [ProducesResponseType(typeof(ErrorMessage), 400)]
        [ProducesResponseType(typeof(ErrorMessage), 500)]
        public DeleteAccountOutput DeleteAccount(DeleteAccountInput input)
        {
            DeleteAccountResponse response = DependencyResolver.Instance()
                .GetInstanceOf<IService<DeleteAccountRequest, DeleteAccountResponse>>()
                .Run(new DeleteAccountRequest()
                {
                    AccountId = input.AccountId
                });

            return new DeleteAccountOutput()
            {
                AccountId = response.AccountId
            };
        }
    }
}
