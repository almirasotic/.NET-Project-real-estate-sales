using BackInformSistemi.Models;

namespace BackInformSistemi.Interfaces
{
    public interface ICityRepository
    {
        Task<City> GetCityByIdAsync(int id);
        void UpdateCity(City city);
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        void DeleteCity(int id);
        Task<City> FindCity(int id);
    }
}
