using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }
        public int Stars { get; set; } //max 5 min 1
        public byte[] Photos { get; set; }

        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}
