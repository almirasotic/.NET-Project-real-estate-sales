using BackInformSistemi.Interfaces;
using BackInformSistemi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackInformSistemi.Data.Repo
{
    public class PregovorRepository : IPregovorRepository
    {
        private readonly DataContext context;

        public PregovorRepository(DataContext context)
        {
            this.context = context;
        }

        // Dohvatanje svih pregovora
        public ICollection<Pregovor> GetAllPregovori()
        {
            return context.Pregovori.ToList();
        }

        // Dohvatanje pregovora po ID-u
        public Pregovor GetPregovorById(int id)
        {
            return context.Pregovori.FirstOrDefault(p => p.Id == id);
        }

        // Kreiranje novog pregovora
        public bool CreatePregovor(Pregovor pregovor)
        {
            try
            {
                context.Pregovori.Add(pregovor);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom kreiranja pregovora: {ex.Message}");
                throw;
            }
        }

        // Ažuriranje postojećeg pregovora
        public bool UpdatePregovor(Pregovor pregovor)
        {
            try
            {
                context.Pregovori.Update(pregovor);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom ažuriranja pregovora: {ex.Message}");
                throw;
            }
        }

        // Brisanje pregovora
        public bool DeletePregovor(Pregovor pregovor)
        {
            try
            {
                context.Pregovori.Remove(pregovor);
                return Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška prilikom brisanja pregovora: {ex.Message}");
                throw;
            }
        }

        // Metoda za čuvanje izmena u bazi
        public bool Save()
        {
            try
            {
                var saved = context.SaveChanges();
                return saved > 0;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Greška pri čuvanju u bazi: {dbEx.InnerException?.Message ?? dbEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Neočekivana greška pri čuvanju u bazi: {ex.Message}");
                throw;
            }
        }


        // Dohvatanje pregovora za određeni property ID
        public ICollection<Pregovor> GetPregovoriByProperty(int propertyId)
        {
            Console.WriteLine($"Dohvatam pregovore za propertyId: {propertyId}");
            var pregovori = context.Pregovori
                .Where(p => p.propertyId == propertyId)
                .ToList();
            Console.WriteLine($"Pronađeno pregovora: {pregovori.Count}");
            return pregovori;
        }

      
    }
}
