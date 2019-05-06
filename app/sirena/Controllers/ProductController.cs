using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sirena.Models;

namespace sirena.Controllers
{
    public class ProductController : Controller
    {
        SqlContext _db;
        private readonly IMapper _mapper;

        public ProductController (SqlContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetProducts()
        {
            var productList = await _db.Products
                                    .Include("ProductCategory.Category")
                                    .Include("ProductColor.Color")
                                    .Include("ProductSize.Size")
                                    .ToListAsync();

            return PartialView();
        }

        public IActionResult GetSomeProducts()
        {
            return PartialView();
        }
    }
}