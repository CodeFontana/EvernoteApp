using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Entities;

public class Notebook : ObservableObject
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    private string _name;
    [Required]
    public string Name
    {
        get
        {
            return _name;
        }

        set
        {
            OnPropertyChanged(ref _name, value);
        }
    }

    private bool _isEditMode;
    [NotMapped]
    public bool IsEditMode
    {
        get
        {
            return _isEditMode;
        }

        set
        {
            OnPropertyChanged(ref _isEditMode, value);
            OnPropertyChanged(nameof(IsDisplayMode));
        }
    }

    [NotMapped]
    public bool IsDisplayMode { get => !IsEditMode; }

    public ObservableCollection<Note> Notes { get; set; } = new();
}
