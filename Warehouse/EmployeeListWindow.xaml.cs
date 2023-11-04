using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using OfficeOpenXml;
using System.IO;

namespace Warehouse
{
    /// <summary>
    /// Логика взаимодействия для EmployeeListWindow.xaml
    /// </summary>
    public partial class EmployeeListWindow : Window
    {
        private DatabaseEmployees databaseEmployees;
        private string selectedAccessLevel = "";
        public int AccessLevel { get; set; }

        public EmployeeListWindow()
        {
            InitializeComponent();
            databaseEmployees = new DatabaseEmployees();

            // Загружаем список пользователей при инициализации окна
            LoadUsers();
        }

        // Загрузка списка пользователей
        private void LoadUsers()
        {
            try
            {
                DataTable dataTable = databaseEmployees.GetAllUsers();
                userGrid.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке пользователей: " + ex.Message);
            }
        }

        // Обработчик нажатия кнопки "Удалить пользователя"
        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedUser = (DataRowView)userGrid.SelectedItem;
            if (selectedUser != null)
            {
                int userID = Convert.ToInt32(selectedUser["UserID"]);

                if (MessageBox.Show("Вы действительно хотите удалить этого пользователя?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DatabaseEmployees databaseEmployees = new DatabaseEmployees();
                    bool success = databaseEmployees.DeleteUser(userID);

                    if (success)
                    {
                        MessageBox.Show("Пользователь успешно удален.");
                        LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении пользователя.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя для удаления.");
            }
        }

        // Обработчик нажатия кнопки "Добавить пользователя"
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAddWindow employeeAddWindow = new EmployeeAddWindow();
            employeeAddWindow.ShowDialog();

            // После добавления пользователя обновляем список пользователей
            LoadUsers();
            LoadComboBox();
        }

        // Обработчик нажатия кнопки "Редактировать пользователя"
        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedUser = (DataRowView)userGrid.SelectedItem;
            if (selectedUser != null)
            {
                int userID = Convert.ToInt32(selectedUser["UserID"]);

                EmployeeEditWindow employeeEditWindow = new EmployeeEditWindow(userID);
                employeeEditWindow.ShowDialog();

                // После редактирования пользователя обновляем список пользователей
                LoadUsers();
                LoadComboBox();
            }
            else
            {
                MessageBox.Show("Выберите пользователя для редактирования.");
            }
        }

        private void LoadComboBox()
        {
            try
            {
                DataTable dataTable = databaseEmployees.GetAllUsers();
                var uniqueAccessLevel = dataTable.AsEnumerable()
                    .Select(row => row.Field<int>("AccessLevel"))
                    .Distinct()
                    .ToList();

                UserSearchComboBox.ItemsSource = uniqueAccessLevel;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке уровней доступа: " + ex.Message);
            }
        }

        // Обработчик события "Загрузка окна"
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Основываясь на уровне доступа, устанавливаем видимость кнопок добавления, редактирования и удаления
            if (AccessLevel == 3 || AccessLevel == 4 || AccessLevel == 2)
            {
                AddUserButton.Visibility = Visibility.Collapsed;
                EditUserButton.Visibility = Visibility.Collapsed;
                DeleteUserButton.Visibility = Visibility.Collapsed;
            }

            LoadComboBox();
        }

        private void ExportToExcel(DataTable dataTable)
        {
            string[] columnNames = new string[]
            {
                "Идентификатор",
                "Логин",
                "Пароль",
                "Имя",
                "Телефон",
                "Уровень доступа"
            };

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ОтчетПользователи");

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
                DataTable dataTable = (userGrid.ItemsSource as DataView).ToTable();
                ExportToExcel(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании файла Excel: " + ex.Message);
            }
        }

        private void UserSearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedAccessLevel = UserSearchComboBox.SelectedItem as string;
                ApplyFilters();
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }

        //Searching
        private void TextBoxSearchUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string searchValue = TextBoxSearchUser.Text;
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
                string searchValue = TextBoxSearchUser.Text;
                string filterExpression = "";

                if (UserSearchComboBox.SelectedItem != null)
                {
                    int selectedAccessLevel = (int)UserSearchComboBox.SelectedItem; // Преобразование к числовому типу
                    filterExpression += $"AccessLevel = {selectedAccessLevel}";
                }

                if (!string.IsNullOrWhiteSpace(searchValue))
                {
                    if (!string.IsNullOrWhiteSpace(filterExpression))
                    {
                        filterExpression += " AND ";
                    }
                    filterExpression += $"Name LIKE '%{searchValue}%'";
                }

                (userGrid.ItemsSource as DataView).RowFilter = filterExpression;
            }
            catch
            {
                MessageBox.Show("Данных не найдено");
            }
        }

        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Сбрасываем значения ComboBox и TextBox для поиска
            UserSearchComboBox.SelectedItem = null;
            TextBoxSearchUser.Text = "";

            // Сбрасываем фильтр и отображаем все данные
            (userGrid.ItemsSource as DataView).RowFilter = "";
        }
    }
}
