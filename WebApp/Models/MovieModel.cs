using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class MovieModel
    {
        [Display(Name = "Movie ID")]
        [Required(ErrorMessage = "You need to give us movie ID")]
        public int id { get; set; }

        [Display(Name = "Movie Title")]
        [StringLength(50, MinimumLength = 1)]
        [Required(ErrorMessage = "You need to give us movie title")]
        public string title { get; set; }

    }
}