using HelloWorld.Data.Connection;
using HelloWorld.Data.Repos;
using LightInject;
using System;
using System.Configuration;
using System.Linq;

namespace HelloWorldConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = InitDI();

            var repo = container.GetInstance<IMessageRepo>();

            var messages = repo.GetMessages().Result.ToList();

            messages.ForEach(w => Console.WriteLine(w.Text));

        }

        static ServiceContainer InitDI()
        {
            var container = new ServiceContainer();
            container.Register<IMessageRepo, MessageStaticRepo>();
            container.Register<IConnectionFactory>(factory => {
                var connStr = ConfigurationManager.AppSettings["connStr"].ToString();
                return new ConnectionFactory(connStr);
            });
            container.Register<IDbContext, DbContext>();
            //container.Register<IMessageRepo, MessageDbRepo>(); //Powered by DB

            return container;
        }
    }
}
