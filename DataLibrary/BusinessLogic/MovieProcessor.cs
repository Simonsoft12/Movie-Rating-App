using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    public static class MovieProcessor
    {
        public static int CreateMovie(string title)
        {
            MovieModel data = new MovieModel
            {
                title = title
            };
            string psql = @"INSERT INTO movies (title) 
                            VALUES (@title);";

            return PostgresDataAccess.SaveData(psql, data);
        }

        public static int UpdateMovie(int id, string title)
        {
            MovieModel data = new MovieModel
            {
                id = id,
                title = title
            };
            string psql = @"UPDATE movies  
                            SET    title = @title
                            WHERE  id = @id;";

            return PostgresDataAccess.SaveData(psql, data);
        }

        public static int DeleteMovie(int id)
        {
            MovieModel data = new MovieModel
            {
                id = id
            };
            string psql = @"DELETE FROM movies 
                            WHERE  id = @id;";

            return PostgresDataAccess.SaveData(psql, data);
        }

        public static List<MovieModel> LoadMovies()
        {
            string psql = "SELECT * FROM movies;";

            return PostgresDataAccess.LoadData<MovieModel>(psql);
        }

        public static List<MovieModel> LoadMovies(int id)
        {
            string psql = @"Select * FROM movies 
                            WHERE id = '" + id + "';";

            return PostgresDataAccess.LoadData<MovieModel>(psql);
        }
    }
}
