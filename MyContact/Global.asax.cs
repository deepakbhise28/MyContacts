using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyContact
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                Instrumentation.LogWriter.Instance.LogError($"Error for User {User.Identity.Name}", ex);
            }
            else
            {
                Instrumentation.LogWriter.Instance.LogError(ex);
            }
         //   RedirectResult
            //log the error!
            //_Logger.Error(ex);
        }
    }
}
