using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBaseService<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        Guid Create(T objectDTO);
        bool Update(T objectDTO);
        bool Delete(Guid id);
    }
}
