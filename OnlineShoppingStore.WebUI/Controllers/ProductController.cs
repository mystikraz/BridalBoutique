using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Concrete;
using OnlineShoppingStore.Domain.Entities;
using OnlineShoppingStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 2;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(string category, int page=1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p=>category==null||p.Category==category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null?
                    repository.Products.Count():
                    repository.Products.Where(e=>e.Category==category).Count()
                },
                CurrentCategory=category
            };
            return View(model);
        }
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult List(string searchTerm)
        {
            EFDbContext db = new EFDbContext();
            ProductsListViewModel model = new ProductsListViewModel();

            if (String.IsNullOrEmpty(searchTerm))
            {
                model.Products = db.Products;
            }
            else
            {
                model.Products = db.Products.Where(x => x.Name.StartsWith(searchTerm));
            }


            model.PagingInfo = new PagingInfo();
            model.PagingInfo.CurrentPage = 1; model.PagingInfo.ItemsPerPage = 1; model.PagingInfo.TotalItems = 1;

            model.CurrentCategory = db.Products.Where(p => p.Category == model.CurrentCategory).ToString();


            return View(model);
        }

    }
}