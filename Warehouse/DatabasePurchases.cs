using System;
using System.Data;
using System.Data.SqlClient;

namespace Warehouse
{
    public class DatabasePurchases
    {
        // Строка подключения к базе данных. Она берется из вспомогательного класса ConnectionHelper.
        private string connectionString = ConnectionHelper.GetConnectionString();

        // Метод для получения всех записей о покупках из базы данных.
        public DataTable GetAllPurchases()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                try
                {
                    connection.Open();
                    string query = "SELECT PurchaseID, ProductID, UserID, Quantity, Amount, PurchaseDate FROM Purchases";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при загрузке покупок: " + ex.Message);
                }
            }
        }

        // Метод для получения имени пользователя по его идентификатору (userID).
        public string GetUserNameById(int userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Name FROM Users WHERE UserID = @UserID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserID", userID);
                    object result = command.ExecuteScalar();

                    return result != null ? result.ToString() : string.Empty;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при получении названия пользователя: " + ex.Message);
                }
            }
        }

        // Метод для получения названия продукта по его идентификатору (productID).
        public string GetProductNameById(int productID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Name FROM Products WHERE ProductID = @ProductID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductID", productID);
                    object result = command.ExecuteScalar();

                    return result != null ? result.ToString() : string.Empty;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при получении названия продукта: " + ex.Message);
                }
            }
        }

        // Метод для удаления записи о покупке по ее идентификатору (purchaseID).
        public bool DeletePurchase(int purchaseID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Purchases WHERE PurchaseID = @PurchaseID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PurchaseID", purchaseID);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при удалении покупки: " + ex.Message);
                }
            }
        }

        // Метод для добавления новой записи о покупке в базу данных.
        public bool AddPurchase(int productID, int userID, int quantity, decimal amount, DateTime purchaseDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Purchases (ProductID, UserID, Quantity, Amount, PurchaseDate) VALUES (@ProductID, @UserID, @Quantity, @Amount, @PurchaseDate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductID", productID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@PurchaseDate", purchaseDate);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при добавлении покупки: " + ex.Message);
                }
            }
        }

        // Метод для обновления записи о покупке по ее идентификатору (purchaseID).
        public bool UpdatePurchase(int purchaseID, int productID, int userID, int quantity, decimal amount, DateTime purchaseDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Purchases SET ProductID = @ProductID, UserID = @UserID, Quantity = @Quantity, Amount = @Amount, PurchaseDate = @PurchaseDate WHERE PurchaseID = @PurchaseID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PurchaseID", purchaseID);
                    command.Parameters.AddWithValue("@ProductID", productID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@PurchaseDate", purchaseDate);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при обновлении покупки: " + ex.Message);
                }
            }
        }

        // Метод для получения списка пользователей.
        public DataTable GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT UserID, Name FROM Users";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при получении данных о пользователях: " + ex.Message);
                }
            }
        }

        // Метод для получения списка продуктов.
        public DataTable GetProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ProductID, Name FROM Products";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при получении данных о продуктах: " + ex.Message);
                }
            }
        }

        // Метод для получения информации о покупке по ее идентификатору (purchaseID).
        public DataTable GetPurchaseById(int purchaseID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT PurchaseID, ProductID, UserID, Quantity, Amount, PurchaseDate FROM Purchases WHERE PurchaseID = @PurchaseID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@purchaseID", purchaseID);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при загрузке данных заказа: " + ex.Message);
                }
            }
        }
    }
}
