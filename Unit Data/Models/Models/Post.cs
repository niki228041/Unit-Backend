using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(AppUser))]
        public AppUser User { get; set; }
        public byte[] Photos { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public int Views { get; set; }

    }
}
