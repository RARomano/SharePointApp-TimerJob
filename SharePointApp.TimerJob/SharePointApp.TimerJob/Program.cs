using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointApp.TimerJob
{
    class Program
    {
        static void Main(string[] args)
        {
            string siteUrl = "http://localhost";
            Uri uri = new Uri(siteUrl);

            string realm = TokenHelper.GetRealmFromTargetUrl(uri);

            //Get the access token for the URL.  
            //   Requires this app to be registered with the tenant
            string accessToken = TokenHelper.GetAppOnlyAccessToken(
                TokenHelper.SharePointPrincipal,
                uri.Authority, realm).AccessToken;

            using (var ctx = TokenHelper.GetClientContextWithAccessToken(uri.ToString(), accessToken))
            {
                /// codigo vai aqui
                ctx.Load(ctx.Web);
                var webTitle = ctx.Web.Title;

                ctx.ExecuteQuery();

                Console.Write(webTitle);

            }
        }
    }
}
