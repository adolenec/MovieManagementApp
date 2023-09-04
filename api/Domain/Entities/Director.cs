using System;
namespace api.Domain.Entities
{
	public class Director : EntityBase
	{
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
		public string Description { get; set; } = string.Empty;
		public virtual ICollection<Movie> Movies { get; set; } = null!;
	}
}

