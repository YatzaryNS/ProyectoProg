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
    public class PacientesController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Pacientes
        public ActionResult Index()
        {
            return View(db.Pacientes.ToList());
        }

        [HttpPost]
        public ActionResult Index(string select, string buscar)
        {
            if (select == "Nombre")
            {
                var datos = from d in db.Pacientes
                           select d;

                datos = datos.Where(a => a.Nombre.Contains(buscar));

                return View(datos);

            }
            else if (select == "Asegurado")
            {
                if (buscar == "Si" || buscar == "si" || buscar == "SI" || buscar == "sI")
                {
                    var datos = from d in db.Pacientes
                               where d.Asegurado.Equals(true)
                               select d;

                    return View(datos);

                }
                else if (buscar == "No" || buscar == "no" || buscar == "NO" || buscar == "nO")
                {
                    var datos = from d in db.Pacientes
                               where d.Asegurado.Equals(false)
                               select d;

                    return View(datos);
                }
            }
            else if (select == "Cedula")
            {

                var datos = from d in db.Pacientes
                           select d;

                datos = datos.Where(a => a.Cedula.Contains(buscar));
                return View(datos);

            }
            return View();

        }

        /*public ActionResult Imprimir()
        {
            var print = new ActionAsPdf("Index");
            return print;
        }*/ 

        // GET: Pacientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // GET: Pacientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPacientes,Cedula,Nombre,Asegurado")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                db.Pacientes.Add(pacientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pacientes);
        }

        // GET: Pacientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPacientes,Cedula,Nombre,Asegurado")] Pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacientes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pacientes);
        }

        // GET: Pacientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacientes pacientes = db.Pacientes.Find(id);
            if (pacientes == null)
            {
                return HttpNotFound();
            }
            return View(pacientes);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacientes pacientes = db.Pacientes.Find(id);
            db.Pacientes.Remove(pacientes);
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
