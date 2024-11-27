 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Test2.Model
{
    public class Database
    {
        private readonly string _connetionString = "Server = (local); Database = Login; Trusted_Connection = True";

        public bool CheckLogin(string username, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connetionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Login WHERE username = @Username  AND  password = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка проверки логина: {ex.Message}");
                return false;
            }
        }

        public DataTable GetUsers()
        {
            DataTable datatable = new DataTable();

            try
            {
                using(SqlConnection connection = new SqlConnection(_connetionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM login";
                    using(SqlCommand command = new SqlCommand(query,connection))
                        using(SqlDataAdapter adapter = new SqlDataAdapter(command))
                          adapter.Fill(datatable);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Ошибка загрузки данных из базы данных ");
            }
            return datatable;
        }


        


        public void GetUser(string username, string password)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(_connetionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Login (username, password) VALUES (@Username, @Password)";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка добалвения пользователя: {ex.Message}");
                throw;
            }
        }

        public void UpdateUser(int id, string username, string password)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(_connetionString))
                {
                    connection.Open();

                    string query = "UPDATE Login SET username = @Username, password = @Password WHERE id = @Id";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка редактирования данных пользователя: {ex.Message}");
                throw;
            }
        }


        public void DeleteUser(int id)
        {
            try
            {
              using(SqlConnection connection = new SqlConnection(_connetionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Login WHERE id = @Id";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Ошибка удаления пользователя: {ex.Message}");
                throw;
            }
        }







    }
}
