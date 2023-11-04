using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Warehouse;

namespace Warehouse
{
    public partial class ProductEditWindow : Window
    {
        private DatabaseProducts databaseProducts;
        private int productID;

        public ProductEditWindow(int productID)
        {
            InitializeComponent();
            this.productID = productID;
            // Инициализация базы данных для продуктов
            databaseProducts = new DatabaseProducts();

            // Загрузка данных продукта для редактирования
            LoadProductData();
        }

        // Обработчик ввода только цифр и запятых/точек в текстовые поля
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            // Регулярное выражение для проверки, что вводимый символ - цифра, запятая или точка
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LoadProductData()
        {
            try
            {
                DataTable productDataTable = databaseProducts.GetAllProducts();

                foreach (DataRow row in productDataTable.Rows)
                {
                    if (Convert.ToInt32(row["ProductID"]) == productID)
                    {
                        // Заполнение текстовых полей данными из базы данных
                        NameTextBox.Text = row["Name"].ToString();
                        QuantityTextBox.Text = row["Quantity"].ToString();
                        PriceTextBox.Text = row["Price"].ToString();
                        ManufacturerTextBox.Text = row["Manufacturer"].ToString();
                        ArticleTextBox.Text = row["Article"].ToString() ;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных продукта: " + ex.Message);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            int quantity = 0;
            decimal price = 0;
            string manufacturer = ManufacturerTextBox.Text;
            int article = 0;

            // Проверка введенных данных

            // Проверка на наличие наименования товара и его длину
            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                MessageBox.Show("Пожалуйста, введите наименование товара.");
                return;
            }

            // Проверка на корректное количество товара (целое неотрицательное число)
            if (!int.TryParse(QuantityTextBox.Text, out quantity) || quantity < 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное количество товара.");
                return;
            }

            // Проверка на корректную цену товара (неотрицательное число)
            if (!decimal.TryParse(PriceTextBox.Text, out price) || price < 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную цену товара.");
                return;
            }
            // Проверка на корректное количество товара (целое неотрицательное число)
            if (!int.TryParse(ArticleTextBox.Text, out article) || article < 0)
            {
                MessageBox.Show("Пожалуйста, введите корректный артикул.");
                return;
            }

            // Обновление данных товара в базе данных и обработка результата
            bool success = databaseProducts.UpdateProduct(productID, name, quantity, price, manufacturer, article);

            if (success)
            {
                MessageBox.Show("Данные товара успешно обновлены.");
                this.Close(); // Закрытие окна после успешного обновления
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных товара.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие окна при отмене
        }
    }
}
