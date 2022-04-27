using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuperSocket.SocketBase;
using SuperWebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Script.Serialization;
using static DataLibrary.BusinessLogic.DataProcessor;
using static DataLibrary.BusinessLogic.UserProcessor;
using static DataLibrary.BusinessLogic.MovieProcessor;
using static DataLibrary.BusinessLogic.RatingProcessor;
using System.Text.Json;
using DataLibrary.Models;

namespace WebApp
{
    public class Websockets
    {
        private static WebSocketServer wsServer;
        private static bool i = true;
        public Websockets()
        {
            wsServer = new WebSocketServer(); 
            int port = 8080;
            wsServer.Setup(port);
            wsServer.NewSessionConnected += NewSessionConnected;
            wsServer.NewMessageReceived += NewMessageReceived;
            wsServer.NewDataReceived += NewDataReceived;
            wsServer.SessionClosed += SessionClosed;
            wsServer.Start();
            Console.WriteLine("Server is running on port " + port + ". Press Enter to exit...");
        }
        private static void NewSessionConnected(WebSocketSession session)
        {
            Console.WriteLine("NewSession Connected");
        }

        private static void SessionClosed(WebSocketSession session, CloseReason value)
        {
            Console.WriteLine("Session Closed");
        }
        private static void NewDataReceived(WebSocketSession session, byte[] data)
        {
            Console.WriteLine("New Data Received");
        }

        private static void NewMessageReceived(WebSocketSession session, string data)
        {
            dynamic message = JObject.Parse(data);

            if (message.msg_code == "ViewTables")
            {
                HandleViewTables(session);
            }
            else if (message.msg_code == "ViewTable") 
            { 
                HandleViewTable(session, message.data.ToString());
            }
            else if (message.msg_code == "DeleteUser")
            {
                int id = message.id;
                HandleDeleteUser(session, id);
            }
            else if (message.msg_code == "DeleteRating")
            {
                int id = message.id;
                HandleDeleteRating(session, id);
            }
            else if (message.msg_code == "DeleteMovie")
            {
                int id = message.id;
                HandleDeleteMovie(session, id);
            }
            else if (message.msg_code == "AddUser")
            {
                string name = message.name;
                string surname = message.surname;
                HandleAddUser(session, name, surname);
            }
            else if (message.msg_code == "AddMovie")
            {
                string title = message.title;
                HandleAddMovie(session, title);
            }
            else if (message.msg_code == "AddRating")
            {
                int rating = message.rating;
                int movie_id = message.movie_id;
                int user_id = message.user_id;
                HandleAddRating(session, rating, movie_id, user_id);
            }
            else if (message.msg_code == "EditUser")
            {
                int id = message.id;
                string name = message.name;
                string surname = message.surname;
                HandleEditUser(session, id, name, surname);
            }
            else if (message.msg_code == "EditMovie")
            {
                int id = message.id;
                string title = message.title;
                HandleEditMovie(session, id, title);
            }
            
        }
        private static void HandleAddRating(WebSocketSession session, int rating, int movie_id, int user_id)
        {
            CreateRating(rating, movie_id, user_id);
            session.Send(new JavaScriptSerializer().Serialize("Rating " + rating + " has been added with movie ID : " + movie_id + " and user ID : " + user_id));
        }

        private static void HandleEditUser(WebSocketSession session, int id, string name, string surname)
        {
            UpdateUser(id, name, surname);
            session.Send(new JavaScriptSerializer().Serialize("User with ID : " + id + " has been edited to : " + name + ' ' + surname));
        }

        private static void HandleEditMovie(WebSocketSession session, int id, string title)
        {
            UpdateMovie(id, title);
            session.Send(new JavaScriptSerializer().Serialize("Movie with ID : " + id + " has been edited to : " + title));
        }

        private static void HandleAddMovie(WebSocketSession session, string title)
        {
            CreateMovie(title);
            session.Send(new JavaScriptSerializer().Serialize("Movie " + title + " has been created"));
        }

        private static void HandleAddUser(WebSocketSession session, string name, string surname)
        {
            CreateUser(name, surname);
            session.Send(new JavaScriptSerializer().Serialize("User " + name + ' ' + surname + " has been created"));
        }

        private static void HandleDeleteUser(WebSocketSession session, int id)
        {
            DeleteUser(id);
            session.Send(new JavaScriptSerializer().Serialize("User with ID : " + id + " has been deleted"));
        }

        private static void HandleDeleteMovie(WebSocketSession session, int id)
        {
            DeleteMovie(id);
            session.Send(new JavaScriptSerializer().Serialize("Movie with ID : " + id + " has been deleted"));
        }

        private static void HandleDeleteRating(WebSocketSession session, int id)
        {
            DeleteRating(id);
            session.Send(new JavaScriptSerializer().Serialize("Rating with ID : " + id + " has been deleted"));
        }
        private static void HandleViewTable(WebSocketSession session, string data)
        {
            if (data == "users")
                HandleViewUsers(session);
            else if (data == "movies")
                HandleViewMovies(session);
            else if (data == "ratings")
                HandleViewRatings(session);
        }

        private static void HandleViewRatings(WebSocketSession session)
        {
            session.Send(new JavaScriptSerializer().Serialize(new RatingsWithMsgcode("ViewRatings", LoadDetailedRatings())));
        }

        private static void HandleViewMovies(WebSocketSession session)
        {
            session.Send(new JavaScriptSerializer().Serialize(new MoviesWithMsgCode("ViewMovies", LoadMovies())));
        }

        private static void HandleViewUsers(WebSocketSession session)
        {
            session.Send(new JavaScriptSerializer().Serialize(new UsersWithMsgCode("ViewUsers", LoadUsers())));
        }

        private static void HandleViewTables(WebSocketSession session)
        {
            session.Send(new JavaScriptSerializer().Serialize(new TablesWithMsgCode("ViewTables", LoadTables())));
        }
        class UsersWithMsgCode { 
            public string msg_code { get; set; }
            public IList<UserModel> Users { get; set; }
            public UsersWithMsgCode(string msg_code, IList<UserModel> users)
            {
                this.msg_code = msg_code;
                this.Users = users;
            }
        }

        class MoviesWithMsgCode
        {
            public string msg_code { get; set; }
            public IList<MovieModel> Movies { get; set; }
            public MoviesWithMsgCode(string msg_code, IList<MovieModel> movies)
            {
                this.msg_code = msg_code;
                this.Movies = movies;
            }
        }

        class RatingsWithMsgcode
        {
            public string msg_code { get; set; }
            public IList<DetailedRatingModel> Ratings { get; set; }
            public RatingsWithMsgcode(string msg_code, IList<DetailedRatingModel> ratings)
            {
                this.msg_code = msg_code;
                this.Ratings = ratings;
            }
        }

        class TablesWithMsgCode
        {
            public string msg_code { get; set; }
            public IList<string> tableName { get; set; }
            public TablesWithMsgCode(string msg_code, IList<string> tableName)
            {
                this.msg_code = msg_code;
                this.tableName = tableName;
            }
        }
    }
}