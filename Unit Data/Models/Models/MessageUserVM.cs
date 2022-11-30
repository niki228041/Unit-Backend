﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class MessageUserVM
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ChatId { get; set; }
    }
}
