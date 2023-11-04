using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Warehouse
{
    public class DatabaseAuth
    {
        // Строка подключения к базе данных. Она берется из вспомогательного класса ConnectionHelper.
        private string connectionString = ConnectionHelper.GetConnectionString();

        // Метод для аутентификации пользователя по его имени пользователя (username) и паролю (password).
        public int AuthenticateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT AccessLevel FROM Users WHERE Login COLLATE Latin1_General_CS_AS = @Username AND Password COLLATE Latin1_General_CS_AS = @Password"; SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        // Если результат запроса не пустой, возвращаем уровень доступа пользователя (AccessLevel).
                        return Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибок, которые могли возникнуть при выполнении запроса.
                    throw new Exception("Ошибка при авторизации: " + ex.Message);
                }
            }
            // Если аутентификация не удалась, возвращаем -1.
            return -1;
        }

        // Метод для регистрации нового пользователя с заданными данными.
        public bool RegisterUser(string username, string password, string name, string phone, int accessLevel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Проверяем, существует ли пользователь с таким же именем (username).
                    string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Login = @Username";
                    SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection);
                    checkUserCommand.Parameters.AddWithValue("@Username", username);

                    int userCount = (int)checkUserCommand.ExecuteScalar();

                    if (userCount > 0)
                    {
                        // Если пользователь с таким именем уже существует, возвращаем false.
                        return false;
                    }

                    // Иначе, выполняем запрос на добавление нового пользователя.
                    string insertQuery = "INSERT INTO Users (Login, Password, Name, Phone, AccessLevel) VALUES (@Username, @Password, @Name, @Phone, @AccessLevel)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@AccessLevel", accessLevel);

                    int rowsAffected = command.ExecuteNonQuery();

                    // Если была добавлена хотя бы одна запись, возвращаем true.
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок, которые могли возникнуть при выполнении запроса.
                    throw new Exception("Ошибка при регистрации пользователя: " + ex.Message);
                }
            }
        }
    }
}
