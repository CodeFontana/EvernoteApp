using DataAccessLibrary.Entities;
using System.Windows.Input;
using WpfUI.Commands;

namespace WpfUI.ViewModels;
public class LoginViewModel : ViewModelBase
{
    private User _user;

    public LoginViewModel()
    {
        RegisterCommand = new RegisterUserCommand(this);
        LoginCommand = new LoginUserCommand(this);
    }

    public User User
    {
        get { return _user; }
        set { _user = value; }
    }

    public ICommand RegisterCommand { get; set; }
    public ICommand LoginCommand { get; set; }
}
