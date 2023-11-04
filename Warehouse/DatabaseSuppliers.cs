using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Warehouse
{
    public class DatabaseSuppliers
    {   
        private string connectionString = ConnectionHelper.GetConnectionString();

        public List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT SupplierID, Name FROM Suppliers";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Supplier supplier = new Supplier
                        {
                            SupplierID = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        suppliers.Add(supplier);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при получении списка поставщиков: " + ex.Message);
                }
            }

            return suppliers;
        }


        public DataTable GetAllSuppliers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT SupplierID, Name, Phone FROM Suppliers";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок подключения или запроса
                    throw new Exception("Ошибка при загрузке поставщиков: " + ex.Message);
                }
            }
        }

        public bool DeleteSupplier(int supplierID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SupplierID", supplierID);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при удалении поставщика: " + ex.Message);
                }
            }
        }

        public bool AddSupplier(string name, string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Suppliers (Name, Phone) VALUES (@Name, @Phone)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone", phone);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка при добавлении поставщика: " + ex.Message);
                }
            }
        }

        public bool UpdateSupplier(int supplierID, string name, string phone)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Suppliers SET Name = @Name, Phone = @Phone WHERE SupplierID = @SupplierID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SupplierID", supplierID);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Phone", phone);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    throw new Exception("Ошибка при обновлении поставщика: " + ex.Message);
                }
            }
        }
    }
}
