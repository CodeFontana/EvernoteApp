using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class UnderlineTextCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public UnderlineTextCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        if (parameter is RichTextBox rtb)
        {
            if (rtb.Selection.GetPropertyValue(Inline.TextDecorationsProperty) == TextDecorations.Underline)
            {
                rtb.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
            else
            {
                rtb.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
    }
}
