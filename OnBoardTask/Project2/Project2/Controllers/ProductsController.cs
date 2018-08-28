using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project2.Models;

namespace Project2.Controllers
{
    public class ProductsController : Controller
    {
        private CRUDTaskEntities db = new CRUDTaskEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetProducts()
        {
            List<ProductViewModel> products = db.Products.Select(x => new ProductViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Price = x.Price

            }).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            return null;
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }

            return null;
        }
        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            Product product = db.Products.Find(productId);

            try
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Foreign key relationship is being violated with ProductsSold");
            }

            return null;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}