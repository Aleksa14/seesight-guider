using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class ModelPlace
    {
        [Key]
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rate { get; set; } = .0;
        public string Address { get; set; }
        public ModelUser Author { get; set; }
    }
}