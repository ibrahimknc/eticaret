 
using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.Domain.Entities
{
    public partial class User : BaseEntitiy
	{ 
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; } 
		public DateTime? lastLoginDate { get; set; }
    }
}
