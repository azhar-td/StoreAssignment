using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreAssignment.Models;
using StoreAssignment.VModel;

namespace StoreAssignment.Controllers
{
    public class ProductController : Controller
    {
        private readonly StoreDBContext _context;

        public ProductController(StoreDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var storeDBContext = _context.Product.Include(p => p.Brand).Include(p => p.Category).Include(p => p.SubCategory);
            var itemList = new List<VProduct>();
            foreach(var item in storeDBContext)
            {
                var vItem = new VProduct();
                itemList.Add(CustomMappingToViewModel(vItem, item));
            }
            return View(itemList.ToList());
        }
        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Title,BrandId,CategoryId,SubCategoryId,AvailabilityDate,IsAvailable,Image,Description")] VProduct vProduct)
        {
            if (ModelState.IsValid)
            {
                var dbModel = new Product();
                //copy byte stream to actual model
                byte[] data;
                using (var memoryStream = new MemoryStream())
                {
                    vProduct.Image.OpenReadStream().CopyTo(memoryStream);
                    data=memoryStream.ToArray();
                }
                dbModel=CustomMappingToActualModel(dbModel, vProduct);
                dbModel.Image = data;
                _context.Add(dbModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", vProduct.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", vProduct.CategoryId);
            ViewData["SubCategoryId"] = new SelectList(_context.SubCategory, "Id", "Name", vProduct.SubCategoryId);
            return View(vProduct);
        }

        private Product CustomMappingToActualModel(Product dbModel, VProduct vProduct)
        {
            dbModel.ProductName = vProduct.ProductName;
            dbModel.Title = vProduct.Title;
            dbModel.BrandId = vProduct.BrandId;
            dbModel.CategoryId = vProduct.CategoryId;
            dbModel.SubCategoryId = vProduct.SubCategoryId;
            dbModel.AvailabilityDate = vProduct.AvailabilityDate;
            dbModel.IsAvailable = vProduct.IsAvailable;
            dbModel.Description = vProduct.Description;
            return dbModel;
        }
        private VProduct CustomMappingToViewModel(VProduct vModel,Product dbModel)
        {
            vModel.ProductName = dbModel.ProductName;
            vModel.Title = dbModel.Title;
            vModel.BrandId = dbModel.BrandId;
            vModel.BrandName = dbModel.Brand.Name;
            vModel.CategoryId = dbModel.CategoryId;
            vModel.CategoryName = dbModel.Category.Name;
            vModel.SubCategoryId = dbModel.SubCategoryId;
            vModel.SubCategoryName = dbModel.SubCategory.Name;
            vModel.AvailabilityDate = dbModel.AvailabilityDate;
            vModel.ImageData = dbModel.Image;
            vModel.IsAvailable = dbModel.IsAvailable;
            vModel.Description = dbModel.Description;
            return vModel;
        }

        [HttpPost]
        public ActionResult GetSubCatByCat(int catid)
        {
            SelectList subCat = new SelectList(_context.SubCategory.Where(a=>a.CategoryId==catid), "Id", "Name");
            return Json(subCat);
        }
    }
}