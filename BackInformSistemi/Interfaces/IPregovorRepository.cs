using BackInformSistemi.Models;

namespace BackInformSistemi.Interfaces
{
    public interface IPregovorRepository
    {
        ICollection<Pregovor> GetAllPregovori();
        Pregovor GetPregovorById(int id);

        

        bool CreatePregovor(Pregovor pregovor);
        bool UpdatePregovor(Pregovor pregovor);
        bool DeletePregovor(Pregovor pregovor);
        bool Save();
        ICollection<Pregovor> GetPregovoriByProperty(int propertyId);
    }
}
