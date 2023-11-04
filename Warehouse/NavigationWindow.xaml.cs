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
    public partial class NavigationWindow : Window
    {
        public int AccessLevel { get; set; }

        public NavigationWindow()
        {
            InitializeComponent();
        }

        // Обработчик нажатия кнопки "Заказы"
        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для просмотра заказов и передаем уровень доступа
            OrderListWindow orderListWindow = new OrderListWindow();
            orderListWindow.AccessLevel = AccessLevel;
            orderListWindow.Show();
        }

        // Обработчик нажатия кнопки "Продажи"
        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для просмотра продаж и передаем уровень доступа
            PurchaseListWindow purchaseListWindow = new PurchaseListWindow();
            purchaseListWindow.AccessLevel = AccessLevel;
            purchaseListWindow.Show();
        }

        // Обработчик нажатия кнопки "Сотрудники"
        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для просмотра сотрудников и передаем уровень доступа
            EmployeeListWindow employeeListWindow = new EmployeeListWindow();
            employeeListWindow.AccessLevel = AccessLevel;
            employeeListWindow.Show();
        }

        // Обработчик нажатия кнопки "Поставщики"
        private void SuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для просмотра поставщиков и передаем уровень доступа
            SupplierListWindow supplierListWindow = new SupplierListWindow();
            supplierListWindow.AccessLevel = AccessLevel;
            supplierListWindow.Show();
        }

        // Обработчик нажатия кнопки "Товары"
        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для просмотра товаров и передаем уровень доступа
            ProductListWindow productListWindow = new ProductListWindow();
            productListWindow.AccessLevel = AccessLevel;
            productListWindow.Show();
        }

        // Обработчик нажатия кнопки "Выход"
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Перезапускаем приложение, чтобы выйти из него
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
