using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Utils.Boundary
{
    public interface IService<TRequest, TResponse>
    {
        TResponse Run(TRequest request);
    }

    public interface IServiceWithoutRequest<TResponse>
    {
        TResponse Run();
    }
}
