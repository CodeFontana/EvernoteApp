using System;
using System.Windows.Input;

namespace WpfUI.ViewModels.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly LoginViewModel _loginViewModel;

        public event EventHandler CanExecuteChanged;

        public LoginCommand(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
