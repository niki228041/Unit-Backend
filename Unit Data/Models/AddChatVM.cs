using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.Models.Models;

namespace Unit_Data.Models
{
    public class AddChatVM
    {
        public string Name { get; set; }
        public IEnumerable<string> UsersId { get; set; }
    }
}
