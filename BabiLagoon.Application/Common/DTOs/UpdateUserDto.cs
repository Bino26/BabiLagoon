using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must contains 3 characters")]
        public string Username { get; set; }
    }
}
