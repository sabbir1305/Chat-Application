using ChatApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Services.Contracts
{
   public interface IChatService
    {

        IEnumerable<Message> GetAll(int senderId,int receiverId);

        Message Create(Message message);
        void Delete(int id);
    }
}
