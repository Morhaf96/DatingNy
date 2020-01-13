using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuvDating.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ChatMessage() { }
        public ChatMessage(ChatMessageDto chatMessageDto)
        {
            Message = chatMessageDto.Message;
            Timestamp = DateTime.Parse(chatMessageDto.Timestamp);
            UserId = chatMessageDto.UserId;
        }
    }

    public class ChatMessageDto
    {
        public string Message { get; set; }
        public string Timestamp { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public ChatMessageDto() { }
        public ChatMessageDto(ChatMessage chatMessage)
        {
            Message = chatMessage.Message;
            Timestamp = chatMessage.Timestamp.ToString(@"yyyy-MM-dd HH\:mm\:ss");
            UserId = chatMessage.UserId;
            // Använd profilens namn som användarnamn:
            UserName = chatMessage.User?.Name ?? chatMessage.User?.UserName;
        }
    }

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