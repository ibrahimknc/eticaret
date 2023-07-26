using System;
using System.Collections.Generic;

#nullable disable

namespace eticaret.DLL.Models
{
    public partial class user
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool isActive { get; set; }
        public DateTime creatingDate { get; set; }
        public DateTime? lastLoginDate { get; set; }
    }
}
