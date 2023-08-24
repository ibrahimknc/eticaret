 
#nullable disable

namespace eticaret.Domain.Entities
{
    public partial class Setting : BaseEntitiy
	{ 
        public string title { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public string footerDetail { get; set; }
    }
}
