using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.DTOs
{
    public class UpdateVillaDto
    {
        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Range(10, 10000)]
        public double Price { get; set; }
        public int Sqft { get; set; }
        [Range(1, 10)]
        public int Occupancy { get; set; }
        [NotMapped]
        //public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? Updated_Date { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public bool IsAvailable { get; set; }
    }
}
