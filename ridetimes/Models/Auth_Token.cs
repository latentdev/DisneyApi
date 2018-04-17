using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ridetimes.Models
{
    public class Auth_Token
    {
        public String access_token { get; set; }
        public String token_type { get; set; }
        public String scope { get; set; }
        public int expires_in { get; set; }

        public String json { get; set; }
        public List<Models.Entry> rides { get; set; }

        private static Auth_Token instance;

        private Auth_Token() { }

        public static Auth_Token Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Auth_Token();
                }
                return instance;
            }
        }

    }
}