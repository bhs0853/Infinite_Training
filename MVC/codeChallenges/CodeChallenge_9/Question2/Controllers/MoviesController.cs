using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Question2.Models;

namespace Question2.Controllers
{
    public class MoviesController : Controller
    {
        private MoviesContext db = new MoviesContext();

        // GET: Movies
        public ActionResult Index()
        {
            return View(db.movies.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movies movies)
        {
            if (ModelState.IsValid)
            {
                db.movies.Add(movies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movies);
        }

        public ActionResult Edit(long id)
        {
            Movies movies = db.movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movies movies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movies);
        }

        public ActionResult Delete(long id)
        {
            Movies movies = db.movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Movies movies = db.movies.Find(id);
            db.movies.Remove(movies);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetMovieByYear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMovieByYear(string year)
        {
            DateTime releaseYear = Convert.ToDateTime(year);
            var movieList = db.movies.Where(m => m.DateofRelease == releaseYear).ToList();

            return View("MovieList", movieList);
        }
        public ActionResult GetMovieByDirector()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMovieByDirector(string director)
        {
            var movieList = db.movies.Where(m => m.DirectorName == director).ToList();
            return View("MovieList", movieList);
        }
    }
}
