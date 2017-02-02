using Flurl.Http;
using ridetimes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ridetimes.Helper
{
    public static class Times
    {
        public static async void Timer()
        {
            /*var timer = new System.Threading.Timer((e) =>
            {
                Init();
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));*/
            Init();
            while (true)
            {
                await Task.Delay(TimeSpan.FromMinutes(5));
                Init();
            }
        }
        public static async Task<Boolean> Init()
        {
            await Fetch();
            Response();
            return true;
        }

        public static async Task<Boolean> Fetch()
        {
            API_Credentials creds = new API_Credentials();
            var token = Auth_Token.Instance;
            if (token.access_token == null || token.expires_in == 0)
            {
                var responseString = await creds.api_accessTokenURL
                .PostUrlEncodedAsync(creds.api_accessTokenURLBody)
                .ReceiveString();
                
                var str = JsonConvert.DeserializeObject(responseString.ToString());
                Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(str.ToString());

                token.access_token = dict["access_token"];
                token.token_type = dict["token_type"];
                token.scope = dict["scope"];
                token.expires_in = Convert.ToInt32(dict["expires_in"]);
                countdown(token);
            }
            var response = await "https://api.wdpro.disney.go.com/facility-service/theme-parks/330339/wait-times".WithOAuthBearerToken(token.access_token).GetAsync();

            token.json = await response.Content.ReadAsStringAsync();
            var cont = response.Content;
            return true;

            /*using (var reader = new System.IO.StreamReader(response., ASCIIEncoding.ASCII))
            {
                string responseText = reader.ReadToEnd();
            }*/
        }
        public static async void countdown(Auth_Token token)
        {
            while (token.expires_in > 0)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1000));
                token.expires_in -= 1;
                System.Diagnostics.Debug.WriteLine("expires in: " + token.expires_in);
            }
        }
        public static void Response()
        {
            var token = Auth_Token.Instance;
            dynamic obj = JsonConvert.DeserializeObject(token.json);
            List<Ride> rides = new List<Ride>();
            foreach (var k in obj.entries)
            {
                Ride ride = new Ride();
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
                    ride.waitTime.fastPass.start = k.waitTime.fastPass.start;
                    ride.waitTime.fastPass.end = k.waitTime.fastPass.end;
                }
                rides.Add(ride);
            }
            token.rides = rides;
        }

    }
}
