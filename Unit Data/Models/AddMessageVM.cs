using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.Models.Models;

namespace Unit_Data.Models
{
    public class AddMessageVM
    {
        public string UserId { get; set; }
        public int ChatId { get; set; }
        //public byte[] Image { get; set; }
        public string Text { get; set; }
    }
}
