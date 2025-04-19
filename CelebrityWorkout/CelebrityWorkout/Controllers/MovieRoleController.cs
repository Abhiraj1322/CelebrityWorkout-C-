using Microsoft.AspNetCore.Mvc;
using CelebrityWorkout.Models;
using CelebrityWorkout.Interfaces;

namespace CelebrityWorkout.Controllers.Api
{
    [Route("api/movieroles")]
    [ApiController]
    public class MovieRoleController : ControllerBase
    {
        private readonly IMovieRoleService _service;

        public MovieRoleController(IMovieRoleService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all movie roles.
        /// </summary>
        /// <returns>A list of MovieRoleDTO objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>
        /// Retrieves a movie role by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie role.</param>
        /// <returns>The matching MovieRoleDTO if found, otherwise 404 Not Found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Creates a new movie role.
        /// </summary>
        /// <param name="dto">The MovieRoleDTO to create.</param>
        /// <returns>The created MovieRoleDTO with status 201 Created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(MovieRoleDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.MovieRoleId }, result);
        }

        /// <summary>
        /// Updates an existing movie role.
        /// </summary>
        /// <param name="id">The ID of the movie role to update.</param>
        /// <param name="dto">The updated MovieRoleDTO object.</param>
        /// <returns>NoContent if successful, NotFound if the movie role does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MovieRoleDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes a movie role by ID.
        /// </summary>
        /// <param name="id">The ID of the movie role to delete.</param>
        /// <returns>NoContent if successful, NotFound if the movie role does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
