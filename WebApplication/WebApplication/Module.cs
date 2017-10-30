using Nancy;

namespace WebApplication
{
    public class Module : NancyModule
    {
        public Module()
        {
            Get["/"] = parameter => "Hello!";
        }
    }
}