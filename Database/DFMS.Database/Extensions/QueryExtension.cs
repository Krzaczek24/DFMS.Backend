using Core.Database.Models;
using DFMS.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DFMS.Database.Extensions
{
    internal static class QueryExtension
    {
        public static async Task<bool> ActiveExists<T>(this IQueryable<T> set, int id) where T : IDbTableCommonModel, IActiveRecord
        {
            return await set.ActiveWhere(x => x.Id == id).AnyAsync();
        }

        public static IQueryable<T> ActiveWhere<T>(this IQueryable<T> query) where T : IActiveRecord
        {
            return query.Where(x => x.Active.HasValue && x.Active.Value);
        }

        public static IQueryable<T> ActiveWhere<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate) where T : IActiveRecord
        {
            return query.Where(predicate).ActiveWhere();
        }
    }
}
