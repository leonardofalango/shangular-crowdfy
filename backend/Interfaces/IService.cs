namespace backend.Model.Interfaces;

public interface IService<T> : IRepository<T>
{
    public Task<T?> GetById(int id);
    public Task<IEnumerable<T>> Take(int quantity);
}