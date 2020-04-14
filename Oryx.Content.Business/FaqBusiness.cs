using Oryx.Common.Business;
using Oryx.Content.DataAccessor;
using Oryx.Content.Model;
using Oryx.ViewModel;
using Oryx.ViewModel.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Content.Business
{
    public class FaqBusiness
    {
        public DataContentEntryAccessor ContentAccessor { get; set; }

        public DataFileEntryAccessor FileAccessor { get; set; }

        public DataCategoriesAccessor CategoryAccessor { get; set; }

        public const string PublishKey = "常见问答";


        private readonly SandBoxBusiness sandBoxBusiness;

        public FaqBusiness(DataContentEntryAccessor _ContentAccossor,
            DataFileEntryAccessor _FileAccessor,
            SandBoxBusiness _sandBoxBusiness,
            DataCategoriesAccessor _CategoryAccessor)
        {
            ContentAccessor = _ContentAccossor;
            FileAccessor = _FileAccessor;
            CategoryAccessor = _CategoryAccessor;
            sandBoxBusiness = _sandBoxBusiness;
        }

        public async Task<List<Categories>> GetKnowledgeCategoryList(ListQuery listQuery)
        {
            var category = await CategoryAccessor.ListAsync<Categories>(x => x.ParentCategory.Name == PublishKey && x.Name == listQuery.QueryKey, listQuery.PageIndex);
            return category.ToList();
        }


        public async Task<int> GetKnowledgeArticleAcount(Guid value)
        {
            return await CategoryAccessor.Count<ContentEntry>(x => x.Tags.Any(c => c.Name == "") && x.CategoryId == value);
        }
        public async Task<int> GetKnowledgeArticleAcount()
        {
            return await CategoryAccessor.Count<ContentEntry>(x => x.Category.Name == PublishKey);
        }

        public async Task<List<Categories>> GetKnowledgeCategoryList()
        {
            var category = await CategoryAccessor.ListAsync<Categories>(x => x.ParentCategory.Name == PublishKey);
            return category.ToList();
        }


        public async Task CreateKnowledgeCategory(Categories categories)
        {
            var parent = await CategoryAccessor.OneAsync<Categories>(x => x.Name == PublishKey);
            if (parent == null)
            {
                var cateModel = new Categories
                {
                    Name = PublishKey,
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now
                };
                await CategoryAccessor.Add(cateModel);
                parent = await CategoryAccessor.OneAsync<Categories>(x => x.Name == PublishKey);
            }

            if (parent.ChildrenCategories == null)
            {
                parent.ChildrenCategories = new List<Categories>();
            }
            categories.Id = Guid.NewGuid();
            categories.CreateTime = DateTime.Now;
            categories.ParentCategory = parent;

            parent.ChildrenCategories.Add(categories);
        }

        public async Task<Categories> GetKnowledgeCategory(Guid Id)
        {
            return await CategoryAccessor.OneAsync<Categories>(x => x.Id == Id);
        }

        public async Task EditKnowledgeCategory(Categories categories)
        {
            await CategoryAccessor.Update(categories);
        }

        public async Task DeleteKnowledgeCategory(Guid Id)
        {
            await CategoryAccessor.Delete<Categories>(x => x.Id == Id);
        }

        public async Task GetKnowledgeArticleList(ListQuery listQuery)
        {
            await ContentAccessor.ListAsync<ContentEntry>(x => x.Category.ParentCategory.Name == listQuery.QueryKey);
        }
        public async Task<List<ContentEntry>> GetFaqArticleList(int pageIndex, int pageSize)
        {
            return (await ContentAccessor.ListAsync<ContentEntry>(x => x.IsFaq, (pageIndex - 1) * pageSize, pageSize)).ToList();
        }
        public async Task<List<ContentEntry>> GetFaqArticleList(Guid Id, int pageIndex, int pageSize)
        {
            return (await ContentAccessor.ListAsync<ContentEntry>(x => x.Category.Id == Id, (pageIndex - 1) * pageSize, pageSize)).ToList();
        }

        public async Task CreateFaqArticle(ContentEntry contentEntry)
        {
            await ContentAccessor.Add(contentEntry);
        }

        public async Task<ContentEntry> GetKnowledgeArticle(Guid Id)
        {
            return await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == Id);
        }

        public async Task UpdateKnowledgeArticle(ContentEntry contentEntry)
        {
            await ContentAccessor.Update(contentEntry);
        }

        public async Task DeleteKnowledgeArticle(Guid Id)
        {
            await ContentAccessor.Delete<ContentEntry>(x => x.Id == Id);
        }
    }
}
