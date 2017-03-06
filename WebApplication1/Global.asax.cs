using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Akka.Actor;
using WebApplication1.Actors;
using WebApplication1.ActorSystem;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // setup akka system
            // config automatically loads from App.config / Web.config
            ActorSystemRefs.ActorSystem = Akka.Actor.ActorSystem.Create("myactorsystem");
            SystemActors.ExampleActorRef = ActorSystemRefs.ActorSystem.ActorOf(Props.Create(() => new ExampleActor()), "exampleActor");
        }

        protected async void Application_End()
        {
            await ActorSystemRefs.ActorSystem.Terminate();

            //wait up to two seconds for a clean shutdown
            await ActorSystemRefs.ActorSystem.WhenTerminated;
        }
    }
}
