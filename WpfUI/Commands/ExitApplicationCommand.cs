using System.Windows;

namespace WpfUI.Commands;
public class ExitApplicationCommand : CommandBase
{
    public override void Execute(object parameter)
    {
        Application.Current.Shutdown();
    }
}
