using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WpfUI.ViewModels;

namespace WpfUI.Commands;
public class BoldTextCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public BoldTextCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        if (parameter is RichTextBox rtb)
        {
            if ((FontWeight)rtb.Selection.GetPropertyValue(Inline.FontWeightProperty) == FontWeights.Bold)
            {
                rtb.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                rtb.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
        }
    }
}
