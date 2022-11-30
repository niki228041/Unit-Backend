using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.Models.Models;

namespace Unit_Data.Models
{
    public class MessageResponse
    {
        public Message message { get; set; }
        public string userId { get; set; }
    }
}
