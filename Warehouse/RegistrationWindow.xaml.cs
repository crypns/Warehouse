using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Warehouse
{
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string name = NameTextBox.Text;
            string phone = PhoneTextBox.Text;
            int accessLevel = 4;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || username.Length < 5 || password.Length < 5)
            {
                MessageBox.Show("Введите логин и пароль (не менее 5 сиимволов)");
                return;
            }

            // Проверяем валидность имени
            if (name.Length < 5)
            {
                MessageBox.Show("Длина имени должна быть больше 5 символов");
                return;
            }

            // Проверяем валидность номера телефона
            if (phone.Length < 5)
            {
                MessageBox.Show("Некорректный номер телефона. Пожалуйста, используйте только цифры и символы '-'.");
                return;
            }

            DatabaseAuth dbRegistration = new DatabaseAuth();
            bool registrationSuccess = dbRegistration.RegisterUser(username, password, name, phone, accessLevel);

            if (registrationSuccess)
            {
                MessageBox.Show("Пользователь успешно зарегистрирован.");
                this.Close(); // Закрыть окно регистрации после успешной регистрации
            }
            else
            {
                MessageBox.Show("Ошибка при регистрации пользователя. Пользователь с таким логином уже существует.");
            }
        }
    }
}
