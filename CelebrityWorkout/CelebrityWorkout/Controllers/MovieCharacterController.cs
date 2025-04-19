using Microsoft.AspNetCore.Mvc;
using CelebrityWorkout.Models;
using CelebrityWorkout.Interfaces;

namespace CelebrityWorkout.Controllers.Api
{
    [Route("api/moviecharacters")]
    [ApiController]
    public class MovieCharacterController : ControllerBase
    {
        private readonly IMovieCharacterService _service;

        public MovieCharacterController(IMovieCharacterService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all movie characters.
        /// </summary>
        /// <returns>A list of MovieCharacterDTO objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>
        /// Retrieves a specific movie character by their ID.
        /// </summary>
        /// <param name="id">The ID of the movie character.</param>
        /// <returns>The matching MovieCharacterDTO if found, otherwise 404.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Creates a new movie character.
        /// </summary>
        /// <param name="dto">The MovieCharacterDTO object to create.</param>
        /// <returns>The created MovieCharacterDTO with status 201 Created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(MovieCharacterDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.CharacterId }, result);
        }

        /// <summary>
        /// Updates an existing movie character.
        /// </summary>
        /// <param name="id">The ID of the movie character to update.</param>
        /// <param name="dto">The updated MovieCharacterDTO object.</param>
        /// <returns>NoContent if successful, NotFound if the character does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MovieCharacterDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Deletes a movie character by ID.
        /// </summary>
        /// <param name="id">The ID of the movie character to delete.</param>
        /// <returns>NoContent if successful, NotFound if the character does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
