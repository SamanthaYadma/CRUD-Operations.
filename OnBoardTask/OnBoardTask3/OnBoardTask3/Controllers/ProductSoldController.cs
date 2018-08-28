using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnBoardTask3.Models;

namespace KnockOutMVC.Controllers
{
    public class ProductSoldsController : Controller
    {
        private CRUDTaskEntities db = new CRUDTaskEntities();

        // GET: ProductSolds
        public ActionResult Index()
        {
            return View();
        }
        //GET All ProductSoldList
        public JsonResult ProductSoldList()
        {
            
            var sale = db.ProductSolds.Select(x => new KOClass
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                ProductId = x.ProductId,
                DateSold = x.DateSold,
                CustomerName = x.Customer.CustomerName,
                ProductName = x.Product.ProductName,
                StoreName = x.Store.StoreName,

            });
            return Json(sale, JsonRequestBehavior.AllowGet);
        }

        // GET: ProductSolds/Details/5
        public ActionResult Details()
        {
            return PartialView();
        }

        // GET: ProductSolds/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public int Create([Bind(Include = "ProductSoldID,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(productSold);
                db.SaveChanges();
                return productSold.Id;
            }
            else
            {
                return 0;
            }
        }
        // GET: ProductSolds/Edit/5
        public ActionResult Edit()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
            return PartialView();
        }
        //POST: ProductSolds/Edit/5
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSold).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpStatusCodeResult(200, "Success");
            }
            return new HttpStatusCodeResult(404, "Unable to update.");
        }





        // GET: ProductSolds/Delete/5
        public ActionResult Delete()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            return PartialView();
        }

        // POST: ProductSolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                ProductSold productSold = db.ProductSolds.Find(id);
                db.ProductSolds.Remove(productSold);
                db.SaveChanges();
                return new HttpStatusCodeResult(200, "Success");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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