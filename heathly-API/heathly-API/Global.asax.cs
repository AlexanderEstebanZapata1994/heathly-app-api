using heathly_API.Configuration;
using System;
using System.Web.Http;

namespace heathly_API
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(HeathlyWebAPIConfig.Register);
        }
    }
}