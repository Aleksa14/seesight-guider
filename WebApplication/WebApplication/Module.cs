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
                    userDb.Users.Add(new Models.ModelUser{UserName = "FantyG"});
                    userDb.Users.Add(new Models.ModelUser { UserName = "Aleksa14" });
                    userDb.SaveChanges();

                    string res = "";
                    foreach (var user in userDb.Users)
                    {
                        res += user.UserName + " " + user.UserId + " ";
                    }
                    return res;
                }
            };
        }
    }
}