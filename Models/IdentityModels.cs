using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using LuDating.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LuvDating.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //Lägger in nya värden.
        public string Name { get; set; }

        public string Gender { get; set; }

        [DisplayName("Birth date")]
        public DateTime BirthDate { get; set; }

        public string Bio { get; set; }

        [DisplayName("Image")]
        public string ImageName { get; set; }
        public virtual ICollection<PostModel> Messages { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<FriendModel> FriendList { get; set; }


        public ApplicationUser()
        {
            this.FriendList = new HashSet<FriendModel>();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<FriendModel> FriendModels { get; set; }
        public DbSet<PostModel> ProfilePosts { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    public class ConnectContext : ApplicationDbContext
    {
        public DbSet<ApplicationDbContext> Student { get; set; }
    }
}