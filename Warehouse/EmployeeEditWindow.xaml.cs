using System;
using System.Collections.Generic;
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

namespace Warehouse
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        private DatabaseEmployees databaseEmployees; // Экземпляр класса для работы с данными о сотрудниках
        private int userID; // Идентификатор пользователя, чьи данные редактируются

        public EmployeeEditWindow(int userID)
        {
            InitializeComponent();
            this.userID = userID;
            databaseEmployees = new DatabaseEmployees();

            // Загрузка данных пользователя для редактирования
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                DataTable userDataTable = databaseEmployees.GetAllUsers();

                // Поиск данных пользователя в таблице по идентификатору
                foreach (DataRow row in userDataTable.Rows)
                {
                    if (Convert.ToInt32(row["UserID"]) == userID)
                    {
                        LoginTextBox.Text = row["Login"].ToString();
                        PasswordBox.Password = row["Password"].ToString();
                        NameTextBox.Text = row["Name"].ToString();
                        PhoneTextBox.Text = row["Phone"].ToString();
                        AccessLevelComboBox.SelectedValue = row["AccessLevel"];
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных пользователя: " + ex.Message);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            string name = NameTextBox.Text;
            string phone = PhoneTextBox.Text;
            int accessLevel = Convert.ToInt32(AccessLevelComboBox.SelectedValue);

            // Проверка на пустые обязательные поля и длину логина и пароля
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            // Проверка длины логина (по вашему выбору, например, логин должен быть не менее 4 символов)
            if (login.Length < 4)
            {
                MessageBox.Show("Логин должен содержать не менее 4 символов.");
                return;
            }

            // Проверка длины пароля (по вашему выбору, например, пароль должен быть не менее 4 символов)
            if (password.Length < 4)
            {
                MessageBox.Show("Пароль должен содержать не менее 4 символов.");
                return;
            }

            // Обновление данных пользователя в базе данных
            bool success = databaseEmployees.UpdateUser(userID, login, password, name, phone, accessLevel);

            if (success)
            {
                MessageBox.Show("Данные пользователя успешно обновлены.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных пользователя.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
