using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class AppUserAppUserContact
    {
        public int AppUserAppUserContactId { get; set; }
        public AppUser User { get; set; }
        public string UserId { get; set; }

        public AppUser Contact { get; set; }
        public int ContactId { get; set; }
    }
}
