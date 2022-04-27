using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class RatingProcessor
    {
        public static int CreateRating(int rating, int movie_id, int user_id)
        {
            RatingModel data = new RatingModel
            {
                rating = rating,
                movie_id = movie_id,
                user_id = user_id
            };
            string psql = @"INSERT INTO ratings (rating, movie_id, user_id) 
                            VALUES (@rating, @movie_id, @user_id);";

            return PostgresDataAccess.SaveData(psql, data);
        }

        public static int DeleteRating(int id)
        {
            RatingModel data = new RatingModel
            {
                id = id
            };
            string psql = @"DELETE FROM ratings 
                            WHERE id = @id;";

            return PostgresDataAccess.SaveData(psql, data);
        }

        public static List<RatingModel> LoadRatings()
        {
            string psql = "SELECT * FROM ratings;";

            return PostgresDataAccess.LoadData<RatingModel>(psql);
        }

        public static List<DetailedRatingModel> LoadDetailedRatings()
        {
            string psql = @"SELECT R.id, M.title, R.rating, U.name, U.surname 
                            FROM ratings R
                            JOIN users U on U.id = R.user_id
                            JOIN movies M on M.id = R.movie_id;";

            return PostgresDataAccess.LoadData<DetailedRatingModel>(psql);
        }
    }
}
