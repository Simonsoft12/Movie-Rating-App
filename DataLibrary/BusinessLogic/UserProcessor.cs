using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    public static class UserProcessor
    {
        public static int CreateUser(string name, string surname)
        {
            UserModel data = new UserModel
            {
                name = name,
                surname = surname
            };
            string psql = @"INSERT INTO users (name, surname)
                            VALUES (@name, @surname);";

            return PostgresDataAccess.SaveData(psql, data);
        }

        public static int UpdateUser(int id, string name, string surname)
        {
            UserModel data = new UserModel
            {
                id = id,
                name = name,
                surname = surname
            };
            string psql = @"UPDATE users  
                            SET    name = @name,  
                                   surname = @surname   
                            WHERE  id = @id;";

            return PostgresDataAccess.SaveData(psql, data);
        }

        public static int DeleteUser(int id)
        {
            UserModel data = new UserModel
            {
                id = id
            };
            string psql = @"DELETE FROM users  
                            WHERE  id = @id;";

            return PostgresDataAccess.SaveData(psql, data);
        }

        public static List<UserModel> LoadUsers()
        {
            string psql = "SELECT * FROM users;";

            return PostgresDataAccess.LoadData<UserModel>(psql);
        }

        public static List<UserModel> LoadUsers(int id)
        {
            string psql = @"SELECT * FROM users 
                            WHERE id = '" + id + "';";

            return PostgresDataAccess.LoadData<UserModel>(psql);
        }
    }
}
