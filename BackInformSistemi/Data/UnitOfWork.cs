using BackInformSistemi.Data.Repo;
using BackInformSistemi.Interfaces;

namespace BackInformSistemi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc )
        {
            this.dc = dc;
        }
        public ICityRepository CityRepository => new CityRepository(dc);

        public async Task<bool> SaveAsync()
        {
           return await dc.SaveChangesAsync() > 0;
        }
        public IUserRepository UserRepository => 
            new UserRepository(dc)
            ;

        public IPropertyRepository PropertyRepository => new PropertyRepository(dc);

        public IPropertyTypeRepository PropertyTypeRepository => 
            new PropertyTypeRepository(dc);

        public IFurnishingTypeRepository FurnishingTypeRepository => 
            new FurnishingTypeRepository(dc);
    }
}
