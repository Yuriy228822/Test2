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
using Test2.Model;

namespace Test2.View
{
    /// <summary>
    /// Логика взаимодействия для ManageDataWindow.xaml
    /// </summary>
    public partial class ManageDataWindow : Window
    {
        private readonly Database _database = new Database();
        private DataTable _usersDataTable;

        public ManageDataWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _usersDataTable = _database.GetUsers();
            DataGridItems.ItemsSource = _usersDataTable.DefaultView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем диалоговое окно для добавления пользователя
            UserDialogWindow dialog = new UserDialogWindow();
            if (dialog.ShowDialog() == true)
            {
                // Получаем данные из диалогового окна
                string username = dialog.Username;
                string password = dialog.Password;

                try
                {
                    // Добавляем пользователя в базу данных
                    Database database = new Database();
                    database.GetUser(username, password);

                    // Обновляем данные в DataGrid
                    LoadData();

                    MessageBox.Show("Пользователь успешно добавлен.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Logger.Log($"Ошибка при добавлении пользователя: {ex.Message}");
                    MessageBox.Show("Произошла ошибка при добавлении пользователя. Подробности в логах.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridItems.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Получение данных из выбранной строки DataGrid
                var selectedRow = ((DataRowView)DataGridItems.SelectedItem).Row;
                int id = (int)selectedRow["id"];
                string username = selectedRow["username"].ToString();
                string password = selectedRow["password"].ToString();

                // Открытие окна редактирования
                var editUserDialog = new UserDialogWindow(id, username, password); // Передаем ID, username, password
                if (editUserDialog.ShowDialog() == true) // Если пользователь нажал "ОК"
                {
                    // Обновление данных через метод базы данных
                    _database.UpdateUser(id, editUserDialog.Username, editUserDialog.Password);

                    // Обновление данных в DataGrid
                    LoadData();

                    MessageBox.Show("Запись успешно обновлена.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка редактирования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteButton_Click(Object sender, RoutedEventArgs e)
        {
            if (DataGridItems.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для удаления.");
                return;
            }

            try
            {
                var selectedRow = ((DataRowView)DataGridItems.SelectedItem).Row;
                int id = (int)selectedRow["id"];

                var result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение",
                                             MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _database.DeleteUser(id); // Метод удаления
                    LoadData(); // Обновление данных
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}");
            }
        }

    }
}
