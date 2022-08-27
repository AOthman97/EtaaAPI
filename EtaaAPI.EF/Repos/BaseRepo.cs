using EtaaAPI.Core.Repos;
using Microsoft.EntityFrameworkCore;
using MoviesProject.EF;
using System.Linq.Expressions;

namespace EtaaAPI.EF.Repos
{
    // The <T> here after specifying that it must be a class means we're accepting any class
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        // It's protected because this project is the only one allowed to deal with the DB directly
        protected ApplicationDbContext _context;

        public BaseRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll() => _context.Set<T>().AsNoTracking().ToList();

        // Because we don't know which model we're accessing here we're using the given T class with the Set
        // method
        public async Task<T> GetById(int Id) => await _context.Set<T>().FindAsync(Id);

        public async Task<T> GetById(Expression<Func<T, bool>> Match, string[]? Includes = null)
        {
            IQueryable<T> Query = _context.Set<T>();

            if (Includes != null)
                foreach (var Included in Includes)
                    Query = Query.Include(Included);

            return await Query.SingleOrDefaultAsync(Match);
        }

        // We've added an optional array of values that would be used to include any other models that we need
        public async Task<T> Find(Expression<Func<T, bool>> Match, string[]? Includes = null)
        {
            // Old: This would just directly query the first or default element and it won't include any
            // other model
            //return await _context.Set<T>().FirstOrDefaultAsync();

            // New
            IQueryable<T> Query = _context.Set<T>();

            if(Includes != null)
                foreach (var Included in Includes)
                    Query = Query.Include(Included);

            return await Query.FirstOrDefaultAsync(Match);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> Match, string[]? Includes = null)
        {
            // Old: This would just directly query the first or default element and it won't include any
            // other model
            //return await _context.Set<T>().FirstOrDefaultAsync();

            // New
            IQueryable<T> Query = _context.Set<T>();

            // The included here refers to all models that are also included in this query so that instead of just
            // getting the DistrictId and the District object would be null, We'll return the whole District object
            // that has the same ID as the one we're sending
            if (Includes != null)
                foreach (var Included in Includes)
                    Query = Query.Include(Included);

            // We're using 'Where' here because we're selecting more than one element
            return Query.AsNoTracking().Where(Match).ToList();
        }

        public IEnumerable<T> FindAll(string[]? Includes = null)
        {
            // Old: This would just directly query the first or default element and it won't include any
            // other model
            //return await _context.Set<T>().FirstOrDefaultAsync();

            // New
            IQueryable<T> Query = _context.Set<T>();

            // The included here refers to all models that are also included in this query so that instead of just
            // getting the DistrictId and the District object would be null, We'll return the whole District object
            // that has the same ID as the one we're sending
            if (Includes != null)
                foreach (var Included in Includes)
                    Query = Query.Include(Included);

            // We're using 'Where' here because we're selecting more than one element
            return Query.AsNoTracking().ToList();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            // The save changes part was removed because now we're using unit of work and there we have a Complete() method
            // that saves the changes in the DB and returns the number of rows effected. We're goin to use it in the 
            // Contributors controller first
            //_context.SaveChanges();

            return entity;
        }

        public IEnumerable<T> AddMultiple(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            //_context.SaveChanges();

            return entities;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            //_context.SaveChanges();

            return entity;
        }

        public bool Delete(int Id)
        {
            var Model = _context.Set<T>().Find(Id);
            if (Model != null)
            {
                _context.Remove(Model);
                return true;
            }
            else
                return false;
        }
    }
}