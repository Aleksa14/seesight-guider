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
            NormalUser,
            Moderator
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

        public override bool Equals(object obj)
        {
            if (obj is ModelUser user)
            {
                return user.UserName == UserName;
            }
            return false;
        }

        public struct View
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public ModelUser.UserRoleType UserRole { get; set; }
            public IEnumerable<int> OwnedPlacesId { get; set; }
        }

        public View GetView()
        {
            return new View
            {
                UserId = UserId,
                UserName = UserName,
                Email = Email,
                UserRole = UserRole,
                OwnedPlacesId = from place in OwnedPlaces select place.PlaceId
            };
        }
    }
}