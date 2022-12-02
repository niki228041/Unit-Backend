using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.Models.Models;

namespace Unit_Data.Models
{
    public class MessageVM
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}
