using BabiLagoon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.DTOs
{
    public class CreateAmenityDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int VillaId { get; set; }
       
    }
}
