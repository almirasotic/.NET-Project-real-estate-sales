using BackInformSistemi.Interfaces;
using BackInformSistemi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BackInformSistemi.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;

        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public async Task<User> Authenticate(string userName, string password)
        {
            // Pronađi korisnika po korisničkom imenu
            var user = await dc.Users.FirstOrDefaultAsync(x => x.Username == userName);

            if (user is null || user.PasswordKey== null )
                return null;

            // Proveri da li lozinka odgovara hash-u u bazi
            if (!MatchPasswordHash(password, user.Password, user.PasswordKey))
                return null;

            return user;
        }

        private bool MatchPasswordHash(string password, byte[] storedPasswordHash, byte[] storedPasswordKey)
        {
            // Kreiraj HMAC sa ključem iz baze
            using (var hmac = new HMACSHA512(storedPasswordKey))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Poredi svaki bajt hash-a
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedPasswordHash[i])
                        return false;
                }
                return true;
            }
        }

        public void Register(string userName, string password)
        {
            byte[] passwordHash, passwordKey;

            // Generiši hash i ključ za lozinku
            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            // Kreiraj korisnika
            User user = new User
            {
                Username = userName,
                Password = passwordHash,
                PasswordKey = passwordKey
            };

            dc.Users.Add(user);
            dc.SaveChanges(); // Sačuvaj promene u bazi
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await dc.Users.AnyAsync(x => x.Username == userName);
        }

        public async Task<bool> UpdateToManager(int id)
        {
                User? u = await dc.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
                if( u != null)
                {
                u.role = 2;
                await dc.SaveChangesAsync();
                return true;    
            }
            return false;
            
        }

        public async Task<bool> UpdateToAdministrator(int id)
        {

            User? u = await dc.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (u != null)
            {
                u.role = 3;
                await dc.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> UpdateToAgent(int id)
        {

            User? u = await dc.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (u != null)
            {
                u.role = 1;
                await dc.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DemoteToBuyer(int id)
        {

            User? u = await dc.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (u != null)
            {
                u.role = 0;
                await dc.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await dc.Users.ToListAsync();
        }




        public async Task<User?> GetUserById(int userId)
        {
            return await dc.Users.FindAsync(userId);
        }
        public void DeleteUser(User user)
        {
            dc.Users.Remove(user);
        }

    }
}
