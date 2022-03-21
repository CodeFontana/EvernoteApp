using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WpfUI.ViewModels;

namespace WpfUI.Commands;
public class NoteTextChangedCommand : CommandBase
{
    private readonly NotesViewModel _notesViewModel;

    public NoteTextChangedCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override void Execute(object parameter)
    {
        if (parameter is RichTextBox rtb)
        {
            _notesViewModel.CurrentNoteCharCount = (new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd)).Text.Length - 2;
        }
    }
}
