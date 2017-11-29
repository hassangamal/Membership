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
using Membership.Areas.Admin.Models;
namespace Membership.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private MembershipEntities db = new MembershipEntities();

        // GET: Admin/Product
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.ProductLinkText).Include(p => p.ProductType);
            return View(await products.ToListAsync());
        }

        // GET: Admin/Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ProductLinkTextID = new SelectList(db.ProductLinkTexts, "ProductLinkTextID", "Title");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Title");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductID,Title,Descriptions,ImageUrl,ProductLinkTextID,ProductTypeID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductLinkTextID = new SelectList(db.ProductLinkTexts, "ProductLinkTextID", "Title", product.ProductLinkTextID);
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Title", product.ProductTypeID);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductLinkTextID = new SelectList(db.ProductLinkTexts, "ProductLinkTextID", "Title", product.ProductLinkTextID);
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Title", product.ProductTypeID);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductID,Title,Descriptions,ImageUrl,ProductLinkTextID,ProductTypeID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductLinkTextID = new SelectList(db.ProductLinkTexts, "ProductLinkTextID", "Title", product.ProductLinkTextID);
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Title", product.ProductTypeID);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
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
