using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackInformSistemi.Models;
using System.Threading.Tasks;
using BackInformSistemi.Data;
using Microsoft.AspNetCore.Authorization;

namespace BackInformSistemi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly DataContext _context;

        public SalesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _context.Sales
                .Include(s => s.Seller)
                .Include(s => s.Buyer)
                .Include(s => s.Agent)
                .Include(s => s.Property)
                .ToListAsync();

            return Ok(sales);
        }

        // POST: api/Sales
        [HttpPost]
        public async Task<IActionResult> AddSale([FromBody] Sale sale)
        {
            if (sale == null)
            {
                return BadRequest("Invalid sale data.");
            }

            try
            {
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                return Ok("Sale added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound("Sale not found.");
            }

            try
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
                return Ok("Sale deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
