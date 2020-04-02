using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
using Rotativa;

namespace ProyectoFinal.Controllers
{
    public class IngresosController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Ingresos
        public ActionResult Index()
        {
            var ingresos = db.Ingresos.Include(i => i.Habitaciones).Include(i => i.Pacientes);
            return View(ingresos.ToList());
        }

        [HttpPost]
        public ActionResult Index(string select, string buscar)
        {
            if (select == "Fecha")
            {
                var ingresos = db.Ingresos.Include(c => c.Habitaciones).Include(c => c.Pacientes).Where(a => a.Fecha_Ingreso.Contains(buscar));
                return View(ingresos.ToList());

            }
            else if (select == "Habitacion")
            {
                int c = (from d in db.Habitaciones where d.Numero == buscar select d.Numero).SingleOrDefault();

                var ingresos = db.Ingresos.Include(i => i.Habitaciones).Include(i => i.Pacientes).Where(e=>e.Habitacion_Id.Equals(select));
                return View(ingresos.ToList());
            }

            return View();

        }

        public ActionResult Imprimir()
        {
            var print = new ActionAsPdf("Index");
            return print;
        }

        // GET: Ingresos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // GET: Ingresos/Create
        public ActionResult Create()
        {
            ViewBag.Habitacion_Id = new SelectList(db.Habitaciones, "IdHabitacion", "IdHabitacion");
            ViewBag.Paciente_Id = new SelectList(db.Pacientes, "IdPacientes", "IdPacientes");
            return View();
        }

        // POST: Ingresos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdIngresos,Paciente_Id,Habitacion_Id,Fecha_Ingreso")] Ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                db.Ingresos.Add(ingresos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Habitacion_Id = new SelectList(db.Habitaciones, "IdHabitacion", "Tipo", ingresos.Habitacion_Id);
            ViewBag.Paciente_Id = new SelectList(db.Pacientes, "IdPacientes", "Cedula", ingresos.Paciente_Id);
            return View(ingresos);
        }

        // GET: Ingresos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Habitacion_Id = new SelectList(db.Habitaciones, "IdHabitacion", "Tipo", ingresos.Habitacion_Id);
            ViewBag.Paciente_Id = new SelectList(db.Pacientes, "IdPacientes", "Cedula", ingresos.Paciente_Id);
            return View(ingresos);
        }

        // POST: Ingresos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdIngresos,Paciente_Id,Habitacion_Id,Fecha_Ingreso")] Ingresos ingresos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingresos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Habitacion_Id = new SelectList(db.Habitaciones, "IdHabitacion", "Tipo", ingresos.Habitacion_Id);
            ViewBag.Paciente_Id = new SelectList(db.Pacientes, "IdPacientes", "Cedula", ingresos.Paciente_Id);
            return View(ingresos);
        }

        // GET: Ingresos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingresos ingresos = db.Ingresos.Find(id);
            if (ingresos == null)
            {
                return HttpNotFound();
            }
            return View(ingresos);
        }

        // POST: Ingresos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingresos ingresos = db.Ingresos.Find(id);
            db.Ingresos.Remove(ingresos);
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
