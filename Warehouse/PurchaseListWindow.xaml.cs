using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using Warehouse;

namespace Warehouse
{
    public partial class PurchaseListWindow : Window
    {
        private DatabasePurchases databasePurchases;
        private string selectedUser = "";

        public int AccessLevel { get; set; }

        public PurchaseListWindow()
        {
            InitializeComponent();
            databasePurchases = new DatabasePurchases();
            LoadPurchases();
            LoadPurchasesWithNames();
        }

        // Метод для загрузки списка покупок
        private void LoadPurchases()
        {
            try
            {
                DataTable dataTable = databasePurchases.GetAllPurchases();
                purchaseGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке покупок: " + ex.Message);
            }
        }

        // Метод для загрузки списка покупок с добавлением имени продукта и имени пользователя
        private void LoadPurchasesWithNames()
        {
            try
            {
                DataTable dataTable = databasePurchases.GetAllPurchases();
                DataTable purchasesWithNamesTable = new DataTable();

                // Создаем таблицу с нужными колонками
                purchasesWithNamesTable.Columns.Add("PurchaseID", typeof(int));
                purchasesWithNamesTable.Columns.Add("ProductName", typeof(string));
                purchasesWithNamesTable.Columns.Add("UserName", typeof(string));
                purchasesWithNamesTable.Columns.Add("Quantity", typeof(int));
                purchasesWithNamesTable.Columns.Add("Amount", typeof(decimal));
                purchasesWithNamesTable.Columns.Add("PurchaseDate", typeof(DateTime));

                foreach (DataRow row in dataTable.Rows)
                {
                    int productID = Convert.ToInt32(row["ProductID"]);
                    int userID = Convert.ToInt32(row["UserID"]);

                    // Получаем имя продукта и имя пользователя по их ID
                    string productName = databasePurchases.GetProductNameById(productID);
                    string userName = databasePurchases.GetUserNameById(userID);

                    // Добавляем данные в новую таблицу
                    purchasesWithNamesTable.Rows.Add(
                        row["PurchaseID"],
                        productName,
                        userName,
                        row["Quantity"],
                        row["Amount"],
                        row["PurchaseDate"]
                    );
                }

                // Устанавливаем новую таблицу как источник данных для грида
                purchaseGrid.ItemsSource = purchasesWithNamesTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке покупок: " + ex.Message);
            }
        }

        // Обработчик нажатия на кнопку удаления покупки
        private void DeletePurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedPurchase = (DataRowView)purchaseGrid.SelectedItem;
            if (selectedPurchase != null)
            {
                int purchaseID = Convert.ToInt32(selectedPurchase["PurchaseID"]);

                if (MessageBox.Show("Вы действительно хотите удалить эту покупку?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    bool success = databasePurchases.DeletePurchase(purchaseID);

                    if (success)
                    {
                        MessageBox.Show("Покупка успешно удалена.");
                        LoadPurchases();
                        LoadPurchasesWithNames();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении покупки.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите покупку для удаления.");
            }
        }

        // Обработчик нажатия на кнопку добавления новой покупки
        private void AddPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            // Открывает окно добавления покупки, если это окно существует
            PurchaseAddWindow purchaseAddWindow = new PurchaseAddWindow();
            purchaseAddWindow.ShowDialog();

            LoadPurchases();
            LoadPurchasesWithNames();
        }

        // Обработчик нажатия на кнопку редактирования покупки
        private void EditPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedPurchase = (DataRowView)purchaseGrid.SelectedItem;
            if (selectedPurchase != null)
            {
                int purchaseID = Convert.ToInt32(selectedPurchase["PurchaseID"]);

                PurchaseEditWindow purchaseEditWindow = new PurchaseEditWindow(purchaseID);
                purchaseEditWindow.ShowDialog();

                LoadPurchases();
                LoadPurchasesWithNames();
            }
            else
            {
                MessageBox.Show("Выберите покупку для редактирования.");
            }
        }

        // Обработчик события загрузки окна
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Если уровень доступа равен 3 или 4, скрываем кнопки добавления, редактирования и удаления
            if (AccessLevel == 3 || AccessLevel == 4)
            {
                AddPurchaseButton.Visibility = Visibility.Collapsed;
                EditPurchaseButton.Visibility = Visibility.Collapsed;
                DeletePurchaseButton.Visibility = Visibility.Collapsed;
            }

            try
            {
                DatabasePurchases databasePurchases = new DatabasePurchases();
                DataTable usersTable = databasePurchases.GetUsers();

                // Загружаем уникальные имена пользователей в ComboBoxChoose
                var uniqueUserNames = usersTable.AsEnumerable()
                    .Select(row => row.Field<string>("Name"))
                    .Distinct()
                    .ToList();

                ComboBoxChoose.ItemsSource = uniqueUserNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке имен пользователей: " + ex.Message);
            }
        }


        private void ExportToExcel(DataTable dataTable)
        {
            string[] columnNames = new string[]
            {
                 "Идентификатор",
                 "Продукт",
                 "Пользователь",
                 "Количество",
                 "Сумма",
                 "Дата покупки",
            };

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ОтчетПродажи");

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
                        // Определяем столбец с датой
                        if (col == 5)
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dataTable.Rows[row][col];
                            worksheet.Cells[row + 2, col + 1].Style.Numberformat.Format = "dd.mm.yyyy";
                        }
                        else
                        {
                            worksheet.Cells[row + 2, col + 1].Value = dataTable.Rows[row][col];
                        }
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
                DataTable dataTable = (purchaseGrid.ItemsSource as DataView).ToTable();
                ExportToExcel(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании файла Excel: " + ex.Message);
            }
        }

        //Searching
        private void ComboBoxChoose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                selectedUser = ComboBoxChoose.SelectedItem as string;
                ApplyFilters();
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }

        private void TextBoxSearchProduct_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
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

                if (!string.IsNullOrWhiteSpace(selectedUser))
                {
                    // Добавьте фильтр по пользователю, если он выбран
                    filterExpression = $"UserName = '{selectedUser}'";

                    // Если выбран пользователь и есть введенное название товара, добавьте условие "И" для фильтрации по названию товара
                    if (!string.IsNullOrWhiteSpace(searchValue))
                    {
                        filterExpression += $" AND ProductName LIKE '%{searchValue}%'";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    // Если не выбран пользователь, но есть введено название товара, фильтруйте только по названию товара
                    filterExpression = $"ProductName LIKE '%{searchValue}%'";
                }

                (purchaseGrid.ItemsSource as DataView).RowFilter = filterExpression;
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxChoose.SelectedItem = null;
            TextBoxSearchProduct.Text = "";

            // Сбрасываем фильтр и отображаем все данные
            (purchaseGrid.ItemsSource as DataView).RowFilter = "";
        }
    }
}
