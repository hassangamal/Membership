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
    public class SubscriptionProductController : Controller
    {
        private MembershipEntities db = new MembershipEntities();

        // GET: Admin/SubscriptionProduct
        public async Task<ActionResult> Index()
        {
            var subscriptionProducts = db.SubscriptionProducts.Include(s => s.Product).Include(s => s.Subscription);
            return View(await subscriptionProducts.ToListAsync());
        }

        // GET: Admin/SubscriptionProduct/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionProduct subscriptionProduct = await db.SubscriptionProducts.FindAsync(id);
            if (subscriptionProduct == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionProduct);
        }

        // GET: Admin/SubscriptionProduct/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title");
            ViewBag.SubscriptionID = new SelectList(db.Subscriptions, "SubscriptionID", "Title");
            return View();
        }

        // POST: Admin/SubscriptionProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubscriptionProductID,ProductID,SubscriptionID,OldProductID,OldSubscriptionID")] SubscriptionProduct subscriptionProduct)
        {
            if (ModelState.IsValid)
            {
                db.SubscriptionProducts.Add(subscriptionProduct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title", subscriptionProduct.ProductID);
            ViewBag.SubscriptionID = new SelectList(db.Subscriptions, "SubscriptionID", "Title", subscriptionProduct.SubscriptionID);
            return View(subscriptionProduct);
        }

        // GET: Admin/SubscriptionProduct/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionProduct subscriptionProduct = await db.SubscriptionProducts.FindAsync(id);
            if (subscriptionProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title", subscriptionProduct.ProductID);
            ViewBag.SubscriptionID = new SelectList(db.Subscriptions, "SubscriptionID", "Title", subscriptionProduct.SubscriptionID);
            return View(subscriptionProduct);
        }

        // POST: Admin/SubscriptionProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SubscriptionProductID,ProductID,SubscriptionID,OldProductID,OldSubscriptionID")] SubscriptionProduct subscriptionProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscriptionProduct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Title", subscriptionProduct.ProductID);
            ViewBag.SubscriptionID = new SelectList(db.Subscriptions, "SubscriptionID", "Title", subscriptionProduct.SubscriptionID);
            return View(subscriptionProduct);
        }

        // GET: Admin/SubscriptionProduct/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubscriptionProduct subscriptionProduct = await db.SubscriptionProducts.FindAsync(id);
            if (subscriptionProduct == null)
            {
                return HttpNotFound();
            }
            return View(subscriptionProduct);
        }

        // POST: Admin/SubscriptionProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SubscriptionProduct subscriptionProduct = await db.SubscriptionProducts.FindAsync(id);
            db.SubscriptionProducts.Remove(subscriptionProduct);
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
