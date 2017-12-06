using Nancy;
using Nancy.Bootstrapper;
using Nancy.Session;
using Nancy.TinyIoc;

namespace WebApplication
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            CookieBasedSessions.Enable(pipelines);
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "http://localhost:3000")
                    .WithHeader("Access-Control-Allow-Methods", "POST, GET, PUT, DELETE, OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type, Credentials")
                    .WithHeader("Access-Control-Expose-Headers", "Accept, Origin, Content-type, Credentials")
                    .WithHeader("Access-Control-Allow-Credentials", "true");
            });
            
        }

    }
}