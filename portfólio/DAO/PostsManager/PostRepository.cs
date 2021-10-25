using portfólio.Models;
using portfólio.ModelViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace portfólio.DAO.PostManager
{
    public class PostRepository : IPostRepository
    {
        public readonly IDbConnection DbConnection;
        public PostRepository()
        {
            this.DbConnection = new SqlConnection("Server=localhost;database=portfolio;user=sa;password=****");
            this.DbConnection.Open();
        }

        public RegisterViewModel Register(RegisterViewModel entity)
        {
            IDbCommand insert = DbConnection.CreateCommand();
            insert.CommandText = "INSERT INTO Users (Username, Email, Password, Photo) VALUES (@Username, @Email, @Password, @Photo);";

            IDbDataParameter paramUsername = new SqlParameter("Username", entity.Username);
            IDbDataParameter paramEmail = new SqlParameter("Email", entity.Email);
            IDbDataParameter paramPassword = new SqlParameter("Password", entity.Password);
            IDbDataParameter paramPhoto = new SqlParameter("Photo", entity.Photo);

            insert.Parameters.Add(paramUsername);
            insert.Parameters.Add(paramEmail);
            insert.Parameters.Add(paramPassword);
            insert.Parameters.Add(paramPhoto);

            var rowsAffected = insert.ExecuteNonQuery();
            DbConnection.Close();

            if (rowsAffected > 0)
                return entity;
            return default;
        }

        public Post CreatePost(Post post)
        {
            IDbCommand insert = DbConnection.CreateCommand();
            insert.CommandText = "INSERT INTO Posts (Subtitle, Created, Image, UserID) VALUES (@Subtitle, @Created, @Image, @UserID);";

            IDbDataParameter paramSubtitle = new SqlParameter("Subtitle", post.Subtitle);
            IDbDataParameter paramCreated = new SqlParameter("Created", post.Created);
            IDbDataParameter paramImage = new SqlParameter("Image", post.Image);
            IDbDataParameter paramUserID = new SqlParameter("UserID", post.UserID);

            insert.Parameters.Add(paramSubtitle);
            insert.Parameters.Add(paramCreated);
            insert.Parameters.Add(paramImage);
            insert.Parameters.Add(paramUserID);

            var rowsAffected = insert.ExecuteNonQuery();
            DbConnection.Close();

            if (rowsAffected > 0)
                return post;
            return default;
        }

        public IEnumerable<Post> GetPosts()
        {
            ICollection<Post> list = new Collection<Post>();

            IDbCommand select = DbConnection.CreateCommand();
            select.CommandText = "SELECT * FROM Posts";

            using (var reader = select.ExecuteReader())
            {
                while (reader.Read())
                    list.Add(new Post()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Subtitle = Convert.ToString(reader["Subtitle"]),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Image = Convert.ToString(reader["Image"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                DbConnection.Close();
                return list;
            }
        }
        public Post GetPost(int id)
        {
            var select = DbConnection.CreateCommand();
            select.CommandText = "SELECT * FROM Posts WHERE Id = @id;";

            IDataParameter paramId = new SqlParameter("Id", id);
            select.Parameters.Add(paramId);

            Post post = null;

            using (var reader = select.ExecuteReader())
                if (reader.Read())
                    post = new Post()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Subtitle = Convert.ToString(reader["Subtitle"]),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Image = Convert.ToString(reader["Image"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
            return post;
        }

        public IEnumerable<Post> MyPosts(string userID)
        {
            ICollection<Post> list = new Collection<Post>();

            IDbCommand select = DbConnection.CreateCommand();
            select.CommandText = "SELECT * FROM Posts WHERE UserID = @userID";

            IDbDataParameter paramUserID = new SqlParameter("@userID", userID);
            select.Parameters.Add(paramUserID);

            using (var reader = select.ExecuteReader())
            {
                while (reader.Read())
                    list.Add(new Post()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Subtitle = Convert.ToString(reader["Subtitle"]),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Image = Convert.ToString(reader["Image"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    });
                DbConnection.Close();
                return list;
            }
        }
        public bool DeletePost(Post post)
        {
            IDbCommand remove = DbConnection.CreateCommand();
            remove.CommandText = "DELETE FROM Posts WHERE Id = @id;";

            IDataParameter paramId = new SqlParameter("Id", post.ID);
            remove.Parameters.Add(paramId);

            var rowsAffected = remove.ExecuteNonQuery();

            DbConnection.Close();

            return rowsAffected > 0;

        }

        public Post UpdatePost(Post post)
        {
            IDbCommand update = DbConnection.CreateCommand();
            update.CommandText = "UPDATE Posts SET Subtitle = @subtitle, Image = @image WHERE Id = @id;";

            IDbDataParameter paramId = new SqlParameter("Id", post.ID);
            IDbDataParameter paramSubtitle = new SqlParameter("Subtitle", post.Subtitle);
            IDbDataParameter paramImage = new SqlParameter("Image", post.Image);

            update.Parameters.Add(paramId);
            update.Parameters.Add(paramSubtitle);
            update.Parameters.Add(paramImage);

            var rowsAffected = update.ExecuteNonQuery();

            if (rowsAffected > 0)
                return post;

            return default;
        }

        public string Username(int userID)
        {
            var select = DbConnection.CreateCommand();
            select.CommandText = "SELECT Username FROM Users WHERE Id = @postUserID;";

            IDataParameter paramPostUserID = new SqlParameter("Id", userID);
            select.Parameters.Add(paramPostUserID);

            User user = null;

            using (var reader = select.ExecuteReader())
                if (reader.Read())
                    user = new User()
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Username = Convert.ToString(reader["Username"]),
                        Email = Convert.ToString(reader["Email"]),
                        Password = Convert.ToString(reader["Password"]),
                        Photo = Convert.ToString(reader["Photo"])
                    };
            return user.Username;
        }

    }
}
