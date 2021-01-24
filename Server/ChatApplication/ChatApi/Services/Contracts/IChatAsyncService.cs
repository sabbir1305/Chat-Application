using ChatApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services.Contracts
{
   public interface IChatAsyncService
    {
        Task<IEnumerable<Message>> GetAllAsync(int senderId, int receiverId);

        Task<Message> CreateAsync(Message message);
        Task DeleteAsync(int id);
        Task DeleteReceiverAsync(int id);

        Task DeleteAllAsync(int senderId, int receiverId);
    }
}
