using BackInformSistemi.Interfaces;
using BackInformSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackInformSistemi.Data.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext dc;

        public PropertyRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public async Task<bool> AddProperty(Property property)
        {
            dc.Properties.Add(property);
            await dc.SaveChangesAsync();
            return true;
        }

        public void Delete(Property property)
        {
            dc.Properties.Remove(property);
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            return await dc.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Include(p => p.User)
                .Where(p => p.SellRent == sellRent)
                .ToListAsync();
        }

        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            return await dc.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            return await dc.Properties
                .Include(p => p.PropertyType)
                .Include(p => p.City)
                .Include(p => p.FurnishingType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public  void DeleteProperty(int id)
        {
            var property = dc.Properties.Find(id);
            if (property != null)
            {
                List<Sale> sales = dc.Sales.Where(s => s.propertyId == property.Id).ToList();
                dc.Sales.RemoveRange(sales);
                dc.Properties.Remove(property);
                dc.SaveChanges();
            }
            else
            {
                throw new Exception($"Property with ID {id} not found."); // Dodajte informativnu grešku
            }
        }

    }
}
