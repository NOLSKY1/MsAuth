using Microsoft.Data.SqlClient;
using Ms_Auth.Models;

namespace Ms_Auth.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public bool PersistUser(User user) {
            SqlConnection conn = new SqlConnection("data source= .\\Sqlexpress ;" +
                "initial catalog = todoMs;" +
                "integrated security = true;" +
                "encrypt = false;");
            conn.Open();
            string query = "insert into users(id , fullName , email , password) values (@id , @fullName , @email , @password)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@id", user.Id));
            cmd.Parameters.Add(new SqlParameter("@fullName", user.FullName));
            cmd.Parameters.Add(new SqlParameter("@email", user.Email));
            cmd.Parameters.Add(new SqlParameter("@password", user.HashedPassword));
            int isRowInserted = cmd.ExecuteNonQuery();
            conn.Close();
            return isRowInserted > 0;
        }
        public User GetUSer(string email)
        {
            SqlConnection conn = new SqlConnection("data source= .\\Sqlexpress ;" +
               "initial catalog = todoMs;" +
               "integrated security = true;" +
               "encrypt = false;");
            conn.Open();
            string query = "select * from users where email = @email";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@email", email));
            SqlDataReader rd = cmd.ExecuteReader();
            User user = null;
            while (rd.Read())
            {
                user = new User
                {
                    Id = rd["id"].ToString(),
                    FullName = rd["fullName"].ToString(),
                    Email = rd["email"].ToString(),
                    HashedPassword = rd["password"].ToString(),
                };
            }
            conn.Close();
            return user;
        }
    }
}
