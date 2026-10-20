using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NewsWPF.Models
{
    /// <summary>
    /// Модель новости.
    /// </summary>
    public class News : INotifyPropertyChanged
    {
        private static int _nextId = 1;

        private string _title = "";
        private Category? _category;
        private DateTime _dateOfPublication;
        private string _author = "";
        private int _numberOfView;
        private string _status = "";
        private string _text = "";

        public int Id { get; private set; }

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public Category? Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged(); }
        }

        public DateTime DateOfPublication
        {
            get { return _dateOfPublication; }
            set { _dateOfPublication = value; OnPropertyChanged(); }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; OnPropertyChanged(); }
        }

        public int NumberOfView
        {
            get { return _numberOfView; }
            set { _numberOfView = value; OnPropertyChanged(); }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Конструктор новости.
        /// </summary>
        public News(string title, Category category, string author, string status, string text)
        {
            this.Id = _nextId;
            _nextId++;
            Title = title;
            Category = category;
            DateOfPublication = DateTime.Now;
            Author = author;
            NumberOfView = 0;
            Status = status;
            Text = text;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}