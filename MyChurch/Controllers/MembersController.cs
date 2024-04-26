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
    public class MembersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Members
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var members = db.Members.Include(m => m.Ministry1);
            return View(members.ToList());
        }

        // GET: Members/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Ministry = new SelectList(db.Ministries, "ID", "MinistryType");
            return View();
        }

        // POST: Members/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMember,FirstName,Lastname,Birthday,Email,PhoneNumber,Address,City,Country,Ministry,RegistrationDate")] Member member)
        {
            int highestMemberId = db.Members.Max(m => m.IDMember);

            int newMemberId = highestMemberId + 1;

            member.IDMember = newMemberId;

            member.RegistrationDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ministry = new SelectList(db.Ministries, "ID", "MinistryType", member.Ministry);
            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ministry = new SelectList(db.Ministries, "ID", "MinistryType", member.Ministry);
            return View(member);
        }

        // POST: Members/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMember,FirstName,Lastname,Birthday,Email,PhoneNumber,Address,City,Country,Ministry,RegistrationDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ministry = new SelectList(db.Ministries, "ID", "MinistryType", member.Ministry);
            return View(member);
        }

        // GET: Members/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
