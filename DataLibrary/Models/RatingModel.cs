using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class RatingModel
    {
        public int id { get; set; }
        public int rating { get; set; }
        public int movie_id { get; set; }
        public int user_id { get; set; }
    }
}
