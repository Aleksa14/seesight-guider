using Nancy;

namespace WebApplication
{
    public class Module : NancyModule
    {
        public Module()
        {
            Get["/"] = parameter =>
            {
                using (var userDb = new WebApplication.Contexts.MainContext())
                {
                    
                }
            };
        }
    }
}