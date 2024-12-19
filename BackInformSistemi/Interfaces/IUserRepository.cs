using BackInformSistemi.Models;

namespace BackInformSistemi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
        void Register (string userName, string password);
        Task<bool> UserAlreadyExists(string userName);
        Task<List<User>> GetAllUsers();
        Task<bool> UpdateToManager(int id);
        Task<bool> UpdateToAdministrator(int id);
        Task<bool> UpdateToAgent(int id);
        Task<bool> DemoteToBuyer(int id);


    }
}
