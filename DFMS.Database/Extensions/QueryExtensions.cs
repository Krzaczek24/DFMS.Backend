using DFMS.Database.Models.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DFMS.Database.Extensions
{
    internal static class QueryExtensions
    {
        public static IQueryable<T> ActiveWhere<T>(this IQueryable<T> query) where T : DbTableCommonModel
        {
            return query.Where(x => x.Active == true);
        }

        public static IQueryable<T> ActiveWhere<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate) where T : DbTableCommonModel
        {
            return ActiveWhere(query).Where(predicate);
        }
    }
}
