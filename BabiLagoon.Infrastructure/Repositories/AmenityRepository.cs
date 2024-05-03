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
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AmenityRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Amenity> UpdateAsync(int id, Amenity amenity)
        {
            var existingAmenity = await dbContext.Amenities.FirstOrDefaultAsync(x => x.Id == id);
            if (amenity is null)
            {
                return null;
            }
            existingAmenity.Name = amenity.Name;
            existingAmenity.Description = amenity.Description;
            existingAmenity.VillaId = amenity.VillaId;
            

            await dbContext.SaveChangesAsync();

            return existingAmenity;
        }
    }
}
