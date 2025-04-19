using CelebrityWorkout.Data;
using CelebrityWorkout.Interfaces;
using CelebrityWorkout.Models;
using Microsoft.EntityFrameworkCore;

namespace CelebrityWorkout.Services
{
    public class CelebrityService : ICelebrityService
    {
        private readonly ApplicationDbContext _context;

        public CelebrityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CelebrityDTO>> GetAllAsync()
        {
            return await _context.Celebrities
                .Select(c => new CelebrityDTO
                {
                    CelebrityId = c.CelebrityId,
                    Name = c.Name,
                    Biography = c.Biography,
                    Profession = c.Profession
                }).ToListAsync();
        }

        public async Task<CelebrityDTO?> GetByIdAsync(int id)
        {
            var celeb = await _context.Celebrities.FindAsync(id);
            if (celeb == null) return null;

            return new CelebrityDTO
            {
                CelebrityId = celeb.CelebrityId,
                Name = celeb.Name,
                Biography = celeb.Biography,
                Profession = celeb.Profession
            };
        }

        public async Task<CelebrityDTO> CreateAsync(CelebrityDTO dto)
        {
            var celeb = new Celebrity
            {
                Name = dto.Name,
                Biography = dto.Biography,
                Profession = dto.Profession
            };
            _context.Celebrities.Add(celeb);
            await _context.SaveChangesAsync();

            dto.CelebrityId = celeb.CelebrityId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, CelebrityDTO dto)
        {
            var celeb = await _context.Celebrities.FindAsync(id);
            if (celeb == null) return false;

            celeb.Name = dto.Name;
            celeb.Biography = dto.Biography;
            celeb.Profession = dto.Profession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var celeb = await _context.Celebrities.FindAsync(id);
            if (celeb == null) return false;

            _context.Celebrities.Remove(celeb);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
