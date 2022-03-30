using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Entities;

public class Notebook
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    //public int UserId { get; set; }

    [Required]
    public string Name { get; set; }

    public ObservableCollection<Note> Notes { get; set; } = new();
}
