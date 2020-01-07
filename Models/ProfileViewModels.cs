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
}