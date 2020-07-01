using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecomaerce.Hubs
{
    [HubName("ProductsHub")]
    public class ProductsHub:Hub
    {
    }
}