using LuvDating.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LuDating.Models
{
    public class PostModel
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public PostModel() { }

        public class PostModelDto
        {
            public string Message { get; set; }
            public string Timestamp { get; set; }
            public string UserId { get; set; }
            public string UserName { get; set; }

            public PostModelDto() { }

            public PostModelDto(PostModel profilePostViewModel)
            {
                Message = profilePostViewModel.Message;
                Timestamp = profilePostViewModel.Timestamp.ToString(@"yyyy-MM-dd HH\:mm\:dd");
                UserId = profilePostViewModel.UserId;
                UserName = profilePostViewModel.User?.UserName;
            }
        }

        public PostModel(PostModelDto profilePostViewModelDto)
        {
            Message = profilePostViewModelDto.Message;
            Timestamp = DateTime.Parse(profilePostViewModelDto.Timestamp);
            UserId = profilePostViewModelDto.UserId;

        }
    }
}