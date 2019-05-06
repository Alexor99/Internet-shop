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
    public class ColorController : Controller
    {
        SqlContext _db;
        private readonly IMapper _mapper;

        public ColorController(SqlContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IActionResult> List()
        {
            var colors = await _db.Colors
                .Include("ProductColor.Product")
                .ToListAsync();

            var temp = colors;

            return View(_mapper.Map<IEnumerable<ColorVM>>(colors));
        }
        
        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ColorCVM color)
        {
            try
            {
                var item = _mapper.Map<Color>(color);

                _db.Colors.Add(item);
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
            var item = await _db.Colors.FirstOrDefaultAsync(t => t.Id == id);
            var model = _mapper.Map<ColorCVM>(item);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ColorCVM color)
        {
            try
            {
                var item = _mapper.Map<Color>(color);

                _db.Colors.Update(item);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(List));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Edit));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _db.Colors.FirstOrDefaultAsync(t => t.Id == id);

            _db.Colors.Remove(item);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }
    }
}