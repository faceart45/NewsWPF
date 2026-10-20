using System;
using System.Windows.Input;

namespace NewsWPF
{
    /// <summary>
    /// Класс-помощник для реализации интерфейса ICommand.
    /// Позволяет привязывать действия кнопок из XAML к методам в ViewModel.
    /// Сигнатуры методов соответствуют интерфейсу ICommand в .NET 10 (с nullable).
    /// </summary>
    internal class RelayCommand : ICommand
    {
        // Поле хранит само действие (метод), которое нужно выполнить
        private readonly Action<object?> _execute;

        // Поле хранит условие, которое проверяет, можно ли выполнить действие
        private readonly Func<object?, bool>? _canExecute;

        /// <summary>
        /// Конструктор команды.
        /// </summary>
        /// <param name="execute">Метод, который выполнится при нажатии</param>
        /// <param name="canExecute">Метод проверки (опционально, по умолчанию null = всегда активна)</param>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Событие, уведомляющее интерфейс об изменении состояния команды.
        /// Используем CommandManager.RequerySuggested для автоматического обновления.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Проверяет, можно ли выполнить команду (активна ли кнопка).
        /// </summary>
        public bool CanExecute(object? parameter)
        {
            // Если условие не задано — команда всегда выполнима
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        /// <summary>
        /// Выполняет действие при нажатии на кнопку.
        /// </summary>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}