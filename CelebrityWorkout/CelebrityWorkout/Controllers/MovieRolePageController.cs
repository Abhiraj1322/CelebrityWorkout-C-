using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CelebrityWorkout.Models;
using CelebrityWorkout.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CelebrityWorkout.Controllers
{
    public class MovieRolePageController : Controller
    {
        private readonly IMovieRoleService _service;
        private readonly ICelebrityService _celebService;

        public MovieRolePageController(IMovieRoleService service, ICelebrityService celebService)
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
            await LoadCelebrityDropdown();
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(MovieRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCelebrityDropdown();
                return View(dto);
            }

            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();

            await LoadCelebrityDropdown();
            return View(item);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, MovieRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCelebrityDropdown();
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

        // ✅ Fix: Wrap celebrity list into SelectList
        private async Task LoadCelebrityDropdown()
        {
            var celebrities = await _celebService.GetAllAsync();
            ViewBag.Celebrities = new SelectList(celebrities, "CelebrityId", "Name");
        }
    }
}
