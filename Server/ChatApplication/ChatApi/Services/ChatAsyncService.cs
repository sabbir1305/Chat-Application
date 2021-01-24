using ChatApi.Data;
using ChatApi.Entities;
using ChatApi.Helpers;
using ChatApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services
{
    public class ChatAsyncService : IChatAsyncService
    {
        private DataContext _context;

        //Inject Db Context
        public ChatAsyncService(DataContext context)
        {
            _context = context;
        }
        public async Task<Message> CreateAsync(Message message)
        {
            // validation
            if (string.IsNullOrWhiteSpace(message.Body))
                throw new AppException("Message is required");
            message.SentDate = DateTime.Now;
            _context.Messages.Add(message);
         await   _context.SaveChangesAsync();

            return   message;
        }

        public async Task DeleteAllAsync(int senderId, int receiverId)
        {
         var allSentMessage= await  _context.Messages
               .OrderBy(x => x.SentDate)
               .Where(x => (x.ReceiverId == receiverId && x.SenderId == senderId && !x.IsDeleted) ).ToListAsync();
            foreach (var item in allSentMessage)
            {
                item.IsDeleted = true;
            }

            var allReceivedMessage = await _context.Messages
              .OrderBy(x => x.SentDate)
              .Where(x => (x.SenderId == receiverId && x.ReceiverId == senderId && !x.IsDeletedByReceiver)).ToListAsync();
            foreach (var item in allReceivedMessage)
            {
                item.IsDeletedByReceiver = true;
            }
            await _context.SaveChangesAsync();
        }

       
        public async Task DeleteAsync(int id)
        {
            var message = _context.Messages.Find(id);
            if (message != null)
            {
                message.IsDeleted = true;
                //_context.Messages.Remove(message);
               await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteReceiverAsync(int id)
        {
            var message = _context.Messages.Find(id);
            if (message != null)
            {
                message.IsDeletedByReceiver = true;
                //_context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Message>> GetAllAsync(int senderId, int receiverId)
        {
            return await _context.Messages
                //.Where(x => x.IsDeleted == false)
               .OrderBy(x => x.SentDate)
               .Where(x => (x.ReceiverId == receiverId && x.SenderId == senderId && !x.IsDeleted) || (x.SenderId == receiverId && x.ReceiverId == senderId && !x.IsDeletedByReceiver)).ToListAsync();
        }
    }
}
