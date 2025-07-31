using LabMvc.Models;
using LabMvc.Repository;

namespace LabMvc.Services;

public class BookService
{
    private readonly BookRepository _repository;

    public BookService(BookRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Book>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Book> FindById(int id)
    {
        return await _repository.FindById(id);
    }
    
    public async Task Create(Book book)
    {
        await _repository.Create(book);
    }
    
    public async Task Update(Book book)
    {
        await _repository.Update(book);
    }
    
    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
    
    public async Task<List<Book>> GetByAuthorId(int authorId)
    {
        return await _repository.GetByAuthorId(authorId);
    }
}