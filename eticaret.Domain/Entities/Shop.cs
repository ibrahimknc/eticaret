using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eticaret.Domain.Entities
{
    public partial class Shop : BaseEntitiy 
    {
        public string name { get; set; }
        public string image { get; set; }
    }
}
