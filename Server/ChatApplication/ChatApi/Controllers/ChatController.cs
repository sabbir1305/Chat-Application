using AutoMapper;
using ChatApi.Entities;
using ChatApi.Helpers;
using ChatApi.Models;
using ChatApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApi.Controllers
{
    [Authorize]
    [Route("api/Chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IChatService _chatService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ChatController(IChatService chatService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _chatService = chatService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{senderId}/{receiverId}")]
        public ActionResult<IEnumerable<MessageDisplayModel>> GetChat(int senderId, int receiverId)
        {
            
            var chatList = _chatService.GetAll(senderId, receiverId);
            if (chatList == null)
                return NotFound();

            return Ok(_mapper.Map<IList<MessageDisplayModel>>(chatList));
        }


        [HttpPost("create")]
        public IActionResult Create( [FromBody] MessageModel model)
        {
            // map model to entity
            
            var message = _mapper.Map<Message>(model);
       

            try
            {
                // create user
                
                _chatService.Create(message);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _chatService.Delete(id);
            return Ok();
        }

   

    }
}
