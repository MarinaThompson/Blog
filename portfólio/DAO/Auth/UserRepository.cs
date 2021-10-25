using portfólio.DAO.RepositoryUser;
using portfólio.Models;
using portfólio.ModelViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace portfólio.DAO
{
    public class UserRepository : IUserRepository
    {
       public readonly IDbConnection DbConnection;
       public UserRepository()
        {
            this.DbConnection = new SqlConnection("Server=localhost;database=portfolio;user=sa;password=****");
            this.DbConnection.Open();
        }

        public User Login(string email, string password)
        {
            IDbCommand auth = DbConnection.CreateCommand();
            auth.CommandText = "SELECT * FROM Users WHERE Email = @email AND Password = @password";

            IDbDataParameter paramEmail = new SqlParameter("@email", email);
            IDbDataParameter paramPassword = new SqlParameter("@password", password);

            auth.Parameters.Add(paramEmail);
            auth.Parameters.Add(paramPassword);


            using (var reader = auth.ExecuteReader())
                if (reader.Read())
                {
                 User user = new User()
                    {
                        ID = Convert.ToInt32(reader["id"]),
                        Username = Convert.ToString(reader["username"]),
                        Email = Convert.ToString(reader["email"]),
                        Password = Convert.ToString(reader["password"])                        
                    };
                    return user;
                }                  
            return null;
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

    }
}
