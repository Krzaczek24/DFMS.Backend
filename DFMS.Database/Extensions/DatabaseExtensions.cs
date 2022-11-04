using DFMS.Database.Models.Base;
using DFMS.Database.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DFMS.Database.Extensions
{
    internal static class DatabaseExtensions
    {
        public static IUpdateBuilder<TEntity> Update<TEntity>(this AppDbContext database, int id)
            where TEntity : DbTableCommonModel, new() => new UpdateBuilder<TEntity>(database, id);

        public static async Task<int> Remove<TEntity>(this AppDbContext database, int id)
            where TEntity : DbTableCommonModel, new()
        {
            try
            {
                var entity = new TEntity() { Id = id };
                database.Attach(entity);
                database.Remove(entity);
                return await database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }

            return 0;
        }

        public static string GetInnerExceptionMessage(this Exception ex) => ex.InnerException?.Message ?? ex.Message;
        public static bool IsDuplicateEntryException(this DbUpdateException ex) => ex.InnerException?.Message?.StartsWith("Duplicate entry") ?? false;
        public static bool IsCannotDeleteOrUpdateExcpetion(this DbUpdateException ex) => ex.InnerException?.Message?.StartsWith("Cannot delete or update") ?? false;

    }
}
