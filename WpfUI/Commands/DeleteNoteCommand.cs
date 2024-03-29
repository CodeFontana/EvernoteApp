﻿using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class DeleteNoteCommand : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public DeleteNoteCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await _notesViewModel.DeleteNoteAsync(_notesViewModel.SelectedNote.Id);
    }
}
