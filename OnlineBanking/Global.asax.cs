using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OnlineBanking.BusinessProcess;
using OnlineBanking.DAL;

namespace OnlineBanking
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            OnlineBanking.BusinessProcess.DatabaseInitializer dbInit = new OnlineBanking.BusinessProcess.DatabaseInitializer();
            dbInit.InitializeDatabase(new BankContext());
        }
    }
}
