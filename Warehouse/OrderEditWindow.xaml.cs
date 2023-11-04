using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Warehouse;

namespace Warehouse
{
    public partial class OrderEditWindow : Window
    {
        private DatabaseOrders databaseOrders;
        private int orderID;

        public OrderEditWindow(int orderID)
        {
            InitializeComponent();
            this.orderID = orderID;
            databaseOrders = new DatabaseOrders();

            // Загрузка данных заказа для редактирования
            LoadOrderData();
        }

        private void LoadOrderData()
        {
            try
            {
                // Получение данных заказа из базы данных по orderID
                DataTable orderDataTable = databaseOrders.GetOrderById(orderID);

                if (orderDataTable.Rows.Count > 0)
                {
                    DataRow orderData = orderDataTable.Rows[0];

                    // Заполнение ComboBox для выбора поставщиков
                    DataTable suppliersDataTable = databaseOrders.GetSuppliers();
                    SupplierComboBox.ItemsSource = suppliersDataTable.DefaultView;
                    SupplierComboBox.DisplayMemberPath = "Name";
                    SupplierComboBox.SelectedValuePath = "SupplierID";
                    SupplierComboBox.SelectedValue = orderData["SupplierID"];

                    // Заполнение ComboBox для выбора пользователей
                    DataTable usersDataTable = databaseOrders.GetUsers();
                    UserComboBox.ItemsSource = usersDataTable.DefaultView;
                    UserComboBox.DisplayMemberPath = "Name";
                    UserComboBox.SelectedValuePath = "UserID";
                    UserComboBox.SelectedValue = orderData["UserID"];

                    // Заполнение ComboBox для выбора продуктов
                    DataTable productsDataTable = databaseOrders.GetProducts();
                    ProductComboBox.ItemsSource = productsDataTable.DefaultView;
                    ProductComboBox.DisplayMemberPath = "Name";
                    ProductComboBox.SelectedValuePath = "ProductID";
                    ProductComboBox.SelectedValue = orderData["ProductID"];

                    // Установка значений в TextBox и DatePicker
                    AmountTextBox.Text = orderData["Amount"].ToString();
                    OrderDatePicker.SelectedDate = Convert.ToDateTime(orderData["OrderDate"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных заказа: " + ex.Message);
            }
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

            if (!int.TryParse(SupplierComboBox.SelectedValue.ToString(), out supplierID) ||
                !int.TryParse(UserComboBox.SelectedValue.ToString(), out userID) ||
                !int.TryParse(ProductComboBox.SelectedValue.ToString(), out productID) ||
                !decimal.TryParse(AmountTextBox.Text, out amount) ||
                !DateTime.TryParse(OrderDatePicker.Text, out orderDate))
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.");
                return;
            }

            // Обновление данных заказа в базе данных и обработка результата
            bool success = databaseOrders.UpdateOrder(orderID, supplierID, userID, productID, amount, orderDate);

            if (success)
            {
                MessageBox.Show("Данные заказа успешно обновлены.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных заказа.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UserComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void ProductComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
