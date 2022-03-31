﻿using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;
public class UpdateNoteCommand : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public UpdateNoteCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await _notesViewModel.StopEditingNoteAsync();
    }
}
