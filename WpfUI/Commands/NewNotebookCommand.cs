using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class NewNotebookCommand : CommandBaseAsync
{
    private readonly MainViewModel _mainViewModel;

    public NewNotebookCommand(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await _mainViewModel.CreateNotebook();
    }
}
