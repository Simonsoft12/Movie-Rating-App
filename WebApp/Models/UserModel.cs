using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UserModel
    {
        [Display(Name = "User ID")]
        [Required(ErrorMessage = "You need to give us user ID")]
        public int id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(10, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required(ErrorMessage = "You need to give us your first name")]
        public string name { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(10, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Required(ErrorMessage = "You need to give us your last name")]
        public string surname { get; set; }
    }
}