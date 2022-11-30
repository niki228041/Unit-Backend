using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.Models.Models;

namespace Unit_Data.Models
{
    public class AppUserVM
    {
        [Key]
        public int id { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(Chat))]
        public int ChatId { get; set; }
    }
}
