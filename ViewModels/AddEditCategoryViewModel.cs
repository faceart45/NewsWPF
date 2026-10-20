using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsWPF.ViewModels
{
    /// <summary>
    /// Модель представления для окна добавления или редактирования категории.
    /// Отвечает за хранение данных формы и их базовую валидацию.
    /// </summary>
    public class AddEditCategoryViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _description;
        private bool _canSave;
        private bool _hasNameError;

        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        /// <summary>
        /// Описание категории.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Свойство, блокирующее кнопку "Сохранить", если форма невалидна.
        /// </summary>
        public bool CanSave
        {
            get { return _canSave; }
            private set
            {
                _canSave = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Флаг ошибки для подсветки поля "Название" красной рамкой.
        /// </summary>
        public bool HasNameError
        {
            get { return _hasNameError; }
            private set
            {
                _hasNameError = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Конструктор формы категории.
        /// </summary>
        public AddEditCategoryViewModel()
        {
            ValidateForm();
        }

        /// <summary>
        /// Метод проверки валидности формы.
        /// Название категории не должно быть пустым.
        /// </summary>
        private void ValidateForm()
        {
            // Проверяем, что имя не пустое и не состоит из одних пробелов
            if (string.IsNullOrWhiteSpace(Name))
            {
                HasNameError = true;
                CanSave = false;
            }
            else
            {
                HasNameError = false;
                CanSave = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}