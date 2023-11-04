using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Warehouse;

namespace Warehouse
{
    public partial class PurchaseAddWindow : Window
    {
        private DatabasePurchases databasePurchases;
        private DatabaseProducts databaseProducts;
        private DatabaseEmployees databaseEmployees;

        public PurchaseAddWindow()
        {
            InitializeComponent();
            // Инициализируем базы данных
            databasePurchases = new DatabasePurchases();
            databaseProducts = new DatabaseProducts();
            databaseEmployees = new DatabaseEmployees();

            // Загружаем данные для ComboBox'ов
            LoadComboBoxData();
        }

        // Метод для загрузки данных в ComboBox'ы
        private void LoadComboBoxData()
        {
            // Получаем продукты и пользователей из базы данных
            var products = databaseProducts.GetProducts();
            var users = databaseEmployees.GetUsers();

            // Устанавливаем элементы для ComboBox'ов
            ProductComboBox.ItemsSource = products;
            ProductComboBox.DisplayMemberPath = "Name";
            ProductComboBox.SelectedValuePath = "ProductID";

            UserComboBox.ItemsSource = users;
            UserComboBox.DisplayMemberPath = "Name";
            UserComboBox.SelectedValuePath = "UserID";
        }

        // Обработчик ввода только цифр и запятых/точек в текстовые поля
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Регулярное выражение для проверки, что вводимый символ - цифра, запятая или точка
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Обработчик нажатия на кнопку "Сохранить"
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int productID;
            int userID;
            int quantity;
            decimal amount;
            DateTime purchaseDate;

            // Проверка на корректность данных и их парсинг
            if (ProductComboBox.SelectedValue == null ||
                UserComboBox.SelectedValue == null ||
                !int.TryParse(ProductComboBox.SelectedValue.ToString(), out productID) ||
                !int.TryParse(UserComboBox.SelectedValue.ToString(), out userID) ||
                !int.TryParse(QuantityTextBox.Text, out quantity) ||
                !decimal.TryParse(AmountTextBox.Text, out amount) ||
                !DateTime.TryParse(PurchaseDatePicker.Text, out purchaseDate))
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.");
                return;
            }

            // Добавление покупки в базу данных
            bool success = databasePurchases.AddPurchase(productID, userID, quantity, amount, purchaseDate);

            if (success)
            {
                MessageBox.Show("Покупка успешно добавлена.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении покупки.");
            }
        }

        // Обработчик нажатия на кнопку "Отмена"
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
