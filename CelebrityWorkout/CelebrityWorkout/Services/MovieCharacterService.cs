using CelebrityWorkout.Data;
using CelebrityWorkout.Interfaces;
using CelebrityWorkout.Models;
using Microsoft.EntityFrameworkCore;

namespace CelebrityWorkout.Services
{
    public class MovieCharacterService : IMovieCharacterService
    {
        private readonly ApplicationDbContext _context;

        public MovieCharacterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovieCharacterDTO>> GetAllAsync()
        {
            return await _context.MovieCharacters
                .Include(mc => mc.Celebrity)
                .Select(mc => new MovieCharacterDTO
                {
                    CharacterId = mc.CharacterId,
                    CharacterName = mc.CharacterName,
                    MovieTitle = mc.MovieTitle,
                    YearOfRelease = mc.YearOfRelease,
                    CelebrityId = mc.CelebrityId,
                    Celebrity = new CelebrityDTO
                    {
                        CelebrityId = mc.Celebrity.CelebrityId,
                        Name = mc.Celebrity.Name
                    }
                }).ToListAsync();
        }

        public async Task<MovieCharacterDTO?> GetByIdAsync(int id)
        {
            var mc = await _context.MovieCharacters
                .Include(mc => mc.Celebrity)
                .FirstOrDefaultAsync(x => x.CharacterId == id);

            if (mc == null) return null;

            return new MovieCharacterDTO
            {
                CharacterId = mc.CharacterId,
                CharacterName = mc.CharacterName,
                MovieTitle = mc.MovieTitle,
                YearOfRelease = mc.YearOfRelease,
                CelebrityId = mc.CelebrityId,
                Celebrity = new CelebrityDTO
                {
                    CelebrityId = mc.Celebrity.CelebrityId,
                    Name = mc.Celebrity.Name
                }
            };
        }

        public async Task<MovieCharacterDTO> CreateAsync(MovieCharacterDTO dto)
        {
            var mc = new MovieCharacter
            {
                CharacterName = dto.CharacterName,
                MovieTitle = dto.MovieTitle,
                YearOfRelease = dto.YearOfRelease,
                CelebrityId = dto.CelebrityId
            };

            _context.MovieCharacters.Add(mc);
            await _context.SaveChangesAsync();
            dto.CharacterId = mc.CharacterId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, MovieCharacterDTO dto)
        {
            var mc = await _context.MovieCharacters.FindAsync(id);
            if (mc == null) return false;

            mc.CharacterName = dto.CharacterName;
            mc.MovieTitle = dto.MovieTitle;
            mc.YearOfRelease = dto.YearOfRelease;
            mc.CelebrityId = dto.CelebrityId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mc = await _context.MovieCharacters.FindAsync(id);
            if (mc == null) return false;

            _context.MovieCharacters.Remove(mc);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
