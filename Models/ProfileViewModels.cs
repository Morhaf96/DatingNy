using LuDating.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [RegularExpression("^([a-zA-Zåäö]{2,}\\s[a-zA-zåäö]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Zåäö]{1,})?)", ErrorMessage = "Write a correct first and last name!")]
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter your gender")]
        public string Gender { get; set; }
       
        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Please enter your birthdate")]
        [DateMinimumAge(18)]
        [DateMaximumAge(100)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birth { get; set; }
        
        [Display(Name = "Bio (Optional)")]
        public string Bio { get; set; }
    }
    
    public class SenderListModel
    {
        public List<ApplicationUser> RequestsFrom { get; set; }
    }


    //Validerings klasser för ålder.
    public class DateMinimumAgeAttribute : ValidationAttribute
    {
        public DateMinimumAgeAttribute(int minimumAge)
        {
            MinimumAge = minimumAge;
            ErrorMessage = "{0} must be someone at least {1} years of age";
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if ((value != null && DateTime.TryParse(value.ToString(), out date)))
            {
                return date.AddYears(MinimumAge) < DateTime.Now;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, MinimumAge);
        }

        public int MinimumAge { get; }
    }

    public class DateMaximumAgeAttribute : ValidationAttribute
    {
        public DateMaximumAgeAttribute(int maximumAge)
        {
            MaximumAge = maximumAge;
            ErrorMessage = "{0} can't be over {1} years of age";
        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if ((value != null && DateTime.TryParse(value.ToString(), out date)))
            {
                return date.AddYears(MaximumAge) > DateTime.Now;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, MaximumAge);
        }

        public int MaximumAge { get; }
    }


}