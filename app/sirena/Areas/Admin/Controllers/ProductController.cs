using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sirena.Helpers;
using sirena.Infrustructure;
using sirena.Models;
using sirena.Models.DBModels;
using sirena.ViewModels;

namespace sirena.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        SqlContext _db;
        private readonly FileManager _fileManager;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _appEnvironment;

        public ProductController(SqlContext db, FileManager fileManager, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _fileManager = fileManager;
            _mapper = mapper;
            _appEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var productList = await _db.Products
                .Include("ProductCategory.Category")
                .Include("ProductColor.Color")
                .Include("ProductSize.Size")
                .ToListAsync();
            
            return View(_mapper.Map<IEnumerable<ProductVM>>(productList));
        }

        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            var categories = await _db.Categories.ToListAsync();
            var colors = await _db.Colors.ToListAsync();
            var sizes = await _db.Sizes.ToListAsync();

            var vm = new ProductCVM()
            {
                Categories = _mapper.Map<IEnumerable<SelectListItem>>(categories),
                Colors = _mapper.Map<IEnumerable<SelectListItem>>(colors),
                Sizes = _mapper.Map<IEnumerable<SelectListItem>>(sizes),
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCVM product)
        {
            try
            {
                var item = _mapper.Map<Product>(product);

                _db.Products.Add(item);
                await _db.SaveChangesAsync();

                if(product.File != null)
                {
                    item.MainPhoto = await _fileManager.UploadPhoto(_appEnvironment.WebRootPath, 
                                                                        item.Id.ToString(), product.File);

                    _db.Products.Update(item);
                    await _db.SaveChangesAsync();
                }

                for (int i = 0; i < product.SelectedCategories.Count(); i++)
                {
                    var category = new ProductCategory()
                    {
                        CategoryId = product.SelectedCategories[i],
                        ProductId = item.Id
                    };

                    _db.ProductCategories.Add(category);
                    item.ProductCategory.Add(category);
                }

                for (int i = 0; i < product.SelectedColors.Count(); i++)
                {
                    var color = new ProductColor()
                    {
                        ColorId = product.SelectedColors[i],
                        ProductId = item.Id
                    };

                    _db.ProductColors.Add(color);
                    item.ProductColor.Add(color);
                }

                for (int i = 0; i < product.SelectedSizes.Count(); i++)
                {
                    var size = new ProductSize()
                    {
                        SizeId = product.SelectedSizes[i],
                        ProductId = item.Id
                    };

                    _db.ProductSizes.Add(size);
                    item.ProductSize.Add(size);
                }

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
            var record = await _db.Products
                .Include("ProductCategory.Category")
                .FirstOrDefaultAsync(x => x.Id == id);

            return View(_mapper.Map<ProductVM>(record));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _db.Products
                                .Include("ProductCategory.Category")
                                .Include("ProductColor.Color")
                                .Include("ProductSize.Size")
                .FirstOrDefaultAsync(x => x.Id == id);
            var vm = _mapper.Map<ProductCVM>(item);

            var categories = await _db.Categories.ToListAsync();
            var colors = await _db.Colors.ToListAsync();
            var sizes = await _db.Sizes.ToListAsync();

            vm.Categories = _mapper.Map<IEnumerable<SelectListItem>>(categories);
            vm.Colors = _mapper.Map<IEnumerable<SelectListItem>>(colors);
            vm.Sizes = _mapper.Map<IEnumerable<SelectListItem>>(sizes);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductCVM product)
        {
            try
            {
                var item = _mapper.Map<Product>(product);
                var oldItem = await _db.Products.FirstOrDefaultAsync(t => t.Id == item.Id);

                item.ProductCategory = new List<ProductCategory>();
                item.ProductColor = new List<ProductColor>();
                item.ProductSize = new List<ProductSize>();

                if (product.File != null)
                {
                    string oldFilePath = item.MainPhoto;
                    item.MainPhoto = await _fileManager.UploadPhoto(_appEnvironment.WebRootPath,
                                                                                item.Id.ToString(), product.File);

                    if (!string.IsNullOrWhiteSpace(oldFilePath))
                        _fileManager.DeleteFile(_appEnvironment.WebRootPath, oldFilePath);
                }

                // --- Category ---//
                
                var oldCategoryList = oldItem.ProductCategory.Select(x => x.CategoryId).ToList();
                List<Guid> newCategoryList = new List<Guid>();

                for (int i = 0; i < product.SelectedCategories.Count(); i++)
                {
                    newCategoryList.Add(product.SelectedCategories[i]);
                }

                ProductHelper.CompareRelations(ref oldCategoryList , ref newCategoryList);

                foreach (var elem in oldCategoryList)
                {
                    var category = new ProductCategory()
                    {
                        CategoryId = elem,
                        ProductId = item.Id
                    };

                    item.ProductCategory.Remove(category);
                    _db.ProductCategories.Remove(category);
                }

                foreach (var elem in newCategoryList)
                {
                    var category = new ProductCategory()
                    {
                        CategoryId = elem,
                        ProductId = item.Id
                    };

                    item.ProductCategory.Add(category);
                    _db.ProductCategories.Add(category);
                }

                // --- Colors --- //

                var oldColorList = oldItem.ProductColor.Select(t => t.ColorId).ToList();
                List<Guid> newColorList = new List<Guid>();

                for (int i = 0; i < product.SelectedColors.Count(); i++)
                {
                    newColorList.Add(product.SelectedColors[i]);
                }

                ProductHelper.CompareRelations(ref oldColorList, ref newColorList);

                foreach (var elem in oldColorList)
                {
                    var color = new ProductColor()
                    {
                        ColorId = elem,
                        ProductId = item.Id
                    };

                    item.ProductColor.Remove(color);
                    _db.ProductColors.Remove(color);
                }

                foreach (var elem in newColorList)
                {
                    var color = new ProductColor()
                    {
                        ColorId = elem,
                        ProductId = item.Id
                    };

                    item.ProductColor.Add(color);
                    _db.ProductColors.Add(color);
                }

                // --- Size --- //

                var oldSizeList = oldItem.ProductSize.Select(t => t.SizeId).ToList();
                List<Guid> newSizeList = new List<Guid>();

                for (int i = 0; i < product.SelectedSizes.Count(); i++)
                {
                    newSizeList.Add(product.SelectedSizes[i]);
                }

                ProductHelper.CompareRelations(ref oldSizeList, ref newSizeList);

                foreach (var elem in oldSizeList)
                {
                    var size = new ProductSize()
                    {
                        SizeId = elem,
                        ProductId = item.Id
                    };

                    item.ProductSize.Remove(size);
                    _db.ProductSizes.Remove(size);
                }

                foreach (var elem in newSizeList)
                {
                    var size = new ProductSize()
                    {
                        SizeId = elem,
                        ProductId = item.Id
                    };

                    item.ProductSize.Add(size);
                    _db.ProductSizes.Add(size);
                }

                _db.Products.Update(item);
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
            try
            {
                var item = await _db.Products
                                    .Include("ProductCategory.Category")
                                    .Include("ProductColor.Color")
                                    .Include("ProductSize.Size")
                                    .FirstOrDefaultAsync(x => x.Id == id);

                var itemCategories = item.ProductCategory.ToList();
                var itemSizes = item.ProductSize.ToList();
                var itemColors = item.ProductColor.ToList();

                foreach (var elem in itemCategories)
                {
                    var category = await _db.ProductCategories.FirstOrDefaultAsync(t => t.CategoryId == elem.CategoryId);
                    _db.ProductCategories.Remove(category);
                }

                foreach (var elem in itemColors)
                {
                    var color = await _db.ProductColors.FirstOrDefaultAsync(t => t.ColorId == elem.ColorId);
                    _db.ProductColors.Remove(color);
                }

                foreach (var elem in itemSizes)
                {
                    var size = await _db.ProductSizes.FirstOrDefaultAsync(t => t.SizeId == elem.SizeId);
                    _db.ProductSizes.Remove(size);
                }

                _db.Products.Remove(item);
                await _db.SaveChangesAsync();

                _fileManager.DeleteFolder(_appEnvironment.WebRootPath, item.MainPhoto);

                return RedirectToAction(nameof(List));

            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(List));
            }
        }
    }
}