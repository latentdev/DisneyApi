using Flurl.Http;
using ridetimes.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Http;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ridetimes.ViewModel;

namespace ridetimes.Helper
{
    public class Ride
    {
        public static async Task<List<Models.Entry>> Fetch(string parkID)
        {
            
            var token = Auth_Token.Instance;
            if (token.access_token == null || token.expires_in == 0)
            {
                API_Credentials creds = new API_Credentials();

                var responseString = await creds.api_accessTokenURL
                .PostUrlEncodedAsync(creds.api_accessTokenURLBody)
                .ReceiveString();
                var str = JsonConvert.DeserializeObject(responseString.ToString());
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(str.ToString());

                token.access_token = dict["access_token"];
                token.token_type = dict["token_type"];
                token.scope = dict["scope"];
                token.expires_in = Convert.ToInt32(dict["expires_in"]);
                Times.countdown(token);
            }
            
            string url = "https://api.wdpro.disney.go.com/facility-service/theme-parks/" + parkID + "/wait-times";

            var response = await url.WithOAuthBearerToken(token.access_token).GetAsync();

            //var cont = response.Content;
            return BuildRide(await response.Content.ReadAsStringAsync()); //Json("true");
        }

        public static List<Models.Entry> BuildRide(string json)
        {
            dynamic obj = JsonConvert.DeserializeObject(json);
            List<Models.Entry> rides = new List<Models.Entry>();
            foreach (var k in obj.entries)
            {
                Models.Entry ride = new Models.Entry();
                ride.name = k.name;
                //ride.id = k.id;
                ride.type = k.type;
                ride.waitTime.status = k.waitTime.status;
                ride.waitTime.singleRider = k.waitTime.singleRider;
                ride.waitTime.postedWaitMinutes = Convert.ToInt32(k.waitTime.postedWaitMinutes);
                ride.waitTime.rollUpStatus = k.waitTime.rollUpStatus;
                ride.waitTime.rollUpWaitTimeMessage = k.waitTime.rollUpWaitTimeMessage;
                ride.waitTime.fastPass.available = k.waitTime.fastPass.available;
                if (ride.waitTime.fastPass.available == true)
                {
                    ride.waitTime.fastPass.startTime = k.waitTime.fastPass.startTime;
                    ride.waitTime.fastPass.endTime = k.waitTime.fastPass.endTime;
                }
                rides.Add(ride);
            }
            return rides;
            //token.rides = rides;
        }

        public static RideViewModel GetRideViewModel(Entry entry)
        {
            return new RideViewModel { Name = entry.name, WaitTime = entry.waitTime.postedWaitMinutes, Status = entry.waitTime.rollUpStatus, Description="Placeholder for Data", History = new List<string> { "Placeholder for Data" } };
        }
    }
}