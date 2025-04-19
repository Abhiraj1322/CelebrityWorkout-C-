using CelebrityWorkout.Data;
using CelebrityWorkout.Interfaces;
using CelebrityWorkout.Models;
using Microsoft.EntityFrameworkCore;

namespace CelebrityWorkout.Services
{
    public class WorkoutRoutineService : IWorkoutRoutineService
    {
        private readonly ApplicationDbContext _context;

        public WorkoutRoutineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutRoutineDTO>> GetAllAsync()
        {
            return await _context.WorkoutRoutines
                .Include(w => w.Celebrity)
                .Select(w => new WorkoutRoutineDTO
                {
                    WorkoutId = w.WorkoutId,
                    WorkoutType = w.WorkoutType,
                    Frequency = w.Frequency,
                    Duration = w.Duration,
                    Description = w.Description,
                    CelebrityId = w.CelebrityId,
                    Celebrity = new CelebrityDTO
                    {
                        CelebrityId = w.Celebrity.CelebrityId,
                        Name = w.Celebrity.Name
                    }
                }).ToListAsync();
        }

        public async Task<WorkoutRoutineDTO?> GetByIdAsync(int id)
        {
            var w = await _context.WorkoutRoutines.Include(w => w.Celebrity).FirstOrDefaultAsync(x => x.WorkoutId == id);
            if (w == null) return null;

            return new WorkoutRoutineDTO
            {
                WorkoutId = w.WorkoutId,
                WorkoutType = w.WorkoutType,
                Frequency = w.Frequency,
                Duration = w.Duration,
                Description = w.Description,
                CelebrityId = w.CelebrityId,
                Celebrity = new CelebrityDTO
                {
                    CelebrityId = w.Celebrity.CelebrityId,
                    Name = w.Celebrity.Name
                }
            };
        }

        public async Task<WorkoutRoutineDTO> CreateAsync(WorkoutRoutineDTO dto)
        {
            var routine = new WorkoutRoutine
            {
                WorkoutType = dto.WorkoutType,
                Frequency = dto.Frequency,
                Duration = dto.Duration,
                Description = dto.Description,
                CelebrityId = dto.CelebrityId
            };

            _context.WorkoutRoutines.Add(routine);
            await _context.SaveChangesAsync();
            dto.WorkoutId = routine.WorkoutId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, WorkoutRoutineDTO dto)
        {
            var w = await _context.WorkoutRoutines.FindAsync(id);
            if (w == null) return false;

            w.WorkoutType = dto.WorkoutType;
            w.Frequency = dto.Frequency;
            w.Duration = dto.Duration;
            w.Description = dto.Description;
            w.CelebrityId = dto.CelebrityId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var w = await _context.WorkoutRoutines.FindAsync(id);
            if (w == null) return false;

            _context.WorkoutRoutines.Remove(w);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
