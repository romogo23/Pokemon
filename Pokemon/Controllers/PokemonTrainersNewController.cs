using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pokemon.Models;
using Pokemon.Models.ViewModels;

namespace Pokemon.Controllers
{
    public class PokemonTrainersNewController : Controller
    {
        private PokemonEntities db = new PokemonEntities();

        public ActionResult listaPokemon(PokemonCatched pokemonC)
        {

            return View();
        }

        // GET: PokemonTrainersNew
        public ActionResult Index()
        {
            return View(db.PokemonTrainers.ToList());
        }

        // GET: PokemonTrainersNew/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pokemon_Trainer pokemonTrainer = db.PokemonTrainers.Find(id);
            if (pokemonTrainer == null)
            {
                return HttpNotFound();
            }
            return View(pokemonTrainer);
        }

        // GET: PokemonTrainersNew/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PokemonTrainersNew/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,Region,Email")] Pokemon_Trainer pokemonTrainer, PokemonCatched pokemonC)
        {
            if (ModelState.IsValid)
            {
                //if (db.Catched_Pokemon.Find(pokemonC.Number) != null)
                //{
                //    int i = 0;
                //    return RedirectToAction("Index");
                //}
                db.PokemonTrainers.Add(pokemonTrainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pokemonTrainer);
        }

        // GET: PokemonTrainersNew/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pokemon_Trainer pokemonTrainer = db.PokemonTrainers.Find(id);
            if (pokemonTrainer == null)
            {
                return HttpNotFound();
            }
            return View(pokemonTrainer);
        }

        // POST: PokemonTrainersNew/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,Region,Email")] Pokemon_Trainer pokemonTrainer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pokemonTrainer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pokemonTrainer);
        }

        // GET: PokemonTrainersNew/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pokemon_Trainer pokemonTrainer = db.PokemonTrainers.Find(id);
            if (pokemonTrainer == null)
            {
                return HttpNotFound();
            }
            return View(pokemonTrainer);
        }

        // POST: PokemonTrainersNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pokemon_Trainer pokemonTrainer = db.PokemonTrainers.Find(id);
            db.PokemonTrainers.Remove(pokemonTrainer);
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
