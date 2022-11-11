using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SV19T1081011.AdminTool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest()
        {
            try
            {
                WebPrincipal principal;
                if (User != null && User.Identity.IsAuthenticated && String.Compare(User.Identity.AuthenticationType, "Forms", false) == 0)
                {
                    WebUserData userData = WebUserData.FromCookie(User.Identity.Name);
                    if (userData != null)
                    {
                        principal = new WebPrincipal(User.Identity, userData);
                    }
                    else
                    {
                        principal = null;
                    }
                    HttpContext.Current.User = Thread.CurrentPrincipal = principal;
                }
                else
                {
                    HttpContext.Current.User = Thread.CurrentPrincipal = null;
                }
            }
            catch
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.User = Thread.CurrentPrincipal = null;
            }
        }
    }
}
