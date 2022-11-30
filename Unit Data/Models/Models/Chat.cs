using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey(nameof(Message))]
        public virtual ICollection<Message> Messages { get; set; }
        //[ForeignKey(nameof(AppUser))]
        public virtual ICollection<AppUserVM> Users { get; set; }
    }
}
