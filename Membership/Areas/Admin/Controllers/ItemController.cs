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
    public class ItemController : Controller
    {
        private MembershipEntities db = new MembershipEntities();

        // GET: Admin/Item
        public async Task<ActionResult> Index()
        {
            var items = db.Items.Include(i => i.ItemType).Include(i => i.Part).Include(i => i.Section);
            return View(await items.ToListAsync());
        }

        // GET: Admin/Item/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Admin/Item/Create
        public ActionResult Create()
        {
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "Title");
            ViewBag.PartID = new SelectList(db.Parts, "PartID", "Title");
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Title");
            return View();
        }

        // POST: Admin/Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ItemID,Title,Descriptions,Url,ImageUrl,HTML,WaitDays,HTMLShort,IsFree,ProductId,SectionID,ItemTypeID,PartID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "Title", item.ItemTypeID);
            ViewBag.PartID = new SelectList(db.Parts, "PartID", "Title", item.PartID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Title", item.SectionID);
            return View(item);
        }

        // GET: Admin/Item/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "Title", item.ItemTypeID);
            ViewBag.PartID = new SelectList(db.Parts, "PartID", "Title", item.PartID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Title", item.SectionID);
            return View(item);
        }

        // POST: Admin/Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemID,Title,Descriptions,Url,ImageUrl,HTML,WaitDays,HTMLShort,IsFree,ProductId,SectionID,ItemTypeID,PartID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ItemTypeID = new SelectList(db.ItemTypes, "ItemTypeID", "Title", item.ItemTypeID);
            ViewBag.PartID = new SelectList(db.Parts, "PartID", "Title", item.PartID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Title", item.SectionID);
            return View(item);
        }

        // GET: Admin/Item/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Admin/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Item item = await db.Items.FindAsync(id);
            db.Items.Remove(item);
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
