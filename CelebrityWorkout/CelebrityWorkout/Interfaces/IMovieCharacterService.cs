using CelebrityWorkout.Models;

namespace CelebrityWorkout.Interfaces
{
    public interface IMovieCharacterService
    {
        Task<List<MovieCharacterDTO>> GetAllAsync();
        Task<MovieCharacterDTO?> GetByIdAsync(int id);
        Task<MovieCharacterDTO> CreateAsync(MovieCharacterDTO dto);
        Task<bool> UpdateAsync(int id, MovieCharacterDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
