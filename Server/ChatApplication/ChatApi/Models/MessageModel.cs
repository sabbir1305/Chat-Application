using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Models
{
    public class MessageModel
    {
        [Required]
        public int ReceiverId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required(ErrorMessage ="Message is required")]
        [StringLength(1000,ErrorMessage ="Maximum message size can be 1000")]
        public string Body { get; set; }
    }
}
