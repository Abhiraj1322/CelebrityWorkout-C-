using Microsoft.AspNetCore.Mvc;
using CelebrityWorkout.Models;
using CelebrityWorkout.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CelebrityWorkout.Controllers
{
    public class MovieCharacterPageController : Controller
    {
        private readonly IMovieCharacterService _service;
        private readonly ICelebrityService _celebService;

        public MovieCharacterPageController(IMovieCharacterService service, ICelebrityService celebService)
        {
            _service = service;
            _celebService = celebService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : View(item);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            await LoadCelebritiesAsync();
            return View();
        }
  
      
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(MovieCharacterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCelebritiesAsync();
                return View(dto);
            }

            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            await LoadCelebritiesAsync();
            return View(item);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, MovieCharacterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCelebritiesAsync();
                return View(dto);
            }

            await _service.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : View(item);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

       
        private async Task LoadCelebritiesAsync()
        {
            ViewBag.Celebrities = await _celebService.GetAllAsync();
        }
    }
}
