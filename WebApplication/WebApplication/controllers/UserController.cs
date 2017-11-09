using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace WebApplication.controllers
{
    public class UserController : NancyModule
    {
        public UserController()
        {
            Get["/user"] = parameters => "Hello user.";
        }
    }
}