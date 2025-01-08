using BackInformSistemi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BackInformSistemi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<FurnishingType> FurnishingTypes { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Facture> Factures { get; set; }

        public DbSet<Pregovor> Pregovori { get; set; }
    }
}
