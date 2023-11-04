using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Warehouse;

namespace Warehouse
{
    public partial class OrderAddWindow : Window
    {
        private DatabaseOrders databaseOrders;
        private DatabaseSuppliers databaseSuppliers;
        private DatabaseEmployees databaseEmployees;
        private DatabaseProducts databaseProducts;

        public OrderAddWindow()
        {
            InitializeComponent();
            databaseOrders = new DatabaseOrders();
            databaseSuppliers = new DatabaseSuppliers();
            databaseEmployees = new DatabaseEmployees();
            databaseProducts = new DatabaseProducts();

            // Загрузка данных для ComboBox'ов
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            // Получение данных для ComboBox'ов из соответствующих баз данных
            var suppliers = databaseSuppliers.GetSuppliers();
            SupplierComboBox.ItemsSource = suppliers;
            SupplierComboBox.DisplayMemberPath = "Name";
            SupplierComboBox.SelectedValuePath = "SupplierID";

            var users = databaseEmployees.GetUsers();
            UserComboBox.ItemsSource = users;
            UserComboBox.DisplayMemberPath = "Name";
            UserComboBox.SelectedValuePath = "UserID";

            var products = databaseProducts.GetProducts();
            ProductComboBox.ItemsSource = products;
            ProductComboBox.DisplayMemberPath = "Name";
            ProductComboBox.SelectedValuePath = "ProductID";
        }

        // Обработчик ввода только цифр и запятых в текстовые поля
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Регулярное выражение для проверки, что вводимый символ - цифра или запятая
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int supplierID;
            int userID;
            int productID;
            decimal amount;
            DateTime orderDate;

            // Проверка введенных данных
            if (SupplierComboBox.SelectedValue == null ||
                UserComboBox.SelectedValue == null ||
                ProductComboBox.SelectedValue == null ||
                !int.TryParse(SupplierComboBox.SelectedValue.ToString(), out supplierID) ||
                !int.TryParse(UserComboBox.SelectedValue.ToString(), out userID) ||
                !int.TryParse(ProductComboBox.SelectedValue.ToString(), out productID) ||
                !decimal.TryParse(AmountTextBox.Text, out amount) ||
                !DateTime.TryParse(OrderDatePicker.Text, out orderDate))
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.");
                return;
            }

            // Добавление нового заказа в базу данных и обработка результата
            bool success = databaseOrders.AddOrder(supplierID, userID, productID, amount, orderDate);

            if (success)
            {
                MessageBox.Show("Заказ успешно добавлен.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении заказа.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
