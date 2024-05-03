using BabiLagoon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BabiLagoon.Application.Common.DTOs
{
    public class AmenityDto
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }
        public string? Description { get; set; }


        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        [ValidateNever]
        public VillaDto Villa { get; set; }
    }
}
