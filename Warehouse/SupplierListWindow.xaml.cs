using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using OfficeOpenXml;
using Warehouse;

namespace Warehouse
{
    public partial class SupplierListWindow : Window
    {
        private DatabaseSuppliers databaseSuppliers;
        public int AccessLevel { get; set; }

        public SupplierListWindow()
        {
            InitializeComponent();
            databaseSuppliers = new DatabaseSuppliers();
            LoadSuppliers();
        }

        // Метод для загрузки списка поставщиков
        private void LoadSuppliers()
        {
            try
            {
                DataTable dataTable = databaseSuppliers.GetAllSuppliers();
                supplierGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке поставщиков: " + ex.Message);
            }
        }

        // Обработчик нажатия на кнопку удаления поставщика
        private void DeleteSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedSupplier = (DataRowView)supplierGrid.SelectedItem;
            if (selectedSupplier != null)
            {
                int supplierID = Convert.ToInt32(selectedSupplier["SupplierID"]);

                if (MessageBox.Show("Вы действительно хотите удалить этого поставщика?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DatabaseSuppliers databaseSuppliers = new DatabaseSuppliers();
                    bool success = databaseSuppliers.DeleteSupplier(supplierID);

                    if (success)
                    {
                        MessageBox.Show("Поставщик успешно удален.");
                        LoadSuppliers();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении поставщика.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите поставщика для удаления.");
            }
        }

        // Обработчик нажатия на кнопку добавления нового поставщика
        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            SupplierAddWindow supplierAddWindow = new SupplierAddWindow();
            supplierAddWindow.ShowDialog();

            LoadSuppliers();
        }

        // Обработчик нажатия на кнопку редактирования поставщика
        private void EditSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedSupplier = (DataRowView)supplierGrid.SelectedItem;
            if (selectedSupplier != null)
            {
                int supplierID = Convert.ToInt32(selectedSupplier["SupplierID"]);

                SupplierEditWindow supplierEditWindow = new SupplierEditWindow(supplierID);
                supplierEditWindow.ShowDialog();

                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Выберите поставщика для редактирования.");
            }
        }

        // Обработчик события загрузки окна
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Если уровень доступа равен 3 или 4, скрываем кнопки добавления, редактирования и удаления
            if (AccessLevel == 3 || AccessLevel == 4)
            {
                AddSupplierButton.Visibility = Visibility.Collapsed;
                EditSupplierButton.Visibility = Visibility.Collapsed;
                DeleteSupplierButton.Visibility = Visibility.Collapsed;
            }
        }

        private void ExportToExcel(DataTable dataTable)
        {
            string[] columnNames = new string[]
            {
                "Идентификатор",
                "Имя",
                "Телефон",
            };

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ОтчетПоставщики");

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
                DataTable dataTable = (supplierGrid.ItemsSource as DataView).ToTable();
                ExportToExcel(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании файла Excel: " + ex.Message);
            }
        }

        //Searching
        private void TextBoxSearchSupplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                (supplierGrid.ItemsSource as DataView).RowFilter = $"Name LIKE '%{TextBoxSearchSupplier.Text}%'";
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }
    }
}
