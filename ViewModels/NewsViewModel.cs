using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using NewsWPF.Models;
using NewsWPF.Pages;

namespace NewsWPF.ViewModels
{
    /// <summary>
    /// Главная модель представления для управления новостями и категориями.
    /// </summary>
    public class NewsViewModel
    {
        public ObservableCollection<News> NewsList { get; private set; }
        public ObservableCollection<Category> CategoryList { get; private set; }

        private News? _selectedNews;
        private Category? _selectedCategory;

        public News? SelectedNews
        {
            get { return _selectedNews; }
            set
            {
                _selectedNews = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public Category? SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        // Команды для новостей
        public ICommand AddNewsCommand { get; private set; }
        public ICommand EditNewsCommand { get; private set; }
        public ICommand DeleteNewsCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ViewNewsCommand { get; private set; }

        // Команды для категорий
        public ICommand AddCategoryCommand { get; private set; }
        public ICommand EditCategoryCommand { get; private set; }
        public ICommand DeleteCategoryCommand { get; private set; }

        public NewsViewModel()
        {
            NewsList = new ObservableCollection<News>();
            CategoryList = new ObservableCollection<Category>();

            // Создаём категории из ТЗ
            CategoryList.Add(new Category("Политика", "Новости политики"));
            CategoryList.Add(new Category("Спорт", "Спортивные события"));
            CategoryList.Add(new Category("Культура", "Культурная жизнь"));
            CategoryList.Add(new Category("Технологии", "IT и гаджеты"));
            CategoryList.Add(new Category("Экономика", "Финансы и рынки"));
            CategoryList.Add(new Category("Происшествия", "Сводки происшествий"));

            // Тестовая новость
            NewsList.Add(new News(
                "Тестовая новость",
                CategoryList[3],
                "Иван Иванов",
                "Опубликовано",
                "Это текст тестовой новости, который содержит более двадцати символов для валидации."));

            // Инициализация команд
            AddNewsCommand = new RelayCommand(OnAddNews, null);
            EditNewsCommand = new RelayCommand(OnEditNews, CanEditOrDeleteNews);
            DeleteNewsCommand = new RelayCommand(OnDeleteNews, CanEditOrDeleteNews);
            RefreshCommand = new RelayCommand(OnRefresh, null);
            ViewNewsCommand = new RelayCommand(OnViewNews, CanEditOrDeleteNews);

            AddCategoryCommand = new RelayCommand(OnAddCategory, null);
            EditCategoryCommand = new RelayCommand(OnEditCategory, CanEditOrDeleteCategory);
            DeleteCategoryCommand = new RelayCommand(OnDeleteCategory, CanEditOrDeleteCategory);
        }

        // --- Методы для новостей ---

        private void OnAddNews(object? parameter)
        {
            AddEditNewsWindow dialog = new AddEditNewsWindow(null, CategoryList);
            SetDialogOwner(dialog);

            if (dialog.ShowDialog() == true)
            {
                AddEditNewsViewModel vm = (AddEditNewsViewModel)dialog.DataContext;
                News newNews = new News(vm.Title!, vm.SelectedCategory!, vm.Author!, vm.SelectedStatus!, vm.Text!);
                newNews.DateOfPublication = vm.DateOfPublication;

                NewsList.Add(newNews);
                vm.SelectedCategory!.CountNews++;
            }
        }

        private void OnEditNews(object? parameter)
        {
            if (SelectedNews == null) return;

            AddEditNewsWindow dialog = new AddEditNewsWindow(SelectedNews, CategoryList);
            SetDialogOwner(dialog);

            if (dialog.ShowDialog() == true)
            {
                AddEditNewsViewModel vm = (AddEditNewsViewModel)dialog.DataContext;

                // Если категорию изменили — корректируем счётчики
                if (SelectedNews.Category != vm.SelectedCategory)
                {
                    SelectedNews.Category!.CountNews--;
                    vm.SelectedCategory!.CountNews++;
                }

                SelectedNews.Title = vm.Title!;
                SelectedNews.Text = vm.Text!;
                SelectedNews.Author = vm.Author!;
                SelectedNews.Status = vm.SelectedStatus!;
                SelectedNews.Category = vm.SelectedCategory;
                SelectedNews.DateOfPublication = vm.DateOfPublication;
            }
        }

        private void OnDeleteNews(object? parameter)
        {
            if (SelectedNews == null) return;

            MessageBoxResult result = MessageBox.Show(
                "Удалить новость \"" + SelectedNews.Title + "\"?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SelectedNews.Category!.CountNews--;
                NewsList.Remove(SelectedNews);
                SelectedNews = null;
            }
        }

        private void OnViewNews(object? parameter)
        {
            if (SelectedNews == null) return;
            SelectedNews.NumberOfView++;
            OnEditNews(null);
        }

        private void OnRefresh(object? parameter)
        {
            MessageBox.Show("Список обновлён", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CanEditOrDeleteNews(object? parameter)
        {
            return SelectedNews != null;
        }

        // Методы для категорий 

        private void OnAddCategory(object? parameter)
        {
            AddEditCategoryWindow dialog = new AddEditCategoryWindow(null);
            SetDialogOwner(dialog);

            if (dialog.ShowDialog() == true)
            {
                AddEditCategoryViewModel vm = (AddEditCategoryViewModel)dialog.DataContext;
                Category newCategory = new Category(vm.Name!, vm.Description!);
                CategoryList.Add(newCategory);
            }
        }

        private void OnEditCategory(object? parameter)
        {
            if (SelectedCategory == null) return;

            AddEditCategoryWindow dialog = new AddEditCategoryWindow(SelectedCategory);
            SetDialogOwner(dialog);

            if (dialog.ShowDialog() == true)
            {
                AddEditCategoryViewModel vm = (AddEditCategoryViewModel)dialog.DataContext;
                SelectedCategory.Name = vm.Name!;
                SelectedCategory.Description = vm.Description!;
            }
        }

        private void OnDeleteCategory(object? parameter)
        {
            if (SelectedCategory == null) return;

            if (SelectedCategory.CountNews > 0)
            {
                MessageBox.Show("Нельзя удалить категорию, в которой есть новости!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CategoryList.Remove(SelectedCategory);
            SelectedCategory = null;
        }

        private bool CanEditOrDeleteCategory(object? parameter)
        {
            return SelectedCategory != null;
        }

        /// <summary>
        /// Безопасная установка родительского окна для диалога.
        /// Проверяет, что MainWindow существует и не закрыт.
        /// </summary>
        private void SetDialogOwner(Window dialog)
        {
            Window? mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
            {
                dialog.Owner = mainWindow;
            }
        }
    }
}