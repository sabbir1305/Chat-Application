using AutoMapper;
using ChatApi.Entities;
using ChatApi.Helpers;
using ChatApi.Hubs;
using ChatApi.Models;
using ChatApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Controllers
{
    [Authorize]
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        private readonly IChatAsyncService Respository;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        private IMapper _mapper;


        public MessagesController(
            IChatAsyncService Respository,
            IHubContext<BroadcastHub, IHubClient> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;
            this.Respository = Respository;
            _mapper = mapper;
            
          
        }

        [HttpGet("{senderId}/{receiverId}")]
        public async Task<IEnumerable<Message>> Get(int senderId, int receiverId)
        {
            var messages = await Respository.GetAllAsync(senderId, receiverId);
            return messages;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] MessageModel model)
        {
            // map model to entity

            var message = _mapper.Map<Message>(model);


            try
            {
                // create user

               await Respository.CreateAsync(message);
                await _hubContext.Clients.All.BroadcastMessage();
                //await _hubContext.Clients.Client(HttpContext.Connection.Id).BroadcastMessage();
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           await Respository.DeleteAsync(id);
            await _hubContext.Clients.All.BroadcastMessage();
            return Ok();
        }

        [HttpDelete("ReceiverDelete/{id}")]
        public async Task<IActionResult> ReceiverDelete(int id)
        {
            await Respository.DeleteReceiverAsync(id);
            await _hubContext.Clients.All.BroadcastMessage();
            return Ok();
        }
        [HttpDelete("{senderId}/{receiverId}")]
        public async Task<IActionResult> DeleteAll(int senderId, int receiverId)
        {
            await Respository.DeleteAllAsync(senderId,receiverId);
            await _hubContext.Clients.All.BroadcastMessage();
            return Ok();
        }
    }
}
