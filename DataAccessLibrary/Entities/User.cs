using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "Last name must be less than 50 characters")]
    public string Lastname { get; set; }

    [Required]
    [MaxLength(50, ErrorMessage = "User name must be less than 50 characters")]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    // public ObservableCollection<Notebook> Notebooks { get; set; }
}
