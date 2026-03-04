using Microsoft.Data.SqlClient;
using Ms_Auth.Models;

namespace Ms_Auth.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public bool PersistUser(User user) {
            SqlConnection conn = new SqlConnection("data source= .\\Sqlexpress ;" +
                "initial catalog = MsEx;" +
                "integrated security = true;" +
                "encrypt = false;");
            conn.Open();
            string query = "insert into users(id , fullName , email , password) values (@id , @fullName , @email , @password)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@id", user.Id));
            cmd.Parameters.Add(new SqlParameter("@fullName", user.FullName));
            cmd.Parameters.Add(new SqlParameter("@email", user.Email));
            cmd.Parameters.Add(new SqlParameter("@password", user.Password));
            int isRowInserted = cmd.ExecuteNonQuery();
            conn.Close();
            return isRowInserted > 0;
        }

    }
}
