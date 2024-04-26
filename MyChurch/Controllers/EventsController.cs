using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyChurch.Models;

namespace MyChurch.Controllers
{
    public class EventsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Events
        public ActionResult Index()
        {
            var events = db.Events.Include(e => e.EventType).ToList();

            // Formatta la data degli eventi // TODO
            //foreach (var @event in events)
            //{
            //    // Ottieni la rappresentazione testuale della data di inizio
            //    var DateStartText = @event.DateStart.ToString();
            //    // Formatta la rappresentazione testuale nel formato desiderato
            //    var DateStartFormatted = DateTime.Parse(DateStartText).ToString("MMM dd, yyyy");

            //    // Ottieni la rappresentazione testuale della data di fine, se necessario
            //    var DateEndText = @event.DateEnd.ToString();
            //    // Formatta la rappresentazione testuale nel formato desiderato
            //    var DateEndFormatted = DateTime.Parse(DateEndText).ToString("MMM dd, yyyy");

            //    @event.DateStartFormatted = DateStartFormatted;
            //    @event.DateEndFormatted = DateEndFormatted;
            //}

            return View(events);
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "ID", "EventType1");
            return View();
        }

        // POST: Events/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDEvent,Title,Description,DateStart,DateEnd,EventTypeID,FlyerEvent")] Event @event)
        {
            int highestEventId = db.Events.Max(e => e.IDEvent);

            int newEventId = highestEventId + 1;

            @event.IDEvent = newEventId;

            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventTypeID = new SelectList(db.EventTypes, "ID", "EventType1", @event.EventTypeID);
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "ID", "EventType1", @event.EventTypeID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDEvent,Title,Description,DateStart,DateEnd,EventTypeID,FlyerEvent")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "ID", "EventType1", @event.EventTypeID);
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
