
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using Warehouse;

namespace Warehouse
{
    public partial class OrderListWindow : Window
    {
        private DatabaseOrders databaseOrders;
        private string selectedUser = "";
        private string selectedSupplier = "";

        public int AccessLevel { get; set; }

        public OrderListWindow()
        {
            InitializeComponent();
            // Инициализация базы данных для заказов
            databaseOrders = new DatabaseOrders();

            // Загрузка заказов и заказов с именами для отображения
            LoadOrders();
            LoadOrdersWithNames();
        }

        private void LoadOrders()
        {
            try
            {
                // Получение всех заказов из базы данных и отображение их в гриде
                DataTable dataTable = databaseOrders.GetAllOrders();
                orderGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке заказов: " + ex.Message);
            }
        }

        private void LoadOrdersWithNames()
        {
            try
            {
                // Получение всех заказов из базы данных и создание таблицы с именами для отображения
                DataTable dataTable = databaseOrders.GetAllOrders();
                DataTable ordersWithNamesTable = new DataTable();

                // Определение столбцов таблицы с именами
                ordersWithNamesTable.Columns.Add("OrderID", typeof(int));
                ordersWithNamesTable.Columns.Add("SupplierName", typeof(string));
                ordersWithNamesTable.Columns.Add("UserName", typeof(string));
                ordersWithNamesTable.Columns.Add("ProductName", typeof(string));
                ordersWithNamesTable.Columns.Add("Amount", typeof(decimal));
                ordersWithNamesTable.Columns.Add("OrderDate", typeof(DateTime));

                foreach (DataRow row in dataTable.Rows)
                {
                    // Получение имен поставщика, пользователя и продукта по их идентификаторам
                    int supplierID = Convert.ToInt32(row["SupplierID"]);
                    int userID = Convert.ToInt32(row["UserID"]);
                    int productID = Convert.ToInt32(row["ProductID"]);

                    string supplierName = databaseOrders.GetSupplierNameById(supplierID);
                    string userName = databaseOrders.GetUserNameById(userID);
                    string productName = databaseOrders.GetProductNameById(productID);

                    // Добавление записи с именами в таблицу с именами
                    ordersWithNamesTable.Rows.Add(
                        row["OrderID"],
                        supplierName,
                        userName,
                        productName,
                        row["Amount"],
                        row["OrderDate"]
                    );
                }

                // Отображение таблицы с именами в гриде
                orderGrid.ItemsSource = ordersWithNamesTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке заказов: " + ex.Message);
            }
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedOrder = (DataRowView)orderGrid.SelectedItem;
            if (selectedOrder != null)
            {
                int orderID = Convert.ToInt32(selectedOrder["OrderID"]);

                if (MessageBox.Show("Вы действительно хотите удалить этот заказ?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Удаление заказа из базы данных и обработка результата
                    DatabaseOrders databaseOrders = new DatabaseOrders();
                    bool success = databaseOrders.DeleteOrder(orderID);

                    if (success)
                    {
                        MessageBox.Show("Заказ успешно удален.");
                        LoadOrders();
                        LoadOrdersWithNames();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении заказа.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите заказ для удаления.");
            }
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна добавления заказа и обновление списка заказов
            OrderAddWindow orderAddWindow = new OrderAddWindow();
            orderAddWindow.ShowDialog();

            LoadOrders();
            LoadOrdersWithNames();
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedOrder = (DataRowView)orderGrid.SelectedItem;
            if (selectedOrder != null)
            {
                int orderID = Convert.ToInt32(selectedOrder["OrderID"]);

                // Открытие окна редактирования заказа и обновление списка заказов
                OrderEditWindow orderEditWindow = new OrderEditWindow(orderID);
                orderEditWindow.ShowDialog();

                LoadOrders();
                LoadOrdersWithNames();
            }
            else
            {
                MessageBox.Show("Выберите заказ для редактирования.");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Скрытие кнопок добавления, редактирования и удаления заказов в зависимости от уровня доступа
            if (AccessLevel == 2 || AccessLevel == 4)
            {
                AddOrderButton.Visibility = Visibility.Collapsed;
                EditOrderButton.Visibility = Visibility.Collapsed;
                DeleteOrderButton.Visibility = Visibility.Collapsed;
            }

            try
            {
                DataTable usersTable = databaseOrders.GetUsers();

                // Загружаем уникальные имена пользователей в ComboBoxChoose
                var uniqueUserNames = usersTable.AsEnumerable()
                    .Select(row => row.Field<string>("Name"))
                    .Distinct()
                    .ToList();

                ComboBoxChoose_User.ItemsSource = uniqueUserNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке имен пользователей: " + ex.Message);
            }

            try
            {
                DataTable usersTable = databaseOrders.GetSuppliers();

                // Загружаем уникальные имена пользователей в ComboBoxChoose
                var uniqueSupplier = usersTable.AsEnumerable()
                    .Select(row => row.Field<string>("Name"))
                    .Distinct()
                    .ToList();

                ComboBoxChoose_Supplier.ItemsSource = uniqueSupplier;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке поставщиков: " + ex.Message);
            }
        }

        private void ExportToExcel(DataTable dataTable)
        {
            string[] columnNames = new string[]
            {
                 "Идентификатор",
                 "Поставщик",
                 "Пользователь",
                 "Продукт",
                 "Сумма",
                 "Дата заказа",
            };

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ОтчетЗаказы");

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
                DataTable dataTable = (orderGrid.ItemsSource as DataView).ToTable();
                ExportToExcel(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании файла Excel: " + ex.Message);
            }
        }

        //Searching
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

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxChoose_User.SelectedItem = null;
            ComboBoxChoose_Supplier.SelectedItem = null;
            TextBoxSearchProduct.Text = "";

            // Сбрасываем фильтр и отображаем все данные
            (orderGrid.ItemsSource as DataView).RowFilter = "";
        }

        private void ComboBoxChoose_User_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                selectedUser = ComboBoxChoose_User.SelectedItem as string;
                ApplyFilters();
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }

        private void ComboBoxChoose_Supplier_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                selectedSupplier = ComboBoxChoose_Supplier.SelectedItem as string;
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
                string filterExpression = "";

                if (!string.IsNullOrWhiteSpace(selectedUser))
                {
                    // Фильтр по выбранному пользователю
                    filterExpression += $"UserName LIKE '%{selectedUser}%' ";
                }

                if (!string.IsNullOrWhiteSpace(selectedSupplier))
                {
                    if (!string.IsNullOrEmpty(filterExpression))
                    {
                        // Добавить "AND" если уже есть фильтр
                        filterExpression += " AND ";
                    }

                    // Фильтр по выбранному поставщику
                    filterExpression += $"SupplierName LIKE '%{selectedSupplier}' ";
                }

                string searchValue = TextBoxSearchProduct.Text;
                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    if (!string.IsNullOrEmpty(filterExpression))
                    {
                        // Добавить "AND" если уже есть фильтр
                        filterExpression += " AND ";
                    }

                    // Фильтр по названию продукта
                    filterExpression += $"ProductName LIKE '%{searchValue}%' ";
                }

                // Установить фильтр в гриде
                (orderGrid.ItemsSource as DataView).RowFilter = filterExpression;
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }

    }
}