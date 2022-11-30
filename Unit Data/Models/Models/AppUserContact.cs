using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class AppUserContact
    {
        public int Id { get; set; }

        public AppUser User { get; set; }
        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }
        public string ContactId { get; set; }
    }
}
