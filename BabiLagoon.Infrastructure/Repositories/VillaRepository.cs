using BabiLagoon.Application.Common.Interfaces;
using BabiLagoon.Domain.Entities;
using BabiLagoon.Infrastructure.Data;
using BabiLagoon.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabiLagoon.Infrastructure.Repositories
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext dbContext;

        public VillaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Villa> UpdateAsync( int id, Villa villa)
        {
            var existingVilla = await dbContext.Villas.FirstOrDefaultAsync(x => x.Id == id);
            if (villa is null)
            {
                return null;
            }
            existingVilla.Name = villa.Name;
            existingVilla.Description = villa.Description;
            existingVilla.Price = villa.Price;
            existingVilla.Occupancy = villa.Occupancy;
            existingVilla.ImageUrl = villa.ImageUrl;

            existingVilla.Sqft = villa.Sqft;
            existingVilla.Updated_Date = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return existingVilla;

        }
    }

}
