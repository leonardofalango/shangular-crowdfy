using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
namespace backend.Model.Interfaces;

public interface IRepository<T>
{
    Task<bool> Create(T obj);
    Task<bool> Update(T obj);
    Task<bool> Delete(T obj);
    Task<List<T>> Filter(Expression<Func<T, bool>> exp);
}