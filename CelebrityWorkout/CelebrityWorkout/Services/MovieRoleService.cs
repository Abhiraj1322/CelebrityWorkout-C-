using CelebrityWorkout.Data;
using CelebrityWorkout.Interfaces;
using CelebrityWorkout.Models;
using Microsoft.EntityFrameworkCore;

namespace CelebrityWorkout.Services
{
    public class MovieRoleService : IMovieRoleService
    {
        private readonly ApplicationDbContext _context;

        public MovieRoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovieRoleDTO>> GetAllAsync()
        {
            return await _context.MovieRoles
                .Include(m => m.Celebrity)
                .Select(m => new MovieRoleDTO
                {
                    MovieRoleId = m.MovieRoleId,
                    MovieTitle = m.MovieTitle,
                    Role = m.Role,
                    YearOfRelease = m.YearOfRelease,
                    CelebrityId = m.CelebrityId,
                    Celebrity = new CelebrityDTO
                    {
                        CelebrityId = m.Celebrity.CelebrityId,
                        Name = m.Celebrity.Name
                    }
                }).ToListAsync();
        }

        public async Task<MovieRoleDTO?> GetByIdAsync(int id)
        {
            var m = await _context.MovieRoles.Include(r => r.Celebrity).FirstOrDefaultAsync(x => x.MovieRoleId == id);
            if (m == null) return null;

            return new MovieRoleDTO
            {
                MovieRoleId = m.MovieRoleId,
                MovieTitle = m.MovieTitle,
                Role = m.Role,
                YearOfRelease = m.YearOfRelease,
                CelebrityId = m.CelebrityId,
                Celebrity = new CelebrityDTO
                {
                    CelebrityId = m.Celebrity.CelebrityId,
                    Name = m.Celebrity.Name
                }
            };
        }

        public async Task<MovieRoleDTO> CreateAsync(MovieRoleDTO dto)
        {
            var m = new MovieRole
            {
                MovieTitle = dto.MovieTitle,
                Role = dto.Role,
                YearOfRelease = dto.YearOfRelease,
                CelebrityId = dto.CelebrityId
            };

            _context.MovieRoles.Add(m);
            await _context.SaveChangesAsync();
            dto.MovieRoleId = m.MovieRoleId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, MovieRoleDTO dto)
        {
            var m = await _context.MovieRoles.FindAsync(id);
            if (m == null) return false;

            m.MovieTitle = dto.MovieTitle;
            m.Role = dto.Role;
            m.YearOfRelease = dto.YearOfRelease;
            m.CelebrityId = dto.CelebrityId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var m = await _context.MovieRoles.FindAsync(id);
            if (m == null) return false;

            _context.MovieRoles.Remove(m);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
