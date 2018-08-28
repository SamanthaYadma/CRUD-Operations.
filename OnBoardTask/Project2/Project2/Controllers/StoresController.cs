using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Diagnostics;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project2.Models;
using Newtonsoft.Json;

namespace Project2.Controllers
{
    public class StoresController : Controller
    {
        private CRUDTaskEntities db = new CRUDTaskEntities();

        // GET: Stores
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetStore()
        {
            List<StoreViewModel> stores = db.Stores.Select(x => new StoreViewModel
            {
                StoreId = x.StoreId,
                StoreName = x.StoreName,
                StoreAddress = x.StoreAddress

            }).ToList();

            return Json(stores, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddStore(Store store)
        {
            if (ModelState.IsValid)
            {
                db.Stores.Add(store);
                db.SaveChanges();
            }

            return null;
        }
        [HttpPost]
        public ActionResult EditStore(Store store)
        {
            if (ModelState.IsValid)
            {
                db.Entry(store).State = EntityState.Modified;
                db.SaveChanges();
            }

            return null;
        }
        [HttpPost]
        public ActionResult DeleteStore(int StoreId)
        {
            Store store = db.Stores.Find(StoreId);

            try
            {
                db.Stores.Remove(store);
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
