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
            if (rtb.Selection.Text.Length > 0)
            {
                rtb.Selection.ApplyPropertyValue(Inline.FontSizeProperty, (double)_notesViewModel.SelectedFontSize);
            }
            
            rtb.FontSize = (double)_notesViewModel.SelectedFontSize;
        }
    }
}
