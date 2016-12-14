using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineBanking.Startup))]
namespace OnlineBanking
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
