namespace LabMvc.Repository.Interfaces;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T> FindById(int id);
    Task Create(T item);
    Task Update(T item);
    Task Delete(int id);
}