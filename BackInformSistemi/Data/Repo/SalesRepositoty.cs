using BackInformSistemi.Interfaces;
using BackInformSistemi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackInformSistemi.Data
{
    public class SalesRepository : ISalesRepository
    {
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private readonly DataContext _context;

        public SalesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sale>> GetSalesAsync()
        {
            return await _context.Sales
                .Include(s => s.Seller)
                .Include(s => s.Buyer)
                .Include(s => s.Agent)
                .Include(s => s.Property)
                .ToListAsync();
        }

        public async Task AddSale(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.Seller)
                .Include(s => s.Buyer)
                .Include(s => s.Agent)
                .Include(s => s.Property)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public void DeleteSale(Sale sale)
        {
            _context.Sales.Remove(sale);
        }
    }
}
