using LuvDating.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LuvDating.Controllers
{
    public class ChatMessageApiController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatMessageApiController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // /api/chatmessageapi/list
        [HttpGet]
        public IEnumerable<ChatMessageDto> List()
        {
            return _dbContext.ChatMessages
               
                .Include(m => m.User)
                .OrderBy(m => m.Timestamp)
                .ToList()
                .Select(m => new ChatMessageDto(m));
        }

        // /api/chatmessageapi/send
        [HttpPost]
        public string Send([FromBody]ChatMessageDto messageDto)
        {
            try
            {
                var message = new ChatMessage(messageDto);
                _dbContext.ChatMessages.Add(message);
                _dbContext.SaveChanges();
                return "Meddelandet har skickats!";
            }
            catch
            {
                return "Något gick fel!";
            }
        }
    }
}
