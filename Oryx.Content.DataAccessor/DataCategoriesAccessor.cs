using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Oryx.CommonDbOperation;
using Oryx.Content.Model;
using Oryx.ViewModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oryx.Content.DataAccessor
{
    public class DataCategoriesAccessor : CommonAccessor
    {
        CommonDbContext dbContext;

        public DataCategoriesAccessor(CommonDbContext _dbContext, IDistributedCache _DistributedCache)
            : base(_dbContext, _DistributedCache)
        {
            dbContext = _dbContext;
        }

        //public async Task<Categories> GetCategoryId(Guid cid)
        //{
        //    return await dbContext.Categories.Include(x => x.ContentList).Include(x => x.ChildrenCategories).Include(x => x.ChildrenCategories.Select(c => c.ContentList)).FirstOrDefaultAsync(x => x.Id == cid);
        //} 

        public async Task<IList<UserRecentLogOutputViewModel>> GroupCategories(Expression<Func<ContentUserReadLog, bool>> expression, int skipCount, int pageSIze = 10, params string[] IncludesProperty)
        {
            var queryableFailed = dbContext.Set<ContentUserReadLog>()
                .Include("Categories")
                .Include("Categories.MediaResource")
                .Where(x => x.Categories.Status != ContentStatus.Close)
                .OrderByDescending(x => x.CreateTime)
                .GroupBy(x => x.CategoryId)
                .Skip(skipCount).Take(pageSIze)

                .Select(x => x.First())
                ;
            //.Where(expression)
            //.Where(x => x.Categories.Status != ContentStatus.Close)
            //.Include("Categories")
            //.Include("Categories.MediaResource")
            //.Distinct()
            //.OrderByDescending(x => x.CreateTime)
            // .Skip(skipCount).Take(pageSIze);
            //   .Include("ContentEntry")

            //.GroupBy(x => x.Categories);//..Where(expression).Skip(skipCount).Take(pageSIze);
            if (IncludesProperty != null && IncludesProperty.Length > 0)
            {
                foreach (var item in IncludesProperty)
                {
                    queryableFailed = queryableFailed.Include(item);
                }
            }
            try
            {
                var tmpData = await queryableFailed.ToListAsync();
                var resultData = tmpData.Select(x => new
                UserRecentLogOutputViewModel
                {
                    Categories = x.Categories,
                    //ContentEntry = x.Select(c => c.ContentEntry).Take(1).ToList()
                }).Distinct().ToList();
                return resultData;
            }
            catch (Exception exc)
            {
                return default(List<UserRecentLogOutputViewModel>);
            }
        }
    }
}
