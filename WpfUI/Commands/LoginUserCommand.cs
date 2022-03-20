using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class LoginUserCommand : CommandBaseAsync
{
    private readonly LoginViewModel _loginViewModel;

    public LoginUserCommand(LoginViewModel loginViewModel)
    {
        _loginViewModel = loginViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await Task.Delay(1);
    }
}
