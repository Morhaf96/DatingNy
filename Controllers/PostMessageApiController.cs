using LuvDating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Http;

using static LuDating.Models.PostModel;
using LuDating.Models;

namespace LuDating.Controllers
{
    public class PostMessageApiController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public PostMessageApiController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // /api/postmessage/list
        [HttpGet]
        public IEnumerable<PostModelDto> List()
        {
            return _dbContext.ProfilePosts
                .Include(m => m.User)
                .OrderBy(m => m.Timestamp)
                .ToList()
                .Select(m => new PostModelDto(m));
        }
        // /api/postmessage/send
        [HttpPost]
        public string Send([FromBody]PostModelDto messageDto)
        {
            try
            {
                var message = new PostModel(messageDto);
                _dbContext.ProfilePosts.Add(message);
                _dbContext.SaveChanges();
                return "Meddelandet har skickats.";
            }
            catch
            {
                return "Meddelandet kunde inte skickas.";
            }
        }
    }
}