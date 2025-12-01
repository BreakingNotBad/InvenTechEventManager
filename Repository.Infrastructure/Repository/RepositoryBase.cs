using Contract.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using System.Linq.Expressions;

namespace Repository.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;
        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            // ถ้า trackChanges เป็น false ให้ใช้ AsNoTracking()
            // ถ้าเป็น true (ต้องการแก้ไขข้อมูล) ให้ใช้แบบปกติ
            return !trackChanges ?
                _context.Set<T>().AsNoTracking() :
                _context.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ?
                _context.Set<T>().Where(expression).AsNoTracking() :
                _context.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
