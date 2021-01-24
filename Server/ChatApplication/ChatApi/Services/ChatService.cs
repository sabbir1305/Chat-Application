using ChatApi.Data;
using ChatApi.Entities;
using ChatApi.Helpers;
using ChatApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public class ChatService : IChatService
    {
        private DataContext _context;

        //Inject Db Context
        public ChatService(DataContext context)
        {
            _context = context;
        }

        public Message Create(Message message)
        {
            // validation
            if (string.IsNullOrWhiteSpace(message.Body))
                throw new AppException("Message is required");
            message.SentDate = DateTime.Now;
            _context.Messages.Add(message);
            _context.SaveChanges();

            return message;
        }

        public void Delete(int id)
        {
            var message = _context.Messages.Find(id);
            if (message != null)
            {
                message.IsDeleted = true;
                //_context.Messages.Remove(message);
                _context.SaveChanges();
            }
        }
        public void DeleteReceiverAsync(int id)
        {
            var message = _context.Messages.Find(id);
            if (message != null)
            {
                message.IsDeletedByReceiver = true;
                //_context.Messages.Remove(message);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Message> GetAll(int senderId, int receiverId)
        {
            return _context.Messages.Where(x=>x.IsDeleted==false)
                .OrderBy(x=>x.SentDate)
                .Where(x=> (x.ReceiverId==receiverId && x.SenderId==senderId) || (x.SenderId==receiverId && x.ReceiverId==senderId));
        }
    }
}
