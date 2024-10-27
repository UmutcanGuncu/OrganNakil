namespace OrganNakil.Application.Interfaces;

public interface IGenericRepository<T> where T:class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T t);
    Task UpdateAsync(T t);
    Task DeleteAsync(T t);

}