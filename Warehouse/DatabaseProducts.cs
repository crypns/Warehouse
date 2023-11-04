using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Warehouse
{
    public class DatabaseProducts
    {
        // Строка подключения к базе данных. Она берется из вспомогательного класса ConnectionHelper.
        private string connectionString = ConnectionHelper.GetConnectionString();

        // Метод для получения списка всех продуктов из базы данных.
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ProductID, Name, Manufacturer, Article FROM Products";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                        };
                        products.Add(product);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при получении списка продуктов: " + ex.Message);
                }
            }

            return products;
        }

        // Метод для получения всех продуктов в виде DataTable.
        public DataTable GetAllProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ProductID, Name, Quantity, Price, Manufacturer, Article FROM Products";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при загрузке товаров: " + ex.Message);
                }
            }
        }

        // Метод для удаления продукта по его идентификатору (productID).
        public bool DeleteProduct(int productID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductID", productID);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при удалении товара: " + ex.Message);
                }
            }
        }

        // Метод для добавления нового продукта в базу данных.
        public bool AddProduct(string name, int quantity, decimal price, string manufacturer, int article)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Products (Name, Quantity, Price, Manufacturer, Article) VALUES (@Name, @Quantity, @Price, @Manufacturer, @Article)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Manufacturer", manufacturer);
                    command.Parameters.AddWithValue("@Article", article);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при добавлении товара: " + ex.Message);
                }
            }
        }

        // Метод для обновления информации о продукте по его идентификатору (productID).
        public bool UpdateProduct(int productID, string name, int quantity, decimal price, string manufacturer, int article)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Products SET Name = @Name, Quantity = @Quantity, Price = @Price, Manufacturer = @Manufacturer, Article = @Article WHERE ProductID = @ProductID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductID", productID);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Manufacturer", manufacturer);
                    command.Parameters.AddWithValue("@Article", article);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при обновлении товара: " + ex.Message);
                }
            }
        }
    }
}
