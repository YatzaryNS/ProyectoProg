using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;
//using Rotativa;

namespace ProyectoFinal.Controllers
{
    public class AltasController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Altas
        public ActionResult Index()
        {
            var altas = db.Altas.Include(a => a.Ingresos);
            return View(altas.ToList());
        }

        [HttpPost]
        public ActionResult Index(string select, string buscar)
        {
            if (select == "Fecha")
            {
                var altas = db.Altas.Include(c => c.Ingresos).Where(a => a.Fecha_Salida==buscar);

                try { 
                ViewBag.total = altas.Sum(c => c.Total_Pagar);
                ViewBag.cont = altas.Count();
                ViewBag.min = altas.Min(c => c.Total_Pagar);
                ViewBag.max = altas.Max(c => c.Total_Pagar);
                ViewBag.prom = altas.Average(c => c.Total_Pagar);

                return View(altas.ToList());
                }
                catch(Exception e)
                {
                    return View(altas.ToList());
                }

            }
            
            else if (select == "Paciente")
            {
                
                var altas = db.Altas.Include(c => c.Ingresos).Where(a => a.Nombre == buscar);

                try { 
                ViewBag.total = altas.Sum(c => c.Total_Pagar);
                ViewBag.cont = altas.Count();
                ViewBag.min = altas.Min(c => c.Total_Pagar);
                ViewBag.max = altas.Max(c => c.Total_Pagar);
                ViewBag.prom = altas.Average(c => c.Total_Pagar);

                return View(altas.ToList());
                }
                catch(Exception e)
                {
                    return View(altas.ToList());
                }

            }
            return View();

        }

        /*public ActionResult Imprimir()
        {
            var print = new ActionAsPdf("Index");
            return print;
        }*/

        // GET: Altas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            return View(altas);
        }

        // GET: Altas/Create
        public ActionResult Create()
        {
            ViewBag.Ingreso_Id = new SelectList(db.Ingresos, "IdIngresos", "IdIngresos");
            return View();

        }

        // POST: Altas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAltas,Ingreso_Id,Fecha_Ingreso,Nombre,Numero,Fecha_Salida,Monto,Total_Pagar")] Altas altas)
        {
            if (ModelState.IsValid)
            {
                db.Altas.Add(altas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ingreso_Id = new SelectList(db.Ingresos, "IdIngresos", "Fecha_Ingreso", altas.Ingreso_Id);
            return View(altas);
        }

        // GET: Altas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ingreso_Id = new SelectList(db.Ingresos, "IdIngresos", "Fecha_Ingreso", altas.Ingreso_Id);
            return View(altas);
        }

        // POST: Altas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAltas,Ingreso_Id,Fecha_Ingreso,Nombre,Numero,Fecha_Salida,Monto,Total_Pagar")] Altas altas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(altas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ingreso_Id = new SelectList(db.Ingresos, "IdIngresos", "Fecha_Ingreso", altas.Ingreso_Id);
            return View(altas);
        }

        // GET: Altas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Altas altas = db.Altas.Find(id);
            if (altas == null)
            {
                return HttpNotFound();
            }
            return View(altas);
        }

        // POST: Altas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Altas altas = db.Altas.Find(id);
            db.Altas.Remove(altas);
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
        public JsonResult Nombre(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join p in db.Pacientes
                             on i.Paciente_Id equals p.IdPacientes
                             where i.IdIngresos == clavePaciente
                             select p.Nombre).ToList();
            return Json(duplicado);
        }

        public JsonResult Monto(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join h in db.Habitaciones
                             on i.Habitacion_Id equals h.IdHabitacion
                             where i.IdIngresos == clavePaciente
                             select h.Precio).ToList();
            return Json(duplicado);
        }

        public JsonResult FechaIngreso(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             where i.IdIngresos == clavePaciente
                             select i.Fecha_Ingreso).ToList();
            return Json(duplicado);
        }

        public JsonResult NumeroHabitacion(int clavePaciente)
        {

            var duplicado = (from i in db.Ingresos
                             join h in db.Habitaciones
                             on i.Habitacion_Id equals h.IdHabitacion
                             where i.IdIngresos == clavePaciente
                             select h.Numero).ToList();
            return Json(duplicado);
        }
            
    }
}
