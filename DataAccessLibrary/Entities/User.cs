using System.ComponentModel.DataAnnotations;

namespace DataAccessLibrary.Entities;

public class User
{
    public int Id { get; set; }

    [MaxLength(50, ErrorMessage = "Name must be less than 50 characters")]
    public string Name { get; set; }

    [MaxLength(50, ErrorMessage = "Last name must be less than 50 characters")]
    public string Lastname { get; set; }

    [MaxLength(50, ErrorMessage = "User name must be less than 50 characters")]
    public string Username { get; set; }

    public string Password { get; set; }
}
