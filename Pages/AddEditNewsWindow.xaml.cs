using System.Collections.ObjectModel;
using System.Windows;
using NewsWPF.Models;
using NewsWPF.ViewModels;

namespace NewsWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditNewsWindow.xaml.
    /// </summary>
    public partial class AddEditNewsWindow : Window
    {
        /// <summary>
        /// Список категорий для ComboBox. Передаётся из главного окна.
        /// </summary>
        public ObservableCollection<Category> CategoriesForCombo { get; set; }

        /// <summary>
        /// Конструктор окна.
        /// </summary>
        /// <param name="news">Новость для редактирования (null для добавления новой)</param>
        /// <param name="categories">Список всех категорий для ComboBox</param>
        public AddEditNewsWindow(News? news, ObservableCollection<Category> categories)
        {
            InitializeComponent();

            AddEditNewsViewModel vm = new AddEditNewsViewModel();
            this.CategoriesForCombo = categories;
            this.DataContext = vm;

            // Если это редактирование — заполняем поля
            if (news != null)
            {
                vm.Title = news.Title;
                vm.Text = news.Text;
                vm.SelectedCategory = news.Category;
                vm.DateOfPublication = news.DateOfPublication;
                vm.Author = news.Author;
                vm.SelectedStatus = news.Status;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditNewsViewModel vm = (AddEditNewsViewModel)this.DataContext;

            if (vm.CanSave)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Пожалуйста, исправьте ошибки в полях, выделенных красным.",
                    "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}