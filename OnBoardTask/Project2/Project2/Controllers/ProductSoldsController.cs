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
    public class ProductSoldsController : Controller
    {
        private CRUDTaskEntities db = new CRUDTaskEntities();

        // GET: ProductSolds
        public ActionResult Index()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");

            return View();
        }
        public ActionResult GetSales()
        {
            List<ProductSoldViewModel> sales = db.ProductSolds.Select(x => new ProductSoldViewModel
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                ProductId = x.ProductId,
                StoreId = x.StoreId,
                CustomerName = x.Customer.CustomerName,
                ProductName = x.Product.ProductName,
                StoreName = x.Store.StoreName,
                DateSold = x.DateSold
            }).ToList();

            return Json(sales, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddSale(ProductSold sale)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(sale);
                db.SaveChanges();
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");

            return null;
        }
        [HttpPost]
        public ActionResult EditSale(ProductSoldViewModel sale)
        {
            try
            {
                if (sale.Id >= 0)
                {

                    ProductSold editedSale = db.ProductSolds.Find(sale.Id);
                    editedSale.ProductId = sale.ProductId;
                    editedSale.CustomerId = sale.CustomerId;
                    editedSale.StoreId = sale.StoreId;
                    editedSale.DateSold = sale.DateSold;
                    db.Entry(editedSale).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
                    ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
                    ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
        [HttpPost]
        public ActionResult DeleteSale(int id)
        {
            ProductSold sale = db.ProductSolds.Find(id);

            try
            {
                db.ProductSolds.Remove(sale);
                db.SaveChanges();

                ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
                ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
                ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
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
       