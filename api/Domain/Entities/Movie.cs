using System;
namespace api.Domain.Entities
{
	public class Movie : EntityBase
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public double Rating { get; set; }
		public int Duration { get; set; }
		public virtual Director Director { get; set; } = null!;
		public int DirectorId { get; set; }
		public virtual Category Category { get; set; } = null!;
		public int CategoryId { get; set; }
		public bool IsFavourite { get; set; }
		public bool IsOnWatchlist { get; set; }
		public bool Watched { get; set; }
	}
}

