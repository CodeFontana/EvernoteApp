﻿using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class DeleteNoteCommandAsync : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public DeleteNoteCommandAsync(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is int noteId)
        {
            await _notesViewModel.DeleteNoteAsync(noteId);
        }
    }
}