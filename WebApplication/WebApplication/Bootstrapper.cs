using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace WebApplication
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            // your customization goes here
        }

    }
}