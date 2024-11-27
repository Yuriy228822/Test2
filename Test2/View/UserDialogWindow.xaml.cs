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

namespace Test2.View
{
    /// <summary>
    /// Логика взаимодействия для UserDialogWindow.xaml
    /// </summary>
    public partial class UserDialogWindow : Window
    {

        public int? UserId { get; set; } // ID пользователя
        public string Username { get; set; } // Логин пользователя
        public string Password { get; set; } // Пароль пользователя

        public UserDialogWindow()
        {
            InitializeComponent();
        }

        public UserDialogWindow(int? userId, string username, string password) : this()
        {
            UserId = userId;
            Username = username;
            Password = password;

            // Заполнение полей
            IdTextBox.Text = userId?.ToString() ?? string.Empty;
            UsernameTextBox.Text = username;
            PasswordBox.Password = password;
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка данных
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Установка свойств
            Username = UsernameTextBox.Text;
            Password = PasswordBox.Password;

            DialogResult = true; // Устанавливаем результат диалога как успешный
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Устанавливаем результат диалога как отмененный
            Close();
        }
    }
}
