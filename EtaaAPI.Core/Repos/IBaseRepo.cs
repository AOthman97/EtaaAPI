using System.Linq.Expressions;

namespace EtaaAPI.Core.Repos
{
    // The <T> here after specifying that it must be a class means we're accepting any class
    public interface IBaseRepo<T> where T : class
    {
        // The return type is also generic which we didn't specify what's going to be returned
        // Add more methods here like get all, add, update, remove, list...etc
        // The implementation of these interfaces will be in the EF project
        Task<T> GetById(int Id);
        IEnumerable<T> GetAll();
        // This is like a dynamic search that's going to be implementd in the controller, So we can search by
        // any property of the model without initially defining it here
        // We've added an optional array of values that would be used to include any other models that we need
        Task<T> Find(Expression<Func<T, bool>> Match, string []? Includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> Match, string[]? Includes = null);
        T Add(T entity);
        IEnumerable<T> AddMultiple(IEnumerable<T> entities);
    }
}