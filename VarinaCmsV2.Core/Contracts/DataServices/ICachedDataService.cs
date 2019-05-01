using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Contracts.DataServices
{
    public interface ICachedDataService<T, TKey> : IDataService<T, TKey> where T : class
    {
        Task<IEnumerable<T>> FromCacheAsync();
    }
}
