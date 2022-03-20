using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;
public class RegisterUserCommand : CommandBaseAsync
{
    private readonly LoginViewModel _loginViewModel;

    public RegisterUserCommand(LoginViewModel loginViewModel)
    {
        _loginViewModel = loginViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await Task.Delay(1);
    }
}
