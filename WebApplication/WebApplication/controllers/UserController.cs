using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using Nancy.ModelBinding.DefaultBodyDeserializers;
using WebApplication.Exeptions;

namespace WebApplication.controllers
{
    public class UserController : NancyModule
    {
        private class PostUserBody
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
            var user = Services.ServiceUser.AuthorizeUser(postBody.UserName, postBody.Password);
            if (user == null)
            {
                return HttpStatusCode.Unauthorized;
            }

            Request.Session["username"] = user.UserName;
            return Response.AsJson(user);
            
        }

        private dynamic PostUser(dynamic parameters)
        {
            string userName = parameters.username;
            PostUserBody postBody;
            try
            {
                postBody = this.Bind<PostUserBody>();
            }
            catch (Nancy.ModelBinding.ModelBindingException)
            {
                return Response.AsJson(new Exeptions.UserCreationExeption{ErrorMessage = "Body not completed"}, HttpStatusCode.BadRequest);
            }
            try
            {
                Services.ServiceUser.CreateUser(userName, postBody.Email, postBody.Password);
            }
            catch (Exeptions.UserCreationExeption exc)
            {
                return Response.AsJson(exc.ErrorMessage, HttpStatusCode.BadRequest);
            }

            return HttpStatusCode.OK;
        }


    }
}