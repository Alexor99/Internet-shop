using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sirena.Infrustructure;
using sirena.Models;
using sirena.Models.DBModels;
using sirena.ViewModels;

namespace sirena.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        SqlContext _db;
        private readonly IMapper _mapper;

        public ContactController(SqlContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var contacts = await _db.Contacts.ToListAsync();

            return View(_mapper.Map<IEnumerable<ContactVM>>(contacts));
        }

        [HttpGet]
        public  IActionResult Create()
        {
            var contact = new ContactVM();
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactVM contact)
        {
            try
            {
                var item = _mapper.Map<Contact>(contact);

                _db.Contacts.Add(item);
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
            var item = await _db.Contacts.FirstOrDefaultAsync(t => t.Id == id);
            var model = _mapper.Map<ContactVM>(item);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactVM contact)
        {
            try
            {
                var item = _mapper.Map<Contact>(contact);

                _db.Contacts.Update(item);
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
            var item = await _db.Contacts.FirstOrDefaultAsync(t => t.Id == id);

            _db.Contacts.Remove(item);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(List));
        }
    }
}