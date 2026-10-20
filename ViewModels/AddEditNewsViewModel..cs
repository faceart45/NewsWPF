using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NewsWPF.Models;

namespace NewsWPF.ViewModels
{
    /// <summary>
    /// Модель представления для окна добавления/редактирования новости.
    /// Содержит поля формы и логику валидации по ТЗ.
    /// </summary>
    public class AddEditNewsViewModel : INotifyPropertyChanged
    {
        private string _title = "";
        private string _text = "";
        private Category? _selectedCategory;
        private DateTime _dateOfPublication;
        private string _author = "";
        private string _selectedStatus = "Черновик";
        private bool _canSave;

        private bool _hasTitleError;
        private bool _hasTextError;
        private bool _hasAuthorError;
        private bool _hasDateError;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); ValidateForm(); }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); ValidateForm(); }
        }

        public Category? SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged(); ValidateForm(); }
        }

        public DateTime DateOfPublication
        {
            get { return _dateOfPublication; }
            set { _dateOfPublication = value; OnPropertyChanged(); ValidateForm(); }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; OnPropertyChanged(); ValidateForm(); }
        }

        public string SelectedStatus
        {
            get { return _selectedStatus; }
            set { _selectedStatus = value; OnPropertyChanged(); ValidateForm(); }
        }

        public bool CanSave
        {
            get { return _canSave; }
            private set { _canSave = value; OnPropertyChanged(); }
        }

        public bool HasTitleError
        {
            get { return _hasTitleError; }
            private set { _hasTitleError = value; OnPropertyChanged(); }
        }

        public bool HasTextError
        {
            get { return _hasTextError; }
            private set { _hasTextError = value; OnPropertyChanged(); }
        }

        public bool HasAuthorError
        {
            get { return _hasAuthorError; }
            private set { _hasAuthorError = value; OnPropertyChanged(); }
        }

        public bool HasDateError
        {
            get { return _hasDateError; }
            private set { _hasDateError = value; OnPropertyChanged(); }
        }

        public AddEditNewsViewModel()
        {
            DateOfPublication = DateTime.Now.Date;
            ValidateForm();
        }

        /// <summary>
        /// Метод проверки валидности формы по правилам ТЗ.
        /// </summary>
        private void ValidateForm()
        {
            bool isTitleValid = !string.IsNullOrEmpty(Title) && Title.Length >= 5 && Title.Length <= 200;
            HasTitleError = !isTitleValid;

            bool isTextValid = !string.IsNullOrEmpty(Text) && Text.Length >= 20 && Text.Length <= 10000;
            HasTextError = !isTextValid;

            bool isAuthorValid = !string.IsNullOrEmpty(Author)
                && Author.Length >= 5 && Author.Length <= 100
                && IsOnlyLettersAndSpaces(Author);
            HasAuthorError = !isAuthorValid;

            bool isDateValid = DateOfPublication <= DateTime.Now.Date;
            HasDateError = !isDateValid;

            bool isCategoryValid = SelectedCategory != null;
            bool isStatusValid = !string.IsNullOrEmpty(SelectedStatus);

            CanSave = isTitleValid && isTextValid && isAuthorValid
                      && isDateValid && isCategoryValid && isStatusValid;
        }

        /// <summary>
        /// Проверяет, что строка состоит только из букв и пробелов.
        /// </summary>
        private bool IsOnlyLettersAndSpaces(string input)
        {
            foreach (char symbol in input)
            {
                if (!char.IsLetter(symbol) && !char.IsWhiteSpace(symbol))
                {
                    return false;
                }
            }
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}