using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LuvDating.Models
{
    public class FriendModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FriendId { get; set; }
        public string FriendRequestReciever { get; set; }

        public int pendingRequest { get; set; }

        public string Name { get; set; }

        [ForeignKey("Users")]
        public virtual ICollection<ApplicationUser> Sender { get; set; }

        public FriendModel()
        {
            this.Sender = new HashSet<ApplicationUser>();
        }
    }
}