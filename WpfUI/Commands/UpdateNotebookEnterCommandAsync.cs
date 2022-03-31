﻿using DataAccessLibrary.Entities;
using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class UpdateNotebookEnterCommandAsync : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public UpdateNotebookEnterCommandAsync(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await _notesViewModel.StopEditingAsync();
    }
}
