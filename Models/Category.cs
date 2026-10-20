using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsWPF.Models
{
    /// <summary>
    /// Модель категории новости.
    /// Реализует INotifyPropertyChanged для поддержки привязки данных в MVVM.
    /// </summary>
    public class Category : INotifyPropertyChanged
    {
        // Статический счётчик для генерации уникальных ID
        private static int _nextId = 1;

        // Приватные поля с инициализацией 
        private string _name = "";
        private string _description = "";
        private int _countNews;

        /// <summary>
        /// Уникальный идентификатор категории.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Описание категории.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Количество новостей в этой категории (вычисляемое поле).
        /// </summary>
        public int CountNews
        {
            get { return _countNews; }
            set { _countNews = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Конструктор категории. Автоматически присваивает уникальный Id.
        /// </summary>
        public Category(string name, string description)
        {
            this.Id = _nextId;
            _nextId++;
            Name = name;
            Description = description;
            CountNews = 0;
        }

        /// <summary>
        /// Событие, уведомляющее интерфейс об изменении свойства.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Метод вызова события изменения свойства.
        /// Атрибут [CallerMemberName] автоматически подставляет имя свойства.
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Переопределение ToString для отображения в ComboBox.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}