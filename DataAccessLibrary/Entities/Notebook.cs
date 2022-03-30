using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Entities;

public class Notebook : ObservableObject
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    //public int UserId { get; set; }

    [Required]
    public string Name { get; set; }

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
            _isEditMode = value;
            OnPropertyChanged(nameof(IsEditMode));
            OnPropertyChanged(nameof(IsDisplayMode));
        }
    }

    [NotMapped]
    public bool IsDisplayMode { get => !IsEditMode; }

    public ObservableCollection<Note> Notes { get; set; } = new();
}
