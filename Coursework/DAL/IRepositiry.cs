
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IRepositiry<T, TId> where T : class, IEntity<TId>
    {
        T GetById(TId id);
        List<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(TId id);

        TId GetNextId();
    }
}
