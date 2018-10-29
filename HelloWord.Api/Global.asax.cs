using HelloWorld.Data.Connection;
using HelloWorld.Data.Repos;
using LightInject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace HelloWord.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new ServiceContainer();
            container.RegisterApiControllers();
            container.EnableWebApi(GlobalConfiguration.Configuration);
            container.Register<IMessageRepo, MessageStaticRepo>();
            container.Register<IConnectionFactory>(factory => {
                var connStr = ConfigurationManager.AppSettings["connStr"].ToString();
                return new ConnectionFactory(connStr);
            });
            container.Register<IDbContext, DbContext>();
            //container.Register<IMessageRepo, MessageDbRepo>(); //Powered by DB

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}