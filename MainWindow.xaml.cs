using System.Windows;
using NewsWPF.ViewModels;

namespace NewsWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml (окно авторизации).
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            this.DataContext = _viewModel;
        }

        private void Exit_BtnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Login_BtnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isLoginSuccessful = _viewModel.CheckCredentials(
                LoginTextBox.Text, PasswordBox.Password, this);

            if (!isLoginSuccessful)
            {
                LoginTextBox.Text = "";
                PasswordBox.Password = "";
                LoginTextBox.Focus();
            }
        }
    }
}