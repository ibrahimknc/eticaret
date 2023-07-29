 
using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.Domain.Entities
{
    public partial class Slider : BaseEntitiy
	{ 
        public string title { get; set; }
        public string image { get; set; }
        public int? rank { get; set; } 
    }
}
