using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Warehouse;

namespace Warehouse
{
    public partial class ProductAddWindow : Window
    {
        private DatabaseProducts databaseProducts;

        public ProductAddWindow()
        {
            InitializeComponent();
            // Инициализация базы данных для продуктов
            databaseProducts = new DatabaseProducts();
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
            string name = NameTextBox.Text;
            int quantity = 0;
            decimal price = 0;
            string manufacturer = ManufacturerTextBox.Text;
            int article = 0;

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


            // Добавление товара в базу данных и обработка результата
            bool success = databaseProducts.AddProduct(name, quantity, price, manufacturer, article);

            if (success)
            {
                MessageBox.Show("Товар успешно добавлен.");
                this.Close(); // Закрытие окна после успешного добавления
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении товара.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Закрытие окна при отмене
        }

    }
}
