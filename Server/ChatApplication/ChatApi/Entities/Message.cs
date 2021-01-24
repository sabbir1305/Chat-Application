using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Entities
{
    public class Message : BaseEntity
    {
 

        public DateTime SentDate { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// IsDeleted = true
        /// </summary>
        public bool IsDeleted { get; set; }
        public bool IsDeletedByReceiver { get; set; }

        public int SenderId { get; set; }

   
        public int ReceiverId { get; set; }

    }
}
