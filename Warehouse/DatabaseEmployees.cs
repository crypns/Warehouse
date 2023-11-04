using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Warehouse
{
    public class DatabaseEmployees
    {
        // Строка подключения к базе данных. Она берется из вспомогательного класса ConnectionHelper.
        private string connectionString = ConnectionHelper.GetConnectionString();

        // Метод для получения списка пользователей в виде списка объектов User.
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT UserID, Name FROM Users";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserID = reader.GetInt32(0),
                            Name = reader.GetString(1)
                            // Здесь можно добавить другие свойства, если они есть в таблице Users
                        };
                        users.Add(user);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при получении списка пользователей: " + ex.Message);
                }
            }

            return users;
        }

        // Метод для получения всех пользователей в виде DataTable.
        public DataTable GetAllUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT UserID, Login, Password, Name, Phone, AccessLevel FROM Users";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при загрузке пользователей: " + ex.Message);
                }
            }
        }

        // Метод для удаления пользователя по его идентификатору (userID).
        public bool DeleteUser(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Users WHERE UserID = @UserID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userID);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при удалении пользователя: " + ex.Message);
                }
            }
        }

        // Метод для добавления нового пользователя в базу данных.
        public bool AddUser(string login, string password, string name, string phone, int accessLevel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Login, Password, Name, Phone, AccessLevel) VALUES (@Login, @Password, @Name, @Phone, @AccessLevel)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@AccessLevel", accessLevel);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при добавлении пользователя: " + ex.Message);
                }
            }
        }

        // Метод для обновления информации о пользователе по его идентификатору (userID).
        public bool UpdateUser(int userID, string login, string password, string name, string phone, int accessLevel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Users SET Login = @Login, Password = @Password, Name = @Name, Phone = @Phone, AccessLevel = @AccessLevel WHERE UserID = @UserID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@AccessLevel", accessLevel);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при обновлении пользователя: " + ex.Message);
                }
            }
        }
    }
}
