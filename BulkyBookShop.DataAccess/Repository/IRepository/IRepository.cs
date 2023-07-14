using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBookShop.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T -> Category (zay elsequence of the categoryController
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null); //= var categoryFromDbFirst = _db.Categories.FirstOrDefault(c => c.Name == "id");
        // string? includeProperties = null ----> is to handle the navigation properties
        IEnumerable<T> GetAll(string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

        //update not the a constant logic
    }
}
