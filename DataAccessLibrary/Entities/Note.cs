using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Entities;

public class Note : ObservableObject
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int NotebookId { get; set; }

    private string _title;
    [Required]
    public string Title
    {
        get
        {
            return _title;
        }

        set
        {
            OnPropertyChanged(ref _title, value);
        }
    }

    public DateTime CreatedAt { get; set; }

    private DateTime _updatedAt;
    public DateTime UpdatedAt
    {
        get
        {
            return _updatedAt;
        }

        set
        {
            OnPropertyChanged(ref _updatedAt, value);
        }
    }

    public string FileLocation { get; set; }

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
}
