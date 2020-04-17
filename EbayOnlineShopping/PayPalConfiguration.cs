using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EbayOnlineShopping
{
    public class PayPalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;

        static PayPalConfiguration()
        {
            var config = getconfig();
            clientId = "AY_VisoiQnQixlPXXqRcWs8xcltyFH3la22LRPVojvVffXLoyYR-9A2XmgWdRQ93t2FS7K5aNM8xqJDL";
            clientSecret = "EPaL-wk_QWQhjt2AkpL-B0dCpcV6rzVQxLkvsKIK-Y0kC92cPds8DCVftCw1Mlw_7nuJHoz8FBHxs7hE";
        }

        private static Dictionary<string, string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();

        }

        // create access toekn
        private static string GetAccessToken()
        {
            string AccessToken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();
            return AccessToken;
        }

        // this will retrun api context object
        public static APIContext GetAPIContext()
        {
            APIContext aPIContext = new APIContext(GetAccessToken());
            aPIContext.Config = getconfig();
            return aPIContext;
        }
    }
}