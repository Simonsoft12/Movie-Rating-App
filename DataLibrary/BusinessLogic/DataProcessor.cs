using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class DataProcessor
    {
        public static List<string> LoadTables()
        {
            string psql = @"SELECT table_name 
                            FROM information_schema.tables
                            WHERE table_schema = 'public' AND table_type = 'BASE TABLE';";
            return PostgresDataAccess.LoadData<string>(psql);
        }
    }
}
