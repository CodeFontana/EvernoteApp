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
            _title = value;
            OnPropertyChanged(nameof(Title));
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
            _updatedAt = value;
            OnPropertyChanged(nameof(UpdatedAt));
        }
    }

    public string FileLocation { get; set; }
}
