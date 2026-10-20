using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsWPF.Models
{
    /// <summary>
    /// Модель пользователя системы.
    /// </summary>
    public class Users : INotifyPropertyChanged
    {
        private string _login = "";
        private string _password = "";
        private int _role;

        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        public int Role
        {
            get { return _role; }
            set { _role = value; OnPropertyChanged(); }
        }

        public Users(string login, string password, int role)
        {
            Login = login;
            Password = password;
            Role = role;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}