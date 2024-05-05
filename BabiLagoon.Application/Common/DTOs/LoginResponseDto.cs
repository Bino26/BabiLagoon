using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.DTOs
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string JwtToken { get; set; }
    }
}
