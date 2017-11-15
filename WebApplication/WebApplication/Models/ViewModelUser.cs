using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ViewModelUser
    {
        public ViewModelUser(ModelUser user)
        {
            this.UserId = user.UserId;
            this.UserName = user.UserName;
            this.Email = user.Email;
            this.UserRole = user.UserRole;
            this.OwnedPlacesId = from place in user.OwnedPlaces select place.PlaceId;
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ModelUser.UserRoleType UserRole { get; set; }
        public IEnumerable<int> OwnedPlacesId { get; set; }
    }
}