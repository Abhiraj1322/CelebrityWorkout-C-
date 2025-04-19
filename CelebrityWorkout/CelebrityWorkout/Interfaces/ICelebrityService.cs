using CelebrityWorkout.Models;

namespace CelebrityWorkout.Interfaces
{
    public interface ICelebrityService
    {
        Task<List<CelebrityDTO>> GetAllAsync();
        Task<CelebrityDTO?> GetByIdAsync(int id);
        Task<CelebrityDTO> CreateAsync(CelebrityDTO dto);
        Task<bool> UpdateAsync(int id, CelebrityDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
