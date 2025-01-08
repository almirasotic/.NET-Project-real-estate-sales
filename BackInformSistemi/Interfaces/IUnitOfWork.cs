namespace BackInformSistemi.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICityRepository CityRepository { get; }
        IPropertyRepository PropertyRepository { get; }

        IFurnishingTypeRepository FurnishingTypeRepository { get; }
        IPropertyTypeRepository PropertyTypeRepository { get; }
        ISalesRepository SalesRepository { get; }
        Task<bool> SaveAsync();
    }
}
