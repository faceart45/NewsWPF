using System.Windows;
using System.Windows.Input;
using NewsWPF.ViewModels;

namespace NewsWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewsWindow.xaml.
    /// </summary>
    public partial class NewsWindow : Window
    {
        public NewsWindow()
        {
            InitializeComponent();
            this.DataContext = new NewsViewModel();

            // Делаем это окно главным, если оно ещё не назначено
            // Это страховка на случай, если LoginViewModel не успел это сделать
            if (Application.Current.MainWindow == null || Application.Current.MainWindow != this)
            {
                Application.Current.MainWindow = this;
            }
        }

        private void NewsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NewsViewModel viewModel = (NewsViewModel)this.DataContext;

            if (viewModel != null && viewModel.SelectedNews != null)
            {
                viewModel.ViewNewsCommand.Execute(null);
            }
        }
    }
}