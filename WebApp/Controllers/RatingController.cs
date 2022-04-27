using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using WebApp.Models;
using static DataLibrary.BusinessLogic.RatingProcessor;
using static DataLibrary.BusinessLogic.UserProcessor;
using static DataLibrary.BusinessLogic.MovieProcessor;
using System.Configuration;
using System.Data;
using DataLibrary.Models;

namespace WebApp.Controllers
{
    public class RatingController : Controller
    {
        // GET: Rating
        public ActionResult Index(string searchBy, string search, int? sortOrder)
        {
            ViewBag.Message = "Ratings List";

            var data = LoadDetailedRatings();
            List<WebApp.Models.DetailedRatingModel> ratings = new List<WebApp.Models.DetailedRatingModel>();

            foreach (var row in data)
            {
                ratings.Add(new Models.DetailedRatingModel
                {
                    id = row.id,
                    title = row.title,
                    rating = row.rating,
                    name = row.name,
                    surname = row.surname
                });
            }

            if (search != null && search != "")
            {
                if (searchBy == "Rating ID")
                {
                    var dataList = ratings.FindAll(x => x.id == int.Parse(search));
                    return View(dataList);
                }
                if (searchBy == "Movie Title")
                {
                    var dataList = ratings.FindAll(x => x.title == search);
                    return View(dataList);
                }
                if (searchBy == "Rating")
                {
                    var dataList = ratings.FindAll(x => x.rating == int.Parse(search));
                    return View(dataList);
                }
                if (searchBy == "First Name")
                {
                    var dataList = ratings.FindAll(x => x.name == search);
                    return View(dataList);
                }
                if (searchBy == "Last Name")
                {
                    var dataList = ratings.FindAll(x => x.surname == search);
                    return View(dataList);
                }
            }

            if (sortOrder == 1)
            {
                List<Models.DetailedRatingModel> ratingsById = ratings.OrderBy(DetailedRatingModel => DetailedRatingModel.id).ToList();
                return View(ratingsById);
            }
            if (sortOrder == 2)
            {
                List<Models.DetailedRatingModel> ratingsByRating = ratings.OrderBy(DetailedRatingModel => DetailedRatingModel.rating).ToList();
                return View(ratingsByRating); 
            }
            if (sortOrder == 3)
            {
                List<Models.DetailedRatingModel> ratingsByTitle = ratings.OrderBy(DetailedRatingModel => DetailedRatingModel.title).ToList();
                return View(ratingsByTitle);
            }
            if (sortOrder == 4)
            {
                List<Models.DetailedRatingModel> ratingsByName = ratings.OrderBy(DetailedRatingModel => DetailedRatingModel.name).ToList();
                return View(ratingsByName);
            }
            if (sortOrder == 5)
            {
                List<Models.DetailedRatingModel> ratingsBySurname = ratings.OrderBy(DetailedRatingModel => DetailedRatingModel.surname).ToList();
                return View(ratingsBySurname);
            }

            return View(ratings);
        }

        // Ratings/AddRating
        public ActionResult AddRating()
        {
            WebApp.Models.RatingModel rating = new WebApp.Models.RatingModel();
            rating.Users = LoadUsers();
            rating.Movies = LoadMovies();
            List<SelectListItem> userList = new List<SelectListItem>();

            foreach (DataLibrary.Models.UserModel row in rating.Users)
            {
                userList.Add(new SelectListItem()
                {
                    Value = row.id.ToString(),
                    Text = row.name.ToString() + " " + row.surname.ToString()
                });
            }

            List<SelectListItem> movieList = new List<SelectListItem>();

            foreach (DataLibrary.Models.MovieModel row in rating.Movies)
            {
                movieList.Add(new SelectListItem()
                {
                    Value = row.id.ToString(),
                    Text = row.title.ToString()
                });
            }
            ViewBag.user_id = userList;
            ViewBag.movie_id = movieList;

            return View(rating);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRating(Models.RatingModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = CreateRating(
                    model.rating,
                    model.movie_id,
                    model.user_id
                    );
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            DeleteRating(id);

            return RedirectToAction("Index");
        }
    }
}