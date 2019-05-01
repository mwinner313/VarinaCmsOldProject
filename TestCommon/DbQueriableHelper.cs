using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon
{
    public class DbQueriableHelper
    {

    }

    public class MockDbAsyncEnumerable<T> :IDbAsyncEnumerable<T> 
    {
        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            throw new NotImplementedException();
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }
    }
}
