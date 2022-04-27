using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Npgsql;
using System.Data.Entity;

namespace DataLibrary.DataAccess
{
    public static class PostgresDataAccess
    {
        public static List<T> LoadData<T>(string psql)
        {
            using (var cnn = new NpgsqlConnection("Server=ec2-52-31-94-195.eu-west-1.compute.amazonaws.com;Port=5432;SSLMode=Require;TrustServerCertificate=true;Database=d9mo8p5qk4e8h7;User Id=jdormfkakixhya;Password=837571041cd94940bb09dd2a1ab8778468852b8782e5171a29ceaef8e9dfcfd3;"))
            {
                return cnn.Query<T>(psql).ToList();
            }
        }

        public static int SaveData<T>(string psql, T data)
        {
            using (IDbConnection cnn = new NpgsqlConnection("Server=ec2-52-31-94-195.eu-west-1.compute.amazonaws.com;Port=5432;SSLMode=Require;TrustServerCertificate=true;Database=d9mo8p5qk4e8h7;User Id=jdormfkakixhya;Password=837571041cd94940bb09dd2a1ab8778468852b8782e5171a29ceaef8e9dfcfd3;"))
            {
                return cnn.Execute(psql, data);
            }
        }
    }
}

