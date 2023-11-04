using System;
using System.Linq;
using System.Windows;
using Warehouse;

namespace Warehouse
{
    public partial class SupplierAddWindow : Window
    {
        private DatabaseSuppliers databaseSuppliers;

        public SupplierAddWindow()
        {
            InitializeComponent();
            databaseSuppliers = new DatabaseSuppliers();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем значения из текстовых полей
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

            // Добавляем поставщика в базу данных
            bool success = databaseSuppliers.AddSupplier(name, phone);

            if (success)
            {
                MessageBox.Show("Поставщик успешно добавлен.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении поставщика.");
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

        // Метод обработки нажатия кнопки "Отмена"
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
