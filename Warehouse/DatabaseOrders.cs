using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Warehouse
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
    }

    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
    }

    public class DatabaseOrders
    {
        // Строка подключения к базе данных. Она берется из вспомогательного класса ConnectionHelper.
        private string connectionString = ConnectionHelper.GetConnectionString();

        // Метод для получения всех заказов в виде DataTable.
        public DataTable GetAllOrders()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT OrderID, SupplierID, UserID, ProductID, Amount, OrderDate FROM Orders";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при загрузке заказов: " + ex.Message);
                }
            }
        }

        // Метод для получения названия поставщика по его идентификатору (supplierID).
        public string GetSupplierNameById(int supplierID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Name FROM Suppliers WHERE SupplierID = @SupplierID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SupplierID", supplierID);
                    object result = command.ExecuteScalar();

                    return result != null ? result.ToString() : string.Empty;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при получении названия поставщика: " + ex.Message);
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

        // Метод для удаления заказа по его идентификатору (orderID).
        public bool DeleteOrder(int orderID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Orders WHERE OrderID = @OrderID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OrderID", orderID);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при удалении заказа: " + ex.Message);
                }
            }
        }

        // Метод для добавления нового заказа в базу данных.
        public bool AddOrder(int supplierID, int userID, int productID, decimal amount, DateTime orderDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Orders (SupplierID, UserID, ProductID, Amount, OrderDate) VALUES (@SupplierID, @UserID, @ProductID, @Amount, @OrderDate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SupplierID", supplierID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@ProductID", productID);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@OrderDate", orderDate);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при добавлении заказа: " + ex.Message);
                }
            }
        }

        // Метод для обновления информации о заказе по его идентификатору (orderID).
        public bool UpdateOrder(int orderID, int supplierID, int userID, int productID, decimal amount, DateTime orderDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Orders SET SupplierID = @SupplierID, UserID = @UserID, ProductID = @ProductID, Amount = @Amount, OrderDate = @OrderDate WHERE OrderID = @OrderID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OrderID", orderID);
                    command.Parameters.AddWithValue("@SupplierID", supplierID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@ProductID", productID);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@OrderDate", orderDate);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при обновлении заказа: " + ex.Message);
                }
            }
        }

        // Метод для получения списка поставщиков в виде DataTable.
        public DataTable GetSuppliers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT SupplierID, Name FROM Suppliers";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при получении данных о поставщиках: " + ex.Message);
                }
            }
        }

        // Метод для получения списка пользователей в виде DataTable.
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

        // Метод для получения списка продуктов в виде DataTable.
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

        // Метод для получения информации о заказе по его идентификатору (orderID) в виде DataTable.
        public DataTable GetOrderById(int orderID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT OrderID, SupplierID, UserID, ProductID, Amount, OrderDate FROM Orders WHERE OrderID = @OrderID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@OrderID", orderID);

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