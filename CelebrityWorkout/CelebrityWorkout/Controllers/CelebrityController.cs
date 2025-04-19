using Microsoft.AspNetCore.Mvc;
using CelebrityWorkout.Models;
using CelebrityWorkout.Interfaces;

namespace CelebrityWorkout.Controllers.Api
{
    [Route("api/celebrities")]
    [ApiController]
    public class CelebrityController : ControllerBase
    {
        private readonly ICelebrityService _service;

        public CelebrityController(ICelebrityService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get a list of all celebrities.
        /// </summary>
        /// <returns>A list of CelebrityDTO objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        /// <summary>
        /// Get details of a specific celebrity by ID.
        /// </summary>
        /// <param name="id">The ID of the celebrity.</param>
        /// <returns>CelebrityDTO if found, otherwise 404 Not Found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Create a new celebrity.
        /// </summary>
        /// <param name="dto">The CelebrityDTO object to create.</param>
        /// <returns>The created celebrity with status 201.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CelebrityDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.CelebrityId }, result);
        }

        /// <summary>
        /// Update an existing celebrity by ID.
        /// </summary>
        /// <param name="id">The ID of the celebrity to update.</param>
        /// <param name="dto">The updated CelebrityDTO object.</param>
        /// <returns>NoContent if successful, NotFound otherwise.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CelebrityDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        /// <summary>
        /// Delete a celebrity by ID.
        /// </summary>
        /// <param name="id">The ID of the celebrity to delete.</param>
        /// <returns>NoContent if successful, NotFound otherwise.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
