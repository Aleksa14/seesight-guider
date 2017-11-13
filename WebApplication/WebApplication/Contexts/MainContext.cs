using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Contexts
{
    public class MainContext : DbContext
    {
        public MainContext() : base("MainDb")
        {
        }

        public DbSet<ModelUser> Users { get; set; }
    }
}