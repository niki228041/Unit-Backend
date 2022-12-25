using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class AvatarImage
    {
        [Key]
        public int Id { get; set; }
        public string ImageName { get; set; }

        public AppUser User { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; } 
    }
}
