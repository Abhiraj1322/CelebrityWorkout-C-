using Microsoft.AspNetCore.Mvc;
using CelebrityWorkout.Models;
using CelebrityWorkout.Interfaces;

namespace CelebrityWorkout.Controllers.Api
{
    [Route("api/workouts")]
    [ApiController]
    public class WorkoutRoutineController : ControllerBase
    {
        private readonly IWorkoutRoutineService _service;

        public WorkoutRoutineController(IWorkoutRoutineService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all workout routines.
        /// </summary>
        /// <returns>A list of WorkoutRoutineDTO objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>
        /// Retrieves a workout routine by its ID.
        /// </summary>
        /// <param name="id">The ID of the workout routine.</param>
        /// <returns>The matching WorkoutRoutineDTO if found, otherwise 404 Not Found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Creates a new workout routine.
        /// </summary>
        /// <param name="dto">The WorkoutRoutineDTO to create.</param>
        /// <returns>The created WorkoutRoutineDTO with status 201 Created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(WorkoutRoutineDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.WorkoutId }, result);
        }

        /// <summary>
        /// Updates an existing workout routine.
        /// </summary>
        /// <param name="id">The ID of the workout routine to update.</param>
        /// <param name="dto">The updated WorkoutRoutineDTO object.</param>
        /// <returns>NoContent if successful, NotFound if the workout does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WorkoutRoutineDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes a workout routine by ID.
        /// </summary>
        /// <param name="id">The ID of the workout routine to delete.</param>
        /// <returns>NoContent if successful, NotFound if the workout does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
