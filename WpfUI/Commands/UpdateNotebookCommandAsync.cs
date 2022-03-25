﻿using DataAccessLibrary.Entities;
using System.Threading.Tasks;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class UpdateNotebookCommandAsync : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public UpdateNotebookCommandAsync(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is Notebook notebook)
        {
            await _notesViewModel.StopEditingAsync(notebook);
        }
    }
}