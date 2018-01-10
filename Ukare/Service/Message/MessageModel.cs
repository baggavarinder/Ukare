using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ukare.Service.Message
{
    public class MessageModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public int MessageTypeId { get; set; }
        [Required(ErrorMessage = "User name is required.")]
        public int? MessageFor { get; set; }

        public string SentTo { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}