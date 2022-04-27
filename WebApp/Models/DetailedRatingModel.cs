using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class DetailedRatingModel
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

        [Display(Name = "Movie Title")]
        [StringLength(40, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required(ErrorMessage = "You need to give us movie title")]
        public string title { get; set; }

        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required(ErrorMessage = "You need to give us first name")]
        public string name { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required(ErrorMessage = "You need to give us last name")]
        public string surname { get; set; }
    }
}