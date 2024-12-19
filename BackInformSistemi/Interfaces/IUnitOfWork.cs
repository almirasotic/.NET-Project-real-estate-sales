namespace BackInformSistemi.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICityRepository CityRepository { get; }
        IPropertyRepository PropertyRepository { get; }

        IFurnishingTypeRepository FurnishingTypeRepository { get; }
        IPropertyTypeRepository PropertyTypeRepository { get; }
        Task<bool> SaveAsync();
    }
}
