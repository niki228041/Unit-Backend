using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public DateTime Created { get; set; }

        //[ForeignKey("AppUserId")]
        //public AppUser User { get; set; }
        [ForeignKey(nameof(Product))]
        public ICollection<Product> Products { get; set; }
    }
}
