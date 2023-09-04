using System;
namespace api.Domain.Entities
{
	public class Category : EntityBase
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public virtual ICollection<Movie> Movies { get; set; } = null!;
	}
}

