using BackInformSistemi.Interfaces;
using BackInformSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackInformSistemi.Data.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext dc;

        public PropertyRepository(DataContext dc )
        {
            this.dc = dc;
        }
        public async Task<bool> AddProperty(Property property)
        {
            dc.Properties.Add(property);
            await dc.SaveChangesAsync();
            return true;
        }

        public void DeleteProperty(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            var properties = await dc.Properties
                .Include(p => p.PropertyType)

                .Include(p => p.City)

                .Include(p => p.FurnishingType)
                .Where(p => p.SellRent == sellRent).Include( p => p.User)
                .ToListAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            var properties = await dc.Properties
                 .Include(p => p.PropertyType)

                 .Include(p => p.City)

                 .Include(p => p.FurnishingType)
                 .Where(p => p.Id == id).Include(p => p.User)
                  .FirstOrDefaultAsync();
            return properties;
        }
    }
}
