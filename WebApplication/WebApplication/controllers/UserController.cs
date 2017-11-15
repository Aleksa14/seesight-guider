using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using Nancy.ModelBinding.DefaultBodyDeserializers;
using WebApplication.Contexts;
using WebApplication.Exeptions;
using WebApplication.Models;

namespace WebApplication.controllers
{

    public class UserController : NancyModule
    {
        public const string SessionUserNameKey = "username";
        private struct PostUserBody
        {
            public string Password { get; set; }
            public string Email { get; set; }
        }

        private struct PostAuthBody
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public UserController()
        {
            Post["/api/users/{username}"] = PostUser;
            Post["/api/user-auth"] = AuthenticateUser;
        }

        private dynamic AuthenticateUser(dynamic parameters)
        {
            PostAuthBody postBody;
            try
            {
                postBody = this.Bind<PostAuthBody>();
            }
            catch (Nancy.ModelBinding.ModelBindingException)
            {
                return Response.AsJson("Body not completed", HttpStatusCode.BadRequest);
            }
            if (Request.Session["username"] != null && (string) Request.Session["username"] == postBody.UserName)
            {
                return Response.AsJson("Already logged.");
            }
            var db = new MainContext();
            var user = Services.ServiceUser.AuthorizeUser(postBody.UserName, postBody.Password, db);
            if (user == null)
            {
                return HttpStatusCode.Unauthorized;
            }

            Request.Session[SessionUserNameKey] = user.UserName;
            return Response.AsJson(new ViewModelUser(user));
            
        }

        private dynamic PostUser(dynamic parameters)
        {
            string userName = parameters.username;
            PostUserBody postBody;
            try
            {
                postBody = this.Bind<PostUserBody>();
                if (postBody.Email == null || postBody.Password == null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return Response.AsJson("Body not completed", HttpStatusCode.BadRequest);
            }
            try
            {
                var db = new MainContext();
                Services.ServiceUser.CreateUser(userName, postBody.Email, postBody.Password, db);
            }
            catch (Exeptions.UserCreationExeption exc)
            {
                return Response.AsJson(exc.ErrorMessage, HttpStatusCode.BadRequest);
            }

            return HttpStatusCode.OK;
        }


    }
}