using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для EmployeeAddWindow.xaml
    /// </summary>
    public partial class EmployeeAddWindow : Window
    {
        private DatabaseEmployees databaseEmployees;
        public EmployeeAddWindow()
        {
            InitializeComponent();
            databaseEmployees = new DatabaseEmployees();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;
            string name = NameTextBox.Text;
            string phone = PhoneTextBox.Text;
            int accessLevel = Convert.ToInt32(AccessLevelComboBox.SelectedValue);

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            
            // Проверка длины пароля (по вашему выбору, например, пароль должен быть не менее 6 символов)
            if (login.Length < 4)
            {
                MessageBox.Show("Логин должен содержать не менее 4 символов.");
                return;
            }

            // Проверка длины пароля (по вашему выбору, например, пароль должен быть не менее 6 символов)
            if (password.Length < 4)
            {
                MessageBox.Show("Пароль должен содержать не менее 4 символов.");
                return;
            }

            bool success = databaseEmployees.AddUser(login, password, name, phone, accessLevel);

            if (success)
            {
                MessageBox.Show("Пользователь успешно добавлен.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
