using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LabMvc.Models;

public class Book
{

    public Book()
    { }

    public Book(int id, string title)
    {
        Id = id;
        Title = title;
    }
    public int Id { get; init; } 
    
    [StringLength(80), Required]
    public string? Title { get; init; } 
    
    [JsonIgnore] 
    public ICollection<Author> Authors { get; init; } = new List<Author>();
    [JsonIgnore] 
    public ICollection<Loan> Loans { get; init; } = new List<Loan>();
    
}