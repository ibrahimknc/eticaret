 
using System; 

#nullable disable

namespace eticaret.Domain.Entities
{
    public partial class Log : BaseEntitiy
	{ 
        public string userID { get; set; }
        public int type { get; set; } 
        public string ip { get; set; }
        public string note { get; set; }

        public LogType LogType { get; set; }
    }
}
