﻿using DataAccessLibrary.Entities;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUI.ViewModels;

namespace WpfUI.Commands;

public class NewNoteCommand : CommandBaseAsync
{
    private readonly NotesViewModel _notesViewModel;

    public NewNoteCommand(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
    }

    public override bool CanExecute(object parameter)
    {
        Notebook selectedNotebook = parameter as Notebook;

        if (selectedNotebook != null)
        {
            return true;
        }

        return false;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        Notebook selectedNotebook = parameter as Notebook;
        await _notesViewModel.CreateNote(selectedNotebook.Id);
    }
}
