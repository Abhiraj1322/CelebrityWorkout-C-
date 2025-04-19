using Microsoft.AspNetCore.Mvc;
using CelebrityWorkout.Models;
using CelebrityWorkout.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CelebrityWorkout.Controllers
{
    public class CelebrityPageController : Controller
    {
        private readonly ICelebrityService _service;

        public CelebrityPageController(ICelebrityService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var celeb = await _service.GetByIdAsync(id);
            return celeb == null ? NotFound() : View(celeb);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CelebrityDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var celeb = await _service.GetByIdAsync(id);
            return celeb == null ? NotFound() : View(celeb);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, CelebrityDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _service.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var celeb = await _service.GetByIdAsync(id);
            return celeb == null ? NotFound() : View(celeb);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
