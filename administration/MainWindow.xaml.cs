using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace administration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //InitUsersList();
        }

        private void UserListSearching_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserListSearching.Text.Equals("Поиск..."))
            {
                UserListSearching.Clear();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserListSearching_LostFocus(object sender, RoutedEventArgs e)
        {
            if(UserListSearching.Text.Length < 1)
            {
                UserListSearching.Text = "Поиск...";
            }
        }

        
        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = InputLogin.Text;
            string password = InputPassword.Password;
            if(login.Length < 5 || password.Length < 5)
            {
                MessageBox.Show("Некоректная длина", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DBConnection db = new DBConnection();
            db.AddAccount(login, password);
            MessageBox.Show("Аккаунт был успешно добавлен", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            InputLogin.Clear();
            InputPassword.Clear();
        }
        private void Refresh_Users(object sender, RoutedEventArgs e)
        {
            UsersList.Items.Clear();
            InitUsersList();
        }
        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void InitUsersList()
        {
            DBConnection db = new DBConnection();
            var result = db.SelectQuery("SELECT login FROM Account");
            int pos = 1;
            while (result.Read())
            {
                string login = result.GetString("login");
                TextBlock textBlock = new TextBlock();
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                textBlock.FontSize = 20;
                textBlock.Text = $"{pos}. {login}";
                UsersList.Items.Add(textBlock);
                pos++;
            }
        }
        
    }
}
