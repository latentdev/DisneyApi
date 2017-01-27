using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ridetimes.Models
{
    public class API_Credentials
    {
        public String api_accessTokenURL = "https://authorization.go.com/token";
        public String api_accessTokenURLBody = "grant_type=assertion&assertion_type=public&client_id=WDPRO-MOBILE.MDX.WDW.ANDROID-PROD";
        public String api_accessTokenURLMethod = "POST";
        public String api_appID = "WDW-MDX-ANDROID-3.4.1";
        public String api_baseURL = "https://api.wdpro.disney.go.com/";
    }
}