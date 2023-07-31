using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eticaret.Domain.Entities
{
	public abstract class BaseEntitiy
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		[Column(Order = 1)] 
        public Guid id { get; set; } = Guid.NewGuid();
		public bool isActive { get; set; }
		public DateTime creatingTime { get; set; } = DateTime.UtcNow;
		public DateTime? updatedTime { get; set; }
	}
}
