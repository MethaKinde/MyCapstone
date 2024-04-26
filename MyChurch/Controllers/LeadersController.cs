using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyChurch.Models;

namespace MyChurch.Controllers
{
    public class LeadersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Leaders
        public ActionResult Index()
        {
            var leaders = db.Leaders.Include(l => l.Ministry1);
            return View(leaders.ToList());
        }

        // GET: Leaders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leader leader = db.Leaders.Find(id);
            if (leader == null)
            {
                return HttpNotFound();
            }
            return View(leader);
        }

        // GET: Leaders/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Ministry = new SelectList(db.Ministries, "ID", "MinistryType");
            return View();
        }

        // POST: Leaders/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLeader,FirstName,Lastname,Ministry,Email,Photo")] Leader leader)
        {
            int highestLeaderId = db.Leaders.Max(l => l.IDLeader);

            int newLeaderId = highestLeaderId + 1;

            leader.IDLeader = newLeaderId;

            if (ModelState.IsValid)
            {
                db.Leaders.Add(leader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ministry = new SelectList(db.Ministries, "ID", "MinistryType", leader.Ministry);
            return View(leader);
        }

        // GET: Leaders/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leader leader = db.Leaders.Find(id);
            if (leader == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ministry = new SelectList(db.Ministries, "ID", "MinistryType", leader.Ministry);
            return View(leader);
        }

        // POST: Leaders/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLeader,FirstName,Lastname,Ministry,Email,Photo")] Leader leader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ministry = new SelectList(db.Ministries, "ID", "MinistryType", leader.Ministry);
            return View(leader);
        }

        // GET: Leaders/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leader leader = db.Leaders.Find(id);
            if (leader == null)
            {
                return HttpNotFound();
            }
            return View(leader);
        }

        // POST: Leaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leader leader = db.Leaders.Find(id);
            db.Leaders.Remove(leader);
            db.SaveChanges();
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
