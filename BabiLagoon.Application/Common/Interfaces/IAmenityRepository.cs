using BabiLagoon.Application.Common.Interfaces.Base;
using BabiLagoon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Application.Common.Interfaces
{
    public interface IAmenityRepository:IRepository<Amenity>
    {
        Task<Amenity> UpdateAsync(int id, Amenity amenity);
    }
}
