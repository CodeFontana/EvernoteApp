using System.Windows.Controls;
using System.Windows.Documents;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class FontFamilyChangedCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public FontFamilyChangedCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        if (parameter is RichTextBox rtb)
        {
            rtb.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, _notesViewModel.SelectedFont);
        }
    }
}
