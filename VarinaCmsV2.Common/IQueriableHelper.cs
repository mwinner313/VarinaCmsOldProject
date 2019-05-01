using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;

namespace VarinaCmsV2.Common
{
    public static class QueriableHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, Pagenation pagenation) where T : class
        {
            return pagenation.NoPaginate ? query : query.Skip(pagenation.SkipSize).Take(pagenation.PageSize);
        }
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageSize,int pageNumber) where T : class
        {
            return query.Skip((pageNumber - 1)*pageSize).Take(pageSize);
        }
        public static int GetAllPagesCount<T>(this IQueryable<T> query, int pageSize) where T : class
        {
            return (int) Math.Ceiling((double) query.Count() / pageSize);
        }
        /// <summary>
        /// returns selected property deeply like a tree
        /// </summary>
        /// <typeparam name="T">type of incomming collection</typeparam>
        /// <typeparam name="P">selected prop type</typeparam>
        /// <param name="queriable"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public static List<P> SelectDeep<T, P>(this IEnumerable<T> queriable,Func<T,P> select) where T : class, IHaveChild<T>
        {
            List<P> list=new List<P>();
            foreach (var x in queriable)
            {
                list.Add(select(x));
                if(x.Childs!=null &&x.Childs.Count!=0)
                list.AddRange(x.Childs.SelectDeep(select));
            }
            return list;
        }
        /// <summary>
        /// returns selected property deeply like a tree
        /// </summary>
        /// <typeparam name="T">type of incomming collection</typeparam>
        /// <typeparam name="P">selected prop type</typeparam>
        /// <param name="queriable"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public static List<P> SelectDeepFromCollection<T, P>(this ICollection<T> queriable, Func<T, P> select) where T : class, IHaveCollectionChild<T>
        {
            List<P> list = new List<P>();
            foreach (var x in queriable)
            {
                list.Add(select(x));
                if (x.Childs != null && x.Childs.Count != 0)
                    list.AddRange(x.Childs.SelectDeepFromCollection(select));
            }
            return list;
        }
        public static void ForEachDeep<T>(this IEnumerable<T> queriable, Action<T> action) where T : class, IHaveChild<T>
        {
            foreach (var x in queriable)
            {
                action(x);
                if (x.Childs != null && x.Childs.Count != 0)
                    x.Childs.ForEachDeep(action);
            }
        }
    }
}
