using System;
using System.Data;
using System.Linq;
using System.Windows;
using Warehouse;

namespace Warehouse
{
    public partial class SupplierEditWindow : Window
    {
        private DatabaseSuppliers databaseSuppliers;
        private int supplierID;

        public SupplierEditWindow(int supplierID)
        {
            InitializeComponent();
            this.supplierID = supplierID;
            databaseSuppliers = new DatabaseSuppliers();

            // Загрузка данных поставщика для редактирования
            LoadSupplierData();
        }

        private void LoadSupplierData()
        {
            try
            {
                DataTable supplierDataTable = databaseSuppliers.GetAllSuppliers();

                foreach (DataRow row in supplierDataTable.Rows)
                {
                    if (Convert.ToInt32(row["SupplierID"]) == supplierID)
                    {
                        NameTextBox.Text = row["Name"].ToString();
                        PhoneTextBox.Text = row["Phone"].ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных поставщика: " + ex.Message);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string phone = PhoneTextBox.Text;

            // Проверяем, что имя поставщика не пусто и не слишком длинное (не более 255 символов)
            if (string.IsNullOrEmpty(name) || name.Length > 255)
            {
                MessageBox.Show("Пожалуйста, введите корректное имя поставщика (не более 255 символов).");
                return;
            }

            // Проверяем, что номер телефона не слишком длинный (не более 20 символов)
            if (!string.IsNullOrEmpty(phone) && phone.Length > 20)
            {
                MessageBox.Show("Номер телефона слишком длинный (не более 20 символов).");
                return;
            }

            // Дополнительная валидация полей:

            // Проверяем, что имя содержит только буквы и цифры
            if (!IsValidName(name))
            {
                MessageBox.Show("Некорректное имя поставщика. Пожалуйста, используйте только буквы и цифры.");
                return;
            }

            // Проверяем, что номер телефона состоит только из цифр и символов '-'
            if (!IsValidPhone(phone))
            {
                MessageBox.Show("Некорректный номер телефона. Пожалуйста, используйте только цифры и символы '-'.");
                return;
            }

            bool success = databaseSuppliers.UpdateSupplier(supplierID, name, phone);

            if (success)
            {
                MessageBox.Show("Данные поставщика успешно обновлены.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных поставщика.");
            }
        }


        // Метод для проверки корректности имени
        private bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.All(char.IsLetterOrDigit);
        }

        // Метод для проверки корректности номера телефона
        private bool IsValidPhone(string phone)
        {
            return string.IsNullOrEmpty(phone) || phone.All(char.IsDigit) || phone.All(c => char.IsDigit(c) || c == '-');
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
