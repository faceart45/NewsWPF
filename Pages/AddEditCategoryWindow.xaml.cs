using System.Windows;
using NewsWPF.Models;
using NewsWPF.ViewModels;

namespace NewsWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditCategoryWindow.xaml.
    /// </summary>
    public partial class AddEditCategoryWindow : Window
    {
        /// <summary>
        /// Конструктор окна.
        /// </summary>
        /// <param name="category">Категория для редактирования (null для новой)</param>
        public AddEditCategoryWindow(Category? category)
        {
            InitializeComponent();

            AddEditCategoryViewModel vm = new AddEditCategoryViewModel();

            if (category != null)
            {
                vm.Name = category.Name;
                vm.Description = category.Description;
            }

            this.DataContext = vm;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}