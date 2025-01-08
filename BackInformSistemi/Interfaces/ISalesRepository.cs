using BackInformSistemi.Models;

namespace BackInformSistemi.Interfaces
{
    public interface ISalesRepository
    {
        Task<IEnumerable<Sale>> GetSalesAsync();
        Task AddSale(Sale sale);
        Task<Sale> GetSaleByIdAsync(int id);
        void DeleteSale(Sale sale);
        Task<bool> SaveAllAsync();
    }
}
