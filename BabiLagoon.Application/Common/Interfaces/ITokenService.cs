using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.Interfaces
{
    public  interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(string userName, IList<string> roles);
    }
}
