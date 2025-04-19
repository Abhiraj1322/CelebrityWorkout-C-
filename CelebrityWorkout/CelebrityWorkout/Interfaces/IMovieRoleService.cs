using CelebrityWorkout.Models;

namespace CelebrityWorkout.Interfaces
{
    public interface IMovieRoleService
    {
        Task<List<MovieRoleDTO>> GetAllAsync();
        Task<MovieRoleDTO?> GetByIdAsync(int id);
        Task<MovieRoleDTO> CreateAsync(MovieRoleDTO dto);
        Task<bool> UpdateAsync(int id, MovieRoleDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
