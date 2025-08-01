using LabMvc.Models;
using LabMvc.Repository;

namespace LabMvc.Services;

public class LoanService
{
    private readonly LoanRepository _repository;

    public LoanService(LoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Loan>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<Loan> FindById(int id)
    {
        return await _repository.FindById(id);
    }
    
    public async Task Create(Loan loan)
    {
        await _repository.Create(loan);
    }
    
    public async Task Update(Loan loan)
    {
        await _repository.Update(loan);
    }
    
    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
    
    public async Task<Loan?> FindLoanByBookId(int bookId)
    {
        return await _repository.FindLoanByBookId(bookId);
    }
    
}