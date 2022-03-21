using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class ItalicTextCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public ItalicTextCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        if (parameter is RichTextBox rtb)
        {
            if ((FontStyle)rtb.Selection.GetPropertyValue(Inline.FontStyleProperty) == FontStyles.Italic)
            {
                rtb.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                rtb.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
        }
    }
}
