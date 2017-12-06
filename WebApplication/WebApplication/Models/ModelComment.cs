using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ModelComment
    {
        [Key]
        public int CommentId { get; set; }
        public string CommentMessage { get; set; }
        public ModelPlace Place { get; set; }
        public ModelUser Author { get; set; }

        public struct View
        {
            public int CommentId;
            public string CommentMessage;
            public int PlaceId;
            public string Author;
        }

        public View GetView()
        {
            return new View
            {
                CommentId = CommentId,
                CommentMessage = CommentMessage,
                PlaceId = Place.PlaceId,
                Author = Author.UserName
            };
        }
    }
}