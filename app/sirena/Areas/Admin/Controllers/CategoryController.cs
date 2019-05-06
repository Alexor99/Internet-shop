using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sirena.Infrustructure;
using sirena.Models;
using sirena.Models.DBModels;
using sirena.ViewModels;

namespace sirena.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private SqlContext _db;
        private readonly IMapper _mapper;

        public CategoryController(SqlContext context,IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> List()
        {
            var categories = await _db.Categories.ToListAsync();

            return View(_mapper.Map<IEnumerable<CategoryVM>>(categories));
        }

        public IActionResult Create()
        {
            var categories = new CategoryVM();

            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM category)
        {
            try
            {
                _db.Categories.Add(_mapper.Map<Category>(category));
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Create));

            }
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);

            return View(_mapper.Map<CategoryVM>(category));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var categories = await _db.Categories.FirstOrDefaultAsync(t => t.Id == id);

            var vm = _mapper.Map<CategoryVM>(categories);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryVM service)
        {
            try
            {
                _db.Categories.Update(_mapper.Map<Category>(service));
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(List));
            }
            catch
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            _db.Categories.Remove(_db.Categories.FirstOrDefault(x => x.Id == id));
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }
    }
}