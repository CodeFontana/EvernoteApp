using DataAccessLibrary.Entities;
using WpfUI.ViewModels.Commands;

namespace WpfUI.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            RegisterCommand = new(this);
            LoginCommand = new(this);
        }

        private User _user;

        public User User
        {
            get { return User; }
            set { User = value; }
        }

        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
    }
}
