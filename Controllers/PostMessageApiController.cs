using LuvDating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using static LuvDating.Models.ProfilePostViewModel;

namespace LuDating.Controllers
{
    public class PostMessageApiController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public PostMessageApiController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpGet]
        public IEnumerable<ProfilePostViewModelDto> List()
        {
            return _dbContext.ProfilePosts
                .Include(m => m.User)
                .OrderBy(m => m.Timestamp)
                .ToList()
                .Select(m => new ProfilePostViewModelDto(m));
        }
        [HttpPost]
        public string Send([FromBody]ProfilePostViewModelDto messageDto)
        {
            try
            {
                var message = new ProfilePostViewModel(messageDto);
                _dbContext.ProfilePosts.Add(message);
                _dbContext.SaveChanges();
                return "Meddelandet har skickats.";
            }
            catch
            {
                return "Inte ok";
            }
        }
    }
}