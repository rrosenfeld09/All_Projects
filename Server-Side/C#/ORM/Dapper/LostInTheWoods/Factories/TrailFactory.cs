using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;
using LostInTheWoods.Models;
using System.Linq;

namespace LostInTheWoods.Factories
{
    public class TrailFactory 
    {
        static string server = "localhost";
        static string db = "lostinthewoods"; //Change to your schema name
        static string port = "3306"; //Potentially 8889
        static string user = "root";
        static string pass = "root";
        internal static IDbConnection Connection {
            get {
                return new MySqlConnection($"Server={server};Port={port};Database={db};UserID={user};Password={pass};SslMode=None");
            }
        }

        public void AddNewTrail(Trail trail)
        {
            using(IDbConnection dbconnection = Connection)
            {
                string query = "INSERT INTO trails (Name, Description, Length, ElevationChange, Longitude, Latitude) VALUES (@Name, @Description, @Length, @ElevationChange, @Longitude, @Latitude)";
                dbconnection.Open();
                dbconnection.Execute(query, trail);
            }
        }

        public List<Trail> GetAllTrails()
        {
            using(IDbConnection dbconnection = Connection)
            {
                using(IDbCommand command = dbconnection.CreateCommand())
                {
                    string query = "SELECT * FROM trails";
                    dbconnection.Open();
                    return dbconnection.Query<Trail>(query).ToList();
                }


            }
        }

        public Trail GetSpecificTrail(int Id)
        {
            using(IDbConnection dbconnection = Connection)
            {
                using(IDbCommand command = dbconnection.CreateCommand())
                {
                    string query = $"SELECT * FROM trails WHERE Id = {Id}";
                    dbconnection.Open();
                    return dbconnection.Query<Trail>(query).FirstOrDefault();
                }
            }
        }
         
    }
}