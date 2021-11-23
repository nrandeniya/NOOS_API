using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_UI.Static
{
    // in case i have to change the URL, only this place to change 
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:44390";  //from API-launchSetting.json  ssl 44390
        public static string PruductsEndpoint = $"{BaseUrl}/api/products/";
        public static string SellersEndpoint = $"{BaseUrl}/api/sellers/";
        public static string BuyersEndpoint = $"{BaseUrl}/api/buyers/";
        public static string SubscriptionsEndpoint = $"{BaseUrl}/api/subscriptions/";
        public static string RegisterEndpoint = $"{BaseUrl}/api/users/register/";  // declared in AuthenticationRepository in Services
        public static string LoginEndpoint = $"{BaseUrl}/api/users/login/";

    }
}
