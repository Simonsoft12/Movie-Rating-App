using DataLibrary;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RatingModel
    {
        public List<DataLibrary.Models.UserModel> Users { get; internal set; }
        public List<DataLibrary.Models.MovieModel> Movies { get; internal set; }

        [Display(Name = "Rating ID")]
        [Required(ErrorMessage = "You need to give us rating ID")]
        public int id { get; set; }

        [Display(Name = "Rating")]
        [Range(0, 100)]
        [Required(ErrorMessage = "You need to give us rating")]
        public int rating { get; set; }

        [Display(Name = "Movie ID")]
        [Required(ErrorMessage = "You need to give us movie ID")]
        public int movie_id { get; set; }

        [Display(Name = "User ID")]
        [Required(ErrorMessage = "You need to give us user ID")]
        public int user_id { get; set; }

    }
}