using System;
using System.Windows.Input;

namespace SerialPortProxyService.Win.Common
{
    public class BaseCommand : ICommand
    {
        public BaseCommand(Action<object> executeAction)
        {
            ExecuteAction = executeAction;
        }

        public Action<object> ExecuteAction { get; set; }


        public Func<object, bool> CanExecuteAction { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (CanExecuteAction != null)
                return CanExecuteAction(parameter);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
        }
    }
}
