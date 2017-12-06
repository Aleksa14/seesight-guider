using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication.Models
{
    public class ModelRate
    {
        [Key]
        public int RateId { get; set; }
        public int Rate { get; set; }
        public ModelPlace RatedPlace { get; set; }

        public struct View
        {
            public int RateId { get; set; }
            public int Rate { get; set; }
            public int PlaceId { get; set; }
        }

        public View GetView()
        {
            return new View
            {
                RateId = RateId,
                Rate = Rate,
                PlaceId = RatedPlace.PlaceId
            };
        }
    }
}