using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Membership.Models;

namespace Membership.Areas.Admin.Controllers
{
    public class ProductItemController : Controller
    {
        private MembershipEntities db = new MembershipEntities();

        // GET: Admin/ProductItem
        public async Task<ActionResult> Index()
        {
            var productItems = db.ProductItems.Include(p => p.Item).Include(p => p.Product);
            return View(await productItems.ToListAsync());
        }

        // GET: Admin/ProductItem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await db.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // GET: Admin/ProductItem/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Title");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title");
            return View();
        }

        // POST: Admin/ProductItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductItemID,ProductID,ItemID,OldProductID,OldItemID")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                db.ProductItems.Add(productItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Title", productItem.ItemID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title", productItem.ProductID);
            return View(productItem);
        }

        // GET: Admin/ProductItem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await db.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Title", productItem.ItemID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title", productItem.ProductID);
            return View(productItem);
        }

        // POST: Admin/ProductItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductItemID,ProductID,ItemID,OldProductID,OldItemID")] ProductItem productItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Title", productItem.ItemID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title", productItem.ProductID);
            return View(productItem);
        }

        // GET: Admin/ProductItem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductItem productItem = await db.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return HttpNotFound();
            }
            return View(productItem);
        }

        // POST: Admin/ProductItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductItem productItem = await db.ProductItems.FindAsync(id);
            db.ProductItems.Remove(productItem);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
