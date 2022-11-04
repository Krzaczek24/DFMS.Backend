using DFMS.Database.Models.Base;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DFMS.Database.Tools
{
    internal interface IUpdateBuilder<TEntity> where TEntity : DbTableCommonModel, new()
    {
        public IUpdateBuilder<TEntity> Set<TProperty>(Expression<Func<TEntity, TProperty>> selector, TProperty value);
        public IUpdateBuilder<TEntity> Set<TProperty>(Expression<Func<TEntity, TProperty>> selector, Specifiable<TProperty> value);
        public IUpdateBuilder<TEntity> SetIfNotNullOrDefault<TProperty>(Expression<Func<TEntity, TProperty>> selector, TProperty value);
        public Task<int> Execute(string updaterLogin);
    }

    internal class UpdateBuilder<TEntity> : IUpdateBuilder<TEntity> where TEntity : DbTableCommonModel, new()
    {
        private AppDbContext context;
        private TEntity entity;
        public bool AnyChanges { get; private set; } = false;

        public UpdateBuilder(AppDbContext database, int id)
        {
            context = database;
            entity = new TEntity() { Id = id };
            try { context.Attach(entity); } catch { }
        }

        public IUpdateBuilder<TEntity> Set<TProperty>(Expression<Func<TEntity, TProperty>> selector, TProperty value)
        {
            var property = (PropertyInfo)((MemberExpression)selector.Body).Member;
            property.SetValue(entity, value);
            context.Entry(entity).Property(selector).IsModified = true;
            AnyChanges = true;
            return this;
        }

        public IUpdateBuilder<TEntity> Set<TProperty>(Expression<Func<TEntity, TProperty>> selector, Specifiable<TProperty> value)
        {
            return value.IsSpecified ? Set(selector, value.Value) : this;            
        }

        public IUpdateBuilder<TEntity> SetIfNotNullOrDefault<TProperty>(Expression<Func<TEntity, TProperty>> selector, TProperty value)
        {
            return value == null || value.Equals(default(TProperty)) ? this : Set(selector, value);
        }

        public async Task<int> Execute(string updaterLogin)
        {
            if (AnyChanges)
            {
                entity.ModifLogin = updaterLogin;
                entity.ModifDate = DateTime.Now;
                return await context.SaveChangesAsync();
            }

            return default;
        }
    }
}
