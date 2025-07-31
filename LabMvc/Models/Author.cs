using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabMvc.Models;

public class Author
{
    public Author()
    {
    }
    public Author(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
    
    public int Id { get; init; } 
    [StringLength(80), Required]
    public required string FirstName { get; init; } 
    [StringLength(80), Required]
    public required string LastName { get; init; } 
    
    
    [JsonIgnore] 
    public ICollection<Book> Books { get; init; } = new List<Book>();
}