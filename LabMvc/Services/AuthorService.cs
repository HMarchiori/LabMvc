using LabMvc.Models;
using LabMvc.Repository;

namespace LabMvc.Services;

public class AuthorService
{
    private readonly AuthorRepository _repository;

    public AuthorService(AuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Author>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Author> FindById(int id)
    {
        return await _repository.FindById(id);
    }

    public async Task<List<Author>> FindByLastName(string lastName)
    {
        return await _repository.FindByLastName(lastName);
    }
    
    public async Task Create(Author author)
    {
        await _repository.Create(author);
    }
    
    public async Task Update(Author author)
    {
        await _repository.Update(author);
    }
    
    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
}