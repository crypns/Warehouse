using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Warehouse;

namespace Warehouse
{
    public partial class ProductListWindow : Window
    {
        private DatabaseProducts databaseProducts;
        private string selectedManufacturer = "";

        // Свойство для уровня доступа
        public int AccessLevel { get; set; }

        public ProductListWindow()
        {
            InitializeComponent();
            // Инициализация базы данных для продуктов
            databaseProducts = new DatabaseProducts();
            // Загрузка списка продуктов
            LoadProducts();
        }


        // Метод для загрузки списка продуктов
        private void LoadProducts()
        {
            try
            {
                DataTable dataTable = databaseProducts.GetAllProducts();
                // Устанавливаем источник данных для грида
                productGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке товаров: " + ex.Message);
            }
        }

        // Обработчик нажатия на кнопку "Удалить товар"
        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedProduct = (DataRowView)productGrid.SelectedItem;
            if (selectedProduct != null)
            {
                int productID = Convert.ToInt32(selectedProduct["ProductID"]);

                // Отображаем диалоговое окно подтверждения удаления товара
                if (MessageBox.Show("Вы действительно хотите удалить этот товар?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DatabaseProducts databaseProducts = new DatabaseProducts();
                    bool success = databaseProducts.DeleteProduct(productID);

                    if (success)
                    {
                        MessageBox.Show("Товар успешно удален.");
                        LoadProducts();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении товара.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления.");
            }
        }

        // Обработчик нажатия на кнопку "Добавить товар"
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно добавления товара в режиме диалога
            ProductAddWindow productAddWindow = new ProductAddWindow();
            productAddWindow.ShowDialog();

            // После закрытия окна обновляем список товаров
            LoadProducts();
            LoadComboBox();
            
        }

        private void LoadComboBox()
        {
            try
            {
                DataTable dataTable = databaseProducts.GetAllProducts();
                var uniqueManufacturers = dataTable.AsEnumerable().Select(row => row.Field<string>("Manufacturer")).Distinct().ToList();

                ComboBoxChoose.ItemsSource = uniqueManufacturers;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке товаров: " + ex.Message);
            }
        }

        // Обработчик нажатия на кнопку "Редактировать товар"
        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedProduct = (DataRowView)productGrid.SelectedItem;
            if (selectedProduct != null)
            {
                int productID = Convert.ToInt32(selectedProduct["ProductID"]);

                // Открываем окно редактирования товара в режиме диалога
                ProductEditWindow productEditWindow = new ProductEditWindow(productID);
                productEditWindow.ShowDialog();

                // После закрытия окна обновляем список товаров
                LoadProducts();
                LoadComboBox();
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования.");
            }
        }

        // Обработчик события "Загрузка окна"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Если уровень доступа пользователя 3 или 4, скрываем кнопки для добавления, редактирования и удаления товаров
            if (AccessLevel == 3 || AccessLevel == 4)
            {
                AddProductButton.Visibility = Visibility.Collapsed;
                EditProductButton.Visibility = Visibility.Collapsed;
                DeleteProductButton.Visibility = Visibility.Collapsed;
            }

            LoadComboBox();
        }


        private void ComboBoxChoose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                selectedManufacturer = ComboBoxChoose.SelectedItem as string;
                ApplyFilters();
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }


        private void ExportToExcel(DataTable dataTable)
        {
            string[] columnNames = new string[]
            {
                "Идентификатор",
                "Наименование",
                "Количество",
                "Цена",
                "Производитель",
                "Артикул"
            };

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ОтчетТовары");

                // Заполняем заголовки
                for (int i = 1; i <= dataTable.Columns.Count; i++)
                {
                    worksheet.Cells[1, i].Value = columnNames[i - 1];
                    worksheet.Cells[1, i].Style.Font.Bold = true;
                }

                // Заполняем данные
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = dataTable.Rows[row][col];
                    }
                }

                // Автонастройка ширины столбцов
                worksheet.Cells.AutoFitColumns();

                // Сохраняем файл Excel
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    var excelFile = new FileInfo(saveFileDialog.FileName);
                    package.SaveAs(excelFile);
                }
            }
        }

        private void Exel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dataTable = (productGrid.ItemsSource as DataView).ToTable();
                ExportToExcel(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании файла Excel: " + ex.Message);
            }
        }


        private void TextBoxSearchProduct_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                string searchValue = TextBoxSearchProduct.Text;
                ApplyFilters();
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }

        private void ApplyFilters()
        {
            try
            {
                string searchValue = TextBoxSearchProduct.Text;
                string filterExpression = "";

                if (!string.IsNullOrWhiteSpace(selectedManufacturer))
                {
                    filterExpression += $"Manufacturer = '{selectedManufacturer}'";
                }

                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    if (!string.IsNullOrWhiteSpace(filterExpression))
                    {
                        filterExpression += " AND ";
                    }
                    filterExpression += $"Name LIKE '%{searchValue}%'";
                }

                (productGrid.ItemsSource as DataView).RowFilter = filterExpression;
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Сбрасываем значения ComboBox и TextBox для поиска
            ComboBoxChoose.SelectedItem = null;
            TextBoxSearchProduct.Text = "";

            // Сбрасываем фильтр и отображаем все данные
            (productGrid.ItemsSource as DataView).RowFilter = "";
        }
    }
}
