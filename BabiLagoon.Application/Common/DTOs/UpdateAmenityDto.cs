using BabiLagoon.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.DTOs
{
    public class UpdateAmenityDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public int VillaId { get; set; }
        
    }
}
