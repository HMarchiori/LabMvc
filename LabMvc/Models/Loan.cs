using System.ComponentModel.DataAnnotations;

namespace LabMvc.Models;

public class Loan
{
    public Loan() {} 

    public Loan(int id, DateTime departDate, DateTime devolutionDate, bool isDelivered, int bookId)
    {
        Id = id;
        DepartDate = departDate;
        DevolutionDate = devolutionDate;
        IsDelivered = isDelivered;
        BookId = bookId;
    }

    public int Id { get; init; }
    [Required]
    public DateTime DepartDate { get; init; }
    public DateTime DevolutionDate { get; init; }
    [Required]
    public bool IsDelivered { get; set; }

    public int BookId { get; init; }  
    public Book? Book { get; init; }   
}