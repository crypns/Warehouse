using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Warehouse;

namespace Warehouse
{
    public partial class PurchaseEditWindow : Window
    {
        private DatabasePurchases databasePurchases;
        private int purchaseID;

        public PurchaseEditWindow(int purchaseID)
        {
            InitializeComponent();
            this.purchaseID = purchaseID;
            databasePurchases = new DatabasePurchases();
            LoadPurchaseData();
        }

        // Метод для загрузки данных о покупке
        private void LoadPurchaseData()
        {
            try
            {
                DataTable purchaseDataTable = databasePurchases.GetPurchaseById(purchaseID);

                if (purchaseDataTable.Rows.Count > 0)
                {
                    DataRow purchaseData = purchaseDataTable.Rows[0];

                    DataTable productsDataTable = databasePurchases.GetProducts();
                    ProductComboBox.ItemsSource = productsDataTable.DefaultView;
                    ProductComboBox.DisplayMemberPath = "Name";
                    ProductComboBox.SelectedValuePath = "ProductID";
                    ProductComboBox.SelectedValue = purchaseData["ProductID"];

                    DataTable usersDataTable = databasePurchases.GetUsers();
                    UserComboBox.ItemsSource = usersDataTable.DefaultView;
                    UserComboBox.DisplayMemberPath = "Name";
                    UserComboBox.SelectedValuePath = "UserID";
                    UserComboBox.SelectedValue = purchaseData["UserID"];

                    QuantityTextBox.Text = purchaseData["Quantity"].ToString();
                    AmountTextBox.Text = purchaseData["Amount"].ToString();
                    PurchaseDatePicker.SelectedDate = Convert.ToDateTime(purchaseData["PurchaseDate"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных покупки: " + ex.Message);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Регулярное выражение для проверки, что вводимый символ - цифра, запятая или точка
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        // Обработчик нажатия на кнопку сохранения изменений
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int productID;
            int userID;
            int quantity;
            decimal amount;
            DateTime purchaseDate;

            // Проверяем валидность данных, если данные не валидны, выводим сообщение и выходим
            if (!int.TryParse(ProductComboBox.SelectedValue.ToString(), out productID) ||
                !int.TryParse(UserComboBox.SelectedValue.ToString(), out userID) ||
                !int.TryParse(QuantityTextBox.Text, out quantity) ||
                !decimal.TryParse(AmountTextBox.Text, out amount) ||
                !DateTime.TryParse(PurchaseDatePicker.Text, out purchaseDate))
            {
                MessageBox.Show("Пожалуйста, введите корректные данные.");
                return;
            }

            // Обновляем данные покупки в базе данных
            bool success = databasePurchases.UpdatePurchase(purchaseID, productID, userID, quantity, amount, purchaseDate);

            if (success)
            {
                MessageBox.Show("Данные покупки успешно обновлены.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных покупки.");
            }
        }

        // Обработчик нажатия на кнопку отмены
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
