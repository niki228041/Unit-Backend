using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public IList<Post> Posts { get; set; }
        public IList<Chat> Chats { get; set; }
        public IList<Order> Orders { get; set; }
        public List<AppUserContact> ContactUsers { get; set; }
        public DateTime Birthday { get; set; }
    }
}
