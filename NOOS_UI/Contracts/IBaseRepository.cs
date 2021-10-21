using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOOS_UI.Contracts
{
    public interface IBaseRepository<T> where T : class    // setting standard faunctionalities fot the UI to interact with UI
    {
        Task<T> Get(string url, int id);

        Task<IList<T>> Get(string url);

        Task<bool> Create(string url, T obj);
        Task<bool> Update(string url, T obj);
        Task<bool> Delete(string url, int id);

    }
}
