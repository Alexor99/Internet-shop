using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sirena.Models;
using sirena.Models.DBModels;
using sirena.ViewModels;

namespace sirena.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        SqlContext _db;
        private readonly IMapper _mapper;

        public SizeController(SqlContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IActionResult> List()
        {
            var sizes = await _db.Sizes.ToListAsync();

            return View(_mapper.Map<IEnumerable<SizeVM>>(sizes));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(SizeCVM size)
        {
            try
            {
                var item = _mapper.Map<Size>(size);

                _db.Sizes.Add(item);

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Create));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _db.Sizes.FirstOrDefaultAsync(t => t.Id == id);
            var model = _mapper.Map<SizeCVM>(item);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SizeCVM size)
        {
            try
            {
                var item = _mapper.Map<Size>(size);

                _db.Sizes.Update(item);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(List));                
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Edit));
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _db.Sizes.FirstOrDefaultAsync(t => t.Id == id);

            _db.Sizes.Remove(item);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }
    }
}