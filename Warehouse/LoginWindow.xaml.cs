using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Warehouse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        // Обработчик нажатия кнопки "Вход"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем введенные логин и пароль
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Проверяем, что логин и пароль не пустые
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создаем объект для работы с базой данных для аутентификации
            DatabaseAuth dbAuth = new DatabaseAuth();

            // Пытаемся аутентифицировать пользователя
            int accessLevel = dbAuth.AuthenticateUser(username, password);

            // Если аутентификация прошла успешно, открываем главное окно навигации
            if (accessLevel >= 0)
            {
                MessageBox.Show("Авторизация успешна. Уровень доступа: " + accessLevel);

                // Создаем и открываем главное окно навигации, передавая уровень доступа
                NavigationWindow navigationWindow = new NavigationWindow();
                navigationWindow.AccessLevel = accessLevel;
                navigationWindow.Show();

                // Закрываем текущее окно входа (LoginWindow)
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль","Повторите попытку", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик нажатия на кнопку "Регистрация"
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно регистрации пользователя
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.ShowDialog();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
