using System.Windows.Controls;
using System.Windows.Documents;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class FontSizeChangedCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public FontSizeChangedCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        if (parameter is RichTextBox rtb)
        {
            rtb.Selection.ApplyPropertyValue(Inline.FontSizeProperty, _notesViewModel.SelectedFontSize);
        }
    }
}
