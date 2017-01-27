using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Flurl.Http;

namespace ridetimes.Controllers
{
    public class EndPointController : ApiController
    {
        [HttpGet]
        public async void FetchTimes()
        {
            ridetimes.Models.API_Credentials creds = new ridetimes.Models.API_Credentials();
            var responseString = await creds.api_accessTokenURL
            .PostUrlEncodedAsync(creds.api_accessTokenURLBody)
            .ReceiveString();
            //var token = responseString;
            var token = Json(responseString.ToString());
        }

    }
}
