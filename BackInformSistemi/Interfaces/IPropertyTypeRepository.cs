using BackInformSistemi.Models;

namespace BackInformSistemi.Interfaces
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();

    }
}
