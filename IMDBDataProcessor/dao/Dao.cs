using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBDataProcessor.dao
{
    public interface IDao<T, in S>
    {
        T Get(S id);
        IList<T> GetAll();
        void Save(T item);
        void SaveAll(IList<T> items);
    }
}
