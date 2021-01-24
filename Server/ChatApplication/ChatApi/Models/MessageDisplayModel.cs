using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Models
{
    public class MessageDisplayModel
    {
        public int Id { get; set; }
        public DateTime SentDate { get; set; }
        public string Body { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
