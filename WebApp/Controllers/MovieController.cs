using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using WebApp.Models;
using static DataLibrary.BusinessLogic.MovieProcessor;
using System.Configuration;
using System.Data;

namespace WebApp.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index(Models.MovieModel model, string searchBy, string search, int? sortOrder)
        {
            ViewBag.Message = "Movies List";

            var data = LoadMovies();
            List<Models.MovieModel> movies = new List<Models.MovieModel>();

            foreach (var row in data)
            {
                movies.Add(new Models.MovieModel
                {
                    id = row.id,
                    title = row.title
                });
            }

            if (search != null && search != "")
            {
                if (searchBy == "Movie ID")
                {
                    var dataList = movies.FindAll(x => x.id == int.Parse(search));
                    return View(dataList);
                }
                if (searchBy == "Movie Title")
                {
                    var dataList = movies.FindAll(x => x.title == search);
                    return View(dataList);
                }
            }

            if (sortOrder == 1)
            {
                List<Models.MovieModel> moviesById = movies.OrderBy(MovieModel => MovieModel.id).ToList();
                return View(moviesById);
            }
            if (sortOrder == 2)
            {
                List<Models.MovieModel> movieByTitle = movies.OrderBy(MovieModel => MovieModel.title).ToList();
                return View(movieByTitle);
            }

            return View(movies);
        }

        // Movies/AddMovie
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMovie(Models.MovieModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = CreateMovie(
                    model.title);
                return RedirectToAction("Index");
            }
            return View();
        }


        // Movies/Edit/1
        public ActionResult Edit(int id)
        {
            var data = LoadMovies(id);
            List<Models.MovieModel> movies = new List<Models.MovieModel>();

            foreach (var row in data)
            {
                movies.Add(new Models.MovieModel
                {
                    id = row.id,
                    title = row.title
                });
            }
            MovieModel movie = new MovieModel();
            movie = movies.First();

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.MovieModel model)
        {
            if (model == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                int recordsCreated = UpdateMovie(
                    model.id,
                    model.title);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            DeleteMovie(id);

            return RedirectToAction("Index");
        }
    }
}