using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        //[ForeignKey("ChatId")]
        //public virtual Chat Chat { get; set; }
        //[ForeignKey("AppUserId")]
        public virtual MessageUserVM UserAndChat { get; set; }
        public int UserAndChatId { get; set; }
        //public byte[] Image { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}
