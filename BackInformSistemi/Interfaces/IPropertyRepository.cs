using BackInformSistemi.Models;

namespace BackInformSistemi.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent);

        Task<Property> GetPropertyDetailAsync(int id);
        Task<bool> AddProperty(Property property);

        void DeleteProperty(int id);

    }
}
