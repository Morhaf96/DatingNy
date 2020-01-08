using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuvDating.Models
{
    public class ProfileIndexViewModel
    {
        public string AccountId { get; set; }
        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime Birth { get; set; }

        public string Bio { get; set; }

        public string Image { get; set; }

    }

    public class ProfileFriendsViewModel
    {
        public List<ApplicationUser> FriendList { get; set; }

    }
    public class ProfilePostViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    
        public ProfilePostViewModel() { }

        public class ProfilePostViewModelDto
        {
            public string Message { get; set; }
            public string Timestamp { get; set; }
            public string UserId { get; set; }
            public string UserName { get; set; }
        
            public ProfilePostViewModelDto() { }

            public ProfilePostViewModelDto(ProfilePostViewModel profilePostViewModel)
            {
                Message = profilePostViewModel.Message;
                Timestamp = profilePostViewModel.Timestamp.ToString(@"yyyy-MM-dd HH\:mm\:dd");
                UserId = profilePostViewModel.UserId;
                UserName = profilePostViewModel.User?.UserName;
            }
        }
        
        public ProfilePostViewModel(ProfilePostViewModelDto profilePostViewModelDto)
        {
            Message = profilePostViewModelDto.Message;
            Timestamp = DateTime.Parse(profilePostViewModelDto.Timestamp);
            UserId = profilePostViewModelDto.UserId;

        }
    }
}