using CelebrityWorkout.Models;

namespace CelebrityWorkout.Interfaces
{
    public interface IWorkoutRoutineService
    {
        Task<List<WorkoutRoutineDTO>> GetAllAsync();
        Task<WorkoutRoutineDTO?> GetByIdAsync(int id);
        Task<WorkoutRoutineDTO> CreateAsync(WorkoutRoutineDTO dto);
        Task<bool> UpdateAsync(int id, WorkoutRoutineDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
