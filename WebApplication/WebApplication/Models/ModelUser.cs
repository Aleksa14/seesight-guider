using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class ModelUser
    {
        public enum UserRoleType
        {
            NormalUser, Moderator
        }
        [Key]
        public int UserId { get; set; }
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        [Index(IsUnique = true)]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRoleType UserRole { get; set; } = UserRoleType.NormalUser;
        public virtual ICollection<ModelPlace> OwnedPlaces { get; set; } = new HashSet<ModelPlace>();
    }
}