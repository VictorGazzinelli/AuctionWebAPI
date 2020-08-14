using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionWebAPI.Utils.InversionControl
{
    public class OverrideMappingType : IOverrideMapping
    {
        public Type From { get; set; }
        public object To { get; set; }
    }
}
