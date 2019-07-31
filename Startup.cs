using System.Web.Http;
using System.Web.Http.Cors;
using Owin;

namespace SelfHostedImageGenerator
{
    [EnableCors("*", "*", "*")]
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.EnableCors(new EnableCorsAttribute("*", "*", "GET, POST, OPTIONS, PUT, DELETE"));

            appBuilder.UseWebApi(config);
        }
    }
}