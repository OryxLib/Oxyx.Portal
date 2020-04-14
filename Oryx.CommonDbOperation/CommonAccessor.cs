using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.CommonDbOperation
{
    public class CommonAccessor
    {
        private CommonDbContext ShopDbContext { get; set; }
        private readonly IDistributedCache DistributedCache;
        public CommonAccessor(
            CommonDbContext _ShopDbContext,
            IDistributedCache _DistributedCache
            )
        {
            ShopDbContext = _ShopDbContext;
            DistributedCache = _DistributedCache;
        }

        public DbSet<T> All<T>()
            where T : class
        {
            return ShopDbContext.Set<T>();
        }

        public T Entry<T>(T model)
            where T : class
        {
            return ShopDbContext.Entry(model).Entity;
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> expression)
            where T : class, IEntityModel
        {
            return ShopDbContext.Set<T>().Where(expression);
        }

        public async Task<bool> Any<T>(Expression<Func<T, bool>> expression)
                        where T : class
        {
            return await ShopDbContext.Set<T>().AnyAsync(expression);
        }

        public async Task<T> OneAsync<T>(Expression<Func<T, bool>> expression)
            where T : class
        {
            return await ShopDbContext.Set<T>().FirstOrDefaultAsync(expression);
        }
        public async Task<T> OneAsync<T>(Expression<Func<T, bool>> expression, params string[] IncludesProperty)
            where T : class
        {
            IQueryable<T> queryableFaild = ShopDbContext.Set<T>().Where(expression);

            if (IncludesProperty != null && IncludesProperty.Length > 0)
            {
                foreach (var item in IncludesProperty)
                {
                    queryableFaild = queryableFaild.Include(item);
                }
            }
            return await queryableFaild.FirstOrDefaultAsync();
        }

        public async Task<IList<T>> ListAsync<T>()
             where T : class
        {
            return await ShopDbContext.Set<T>().ToListAsync();
        }
        public async Task<IList<T>> ListAsync<T>(int skipCount, int pageSIze = 10)
            where T : class, IEntityModel
        {
            return await ShopDbContext.Set<T>().OrderBy(x => x.CreateTime).Skip(skipCount).Take(pageSIze).ToListAsync();
        }

        public async Task<IList<T>> ListAsync<T>(Expression<Func<T, bool>> expression, params string[] IncludesProperty)
        where T : class, IEntityModel
        {
            IQueryable<T> queryableFailed = ShopDbContext.Set<T>().Where(expression);
            if (IncludesProperty != null && IncludesProperty.Length > 0)
            {
                foreach (var item in IncludesProperty)
                {
                    queryableFailed = queryableFailed.Include(item);
                }
            }
            return await queryableFailed.OrderBy(x => x.CreateTime).ToListAsync();
        }

        public async Task<IList<T>> ListAsync<T>(Expression<Func<T, bool>> expression, int skipCount, int pageSIze = 10, params string[] IncludesProperty)
            where T : class, IEntityModel
        {
            IQueryable<T> queryableFailed = ShopDbContext.Set<T>().Where(expression).Skip(skipCount).Take(pageSIze);
            if (IncludesProperty != null && IncludesProperty.Length > 0)
            {
                foreach (var item in IncludesProperty)
                {
                    queryableFailed = queryableFailed.Include(item);
                }
            }
            return await queryableFailed.OrderBy(x => x.CreateTime).ToListAsync();
        }

        public async Task<IList<T>> ListAsync<T>(Expression<Func<T, bool>> expression, int skipCount = 0, int pageSIze = 10)
            where T : class, IEntityModel
        {
            var hasSoftDelete = isInsterfaceOf<T>(typeof(ISoftDeleteModel));
            if (hasSoftDelete)
            {
                var softDeleteProperty = typeof(T).GetProperty("IsSoftDelete");
                var parameter = Expression.Parameter(typeof(T));
                var comparison = Expression.Equal(Expression.Property(parameter, softDeleteProperty), Expression.Constant(true));

                var discountFilterExpression = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                return await ShopDbContext.Set<T>().Where(expression).Where(discountFilterExpression).OrderBy(x => x.CreateTime).Skip(skipCount).Take(pageSIze).ToListAsync();
            }
            else
            {
                return await ShopDbContext.Set<T>().Where(expression).OrderBy(x => x.CreateTime).Skip(skipCount).Take(pageSIze).ToListAsync();
            }
        }

        public async Task<IList<T>> ListOrderByDescendingAsync<T>(int skipCount = 0, int pageSIze = 10)
            where T : class, IEntityModel
        {
            IQueryable<T> queryableFailed = ShopDbContext.Set<T>().OrderByDescending(x => x.CreateTime).Skip(skipCount).Take(pageSIze);
            return await queryableFailed.ToListAsync();
        }

        public async Task<IList<T>> ListOrderByDescendingAsync<T>(Expression<Func<T, bool>> expression, int skipCount = 0, int pageSIze = 10, params string[] IncludesProperty)
            where T : class, IEntityModel
        {
            IQueryable<T> queryableFailed = ShopDbContext.Set<T>().OrderByDescending(x => x.CreateTime).Where(expression).Skip(skipCount).Take(pageSIze);
            if (IncludesProperty != null && IncludesProperty.Length > 0)
            {
                foreach (var item in IncludesProperty)
                {
                    queryableFailed = queryableFailed.Include(item);
                }
            }
            return await queryableFailed.ToListAsync();
        }

        public async Task<int> Count<T>(Expression<Func<T, bool>> expression)
                 where T : class
        {
            return await ShopDbContext.Set<T>().Where(expression).CountAsync();
        }
        public async Task<int> Count<T>()
                 where T : class
        {
            return await ShopDbContext.Set<T>().CountAsync();
        }


        public async Task Add<T>(T model)
            where T : class
        {
            await ShopDbContext.Set<T>().AddAsync(model);
            await ShopDbContext.SaveChangesAsync();
        }
        public async Task AddRange<T>(List<T> model)
            where T : class
        {
            await ShopDbContext.Set<T>().AddRangeAsync(model);
            await ShopDbContext.SaveChangesAsync();
        }
        public async Task<bool> Update<T>(T model)
             where T : class
        {
            try
            {
                ShopDbContext.Entry<T>(model);
                ShopDbContext.Set<T>().Update(model);
                await ShopDbContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                return false;
            }
            return true;
        }

        public async Task UpdateRange<T>(List<T> modelList)
            where T : class, new()
        {
            ShopDbContext.Set<T>().UpdateRange(modelList);
            await ShopDbContext.SaveChangesAsync();
        }

        public async Task AddOrUpdate<T>(T model)
            where T : class, IEntityModel
        {
            var dbDataModel = await ShopDbContext.Set<T>().FirstOrDefaultAsync<T>(x => x.Id == model.Id);

            if (dbDataModel == null)
            {
                await ShopDbContext.Set<T>().AddAsync(model);
            }
            else
            {
                var TType = typeof(T);
                var TProperty = TType.GetProperties();
                foreach (var prop in TProperty)
                {
                    switch (prop.Name)
                    {
                        case "Id":
                        case "CreateTime":
                            continue;
                        default:
                            prop.SetValue(dbDataModel, prop.GetValue(model));
                            break;
                    }
                }
                ShopDbContext.Set<T>().Update(model);
            }
            await ShopDbContext.SaveChangesAsync();
        }

        public async Task<bool> Delete<T>(T model)
              where T : class
        {
            try
            {
                var hasSoftDelete = isInsterfaceOf<T>(typeof(ISoftDeleteModel));
                if (hasSoftDelete)
                {
                    var typeOfT = typeof(T);
                    var propOfSoftDelete = typeOfT.GetProperty("IsSoftDelete");
                    propOfSoftDelete.SetValue(model, true);
                }
                else
                {
                    ShopDbContext.Set<T>().Remove(model);
                }
                await ShopDbContext.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw;
            }
            return true;
        }

        public async Task<bool> Delete<T>(Expression<Func<T, bool>> expression)
               where T : class
        {
            try
            {
                var targetModel = await ShopDbContext.Set<T>().FirstOrDefaultAsync(expression);
                await Delete(targetModel);
            }
            catch (Exception exc)
            {
                throw;
            }
            return true;
        }

        private static bool isInsterfaceOf<T>(Type targetInterface) where T : class
        {
            var typeOfT = typeof(T);
            var typeOfInterface = targetInterface;
            var typeFilter = new TypeFilter((t, o) =>
            {
                if (t == (Type)o)
                    return true;
                return false;
            });
            var targetInterfaceArr = typeOfT.FindInterfaces(typeFilter, typeOfInterface);
            if (targetInterfaceArr.Length > 0)
            {
                return true;
            }
            return false;
        }

        public async Task SaveAsync()
        {
            await ShopDbContext.SaveChangesAsync();
        }
    }
}
