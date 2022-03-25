using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class LoginUserCommandAsync : CommandBaseAsync
{
    private readonly LoginViewModel _loginViewModel;

    public LoginUserCommandAsync(LoginViewModel loginViewModel)
    {
        _loginViewModel = loginViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await Task.Delay(1);
    }
}
