using System.Windows;
using System.Windows.Input;
using NewsWPF.Pages;

namespace NewsWPF.ViewModels
{
    /// <summary>
    /// ViewModel для окна авторизации.
    /// </summary>
    public class LoginViewModel
    {
        public ICommand LoginCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin, null);
            ExitCommand = new RelayCommand(ExecuteExit, null);
        }

        /// <summary>
        /// Проверяет логин и пароль. При успехе открывает NewsWindow и закрывает окно авторизации.
        /// </summary>
        public bool CheckCredentials(string loginText, string passwordText, Window currentWindow)
        {
            // По ТЗ: вход только для editor / news2025
            if (loginText == "editor" && passwordText == "news2025")
            {
                // Создаём главное окно приложения
                NewsWindow mainAppWindow = new NewsWindow();

                // Делаем NewsWindow главным окном приложения ДО показа
                Application.Current.MainWindow = mainAppWindow;

                // Показываем окно
                mainAppWindow.Show();

                // Закрываем окно авторизации
                currentWindow.Close();
                return true;
            }
            else
            {
                MessageBox.Show("Ошибка авторизации. Проверьте логин и пароль",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void ExecuteExit(object? parameter)
        {
            Application.Current.Shutdown();
        }

        private void ExecuteLogin(object? parameter)
        {
            // Заглушка для ICommand
        }
    }
}