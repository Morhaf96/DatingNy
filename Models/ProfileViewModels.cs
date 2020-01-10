using LuDating.Models;
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

    public class ProfileEditViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        //[Required(ErrorMessage = "Please enter a new email")]
        public string Email { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter your gender")]
        public string Gender { get; set; }
       
        [Display(Name = "Date of birth (MM/DD/YYYY)")]
        [Required(ErrorMessage = "Please enter your birthdate")]
        [DataType(DataType.Date)]
        public DateTime Birth { get; set; }
        [Display(Name = "Bio")]
        public string Bio { get; set; }
    }
    
    public class SenderListModel
    {
        public List<ApplicationUser> Requests { get; set; }
    }
    
}