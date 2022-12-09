using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TGFDelivery.Interfaces
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
