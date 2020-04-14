using Microsoft.EntityFrameworkCore;
using Oryx.Common.Business;
using Oryx.Common.Model.SandBox;
using Oryx.Content.DataAccessor;
using Oryx.Content.Model;
using Oryx.Content.Model.ContentEntryExtension;
using Oryx.Content.Model.SocialExtension;
using Oryx.Social.Model;
using Oryx.UserAccount.Model.UserAccountExtend;
using Oryx.Utilities;
using Oryx.ViewModel;
using Oryx.ViewModel.Content;
using Oryx.ViewModel.CRUD;
using Oryx.ViewModel.Social.PostEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oryx.Content.Business
{
    public class ContentBusiness
    {
        public DataContentEntryAccessor ContentAccessor { get; set; }

        public DataFileEntryAccessor FileAccessor { get; set; }

        public DataCategoriesAccessor CategoryAccessor { get; set; }


        private readonly SandBoxBusiness sandBoxBusiness;

        public ContentBusiness(DataContentEntryAccessor _ContentAccossor,
            DataFileEntryAccessor _FileAccessor,
            SandBoxBusiness _sandBoxBusiness,
            DataCategoriesAccessor _CategoryAccessor)
        {
            ContentAccessor = _ContentAccossor;
            FileAccessor = _FileAccessor;
            CategoryAccessor = _CategoryAccessor;
            sandBoxBusiness = _sandBoxBusiness;
        }

        public async Task<List<ContentEntry>> GetContentListByCategoryName(string categoryName, int num)
        {
            return await ContentAccessor.All<ContentEntry>().Where(x => x.Category.Name == categoryName & !x.IsFaq)
                 .OrderByDescending(x => x.IsTop)
                 .OrderByDescending(x => x.Order)
                 .OrderByDescending(x => x.CreateTime)
                 .Take(num)
                 .ToListAsync();
        }

        public async Task<Tuple<IList<ContentEntry>, int>> GetContentListByUserId(Guid userId, int skipCount, int pageSize)
        {
            var total = await ContentAccessor.Count<ContentEntry>(x => x.ContentEntryInfo.UserAccountId == userId);
            var contentList = await ContentAccessor.ListAsync<ContentEntry>(x => x.ContentEntryInfo.UserAccountId == userId, skipCount, pageSize);
            return Tuple.Create(contentList, total);
        }


        public async Task<Tuple<IList<Comment>, int>> GetCommentByUserId(Guid userId, int skipCount, int pageSize)
        {
            var total = await ContentAccessor.Count<Comment>(x => x.UserAccountId == userId);
            var contentList = await ContentAccessor.ListAsync<Comment>(x => x.UserAccountId == userId, skipCount, pageSize, "ContentEntry");
            return Tuple.Create(contentList, total);
        }

        public async Task<Tuple<IList<UserAccountLoginLog>, int>> GetLoginLogByUserId(Guid userId, int skipCount, int pageSize)
        {
            var total = await ContentAccessor.Count<UserAccountLoginLog>(x => x.UserAccountEntryId == userId);
            var contentList = await ContentAccessor.ListAsync<UserAccountLoginLog>(x => x.UserAccountEntryId == userId, skipCount, pageSize);
            return Tuple.Create(contentList, total);
        }

        public async Task<Categories> GetCategoryByName(string name)
        {
            return await ContentAccessor.OneAsync<Categories>(x => x.Name == name);
        }

        public async Task<Tuple<List<ContentEntry>, int>> GetContentListByCategoryName(string categoryName, int skipCount, int pageSize)
        {
            var dbSet = ContentAccessor.All<ContentEntry>().Where(x => x.Category.Name == categoryName & !x.IsFaq);
            var contetnList = await dbSet
              .Include("MediaResource")
              .OrderByDescending(x => x.IsTop)
              .OrderByDescending(x => x.Order)
              .OrderByDescending(x => x.CreateTime)
              .Skip(skipCount)
              .Take(pageSize)
              .ToListAsync();
            var total = await dbSet.CountAsync();
            return Tuple.Create(contetnList, total);
            //throw new NotImplementedException();
        }

        public async Task<Dictionary<Tuple<Guid, string>, List<ContentEntry>>> GetContentWithCategory(string pCategoryName, int pageSize)
        {
            var categoryList = await ContentAccessor.All<Categories>()
                                    .Where(x => x.ParentCategory.Name == pCategoryName)
                                    .ToListAsync();
            var dictContent = new Dictionary<Tuple<Guid, string>, List<ContentEntry>>();
            foreach (var categoryItem in categoryList)
            {
                var contentList = await ContentAccessor.All<ContentEntry>().Where(x => x.Category.Id == categoryItem.Id).ToListAsync();
                var tuple = Tuple.Create(categoryItem.Id, categoryItem.Name);
                dictContent.Add(tuple, contentList);
            }
            return dictContent;

            //var contentList = await ContentAccessor.All<ContentEntry>()
            //                                .Where(x => x.Category.Name == pCategoryName)
            //                                .Select(x => new
            //                                {
            //                                    Category = x.Category,
            //                                    Content = x
            //                                })
            //                                .GroupBy(x => x.Category.Name)
            //                                .ToDictionaryAsync(x => x.Key, c => c.Select(v => v.Content));
            //var cateList = ContentAccessor.All<Categories>()
            //                            .Where(x => x.ParentCategory.Name == pCategoryName)
            //                            .Select(x => new
            //                            {
            //                                Category = x,
            //                                ContentList = x.ContentList.Take(pageSize).ToList()
            //                            })
            //                            .GroupBy(x => x.Category.Name);

            //return await cateList.ToDictionaryAsync(x => x.Key, c => c.Select(v => v.ContentList));
        }

        public async Task DeleteCategory(Guid id)
        {
            await CategoryAccessor.Delete<Categories>(x => x.Id == id);
        }



        //public List<string> GetCartoonTags()
        //{

        //}

        public async Task<IList<Categories>> GetNoCoverPage(int skipCount = 0, int pageSize = 20)
        {
            return await CategoryAccessor.ListOrderByDescendingAsync<Categories>(x => x.Status != ContentStatus.Close && x.ParentCategory.Name == "漫画" && (x.MediaResource == null || x.MediaResource.Count < 1), skipCount, pageSize, "MediaResource");
        }

        public async Task<IList<Categories>> GetTopCategory(string queryKey, int skipCount = 0, int pageSize = 20)
        {
            return await ApiMessage.WrapData(async () =>
            {
                IList<Categories> cateList;
                if (!string.IsNullOrEmpty(queryKey))
                {
                    cateList = await CategoryAccessor.ListOrderByDescendingAsync<Categories>(x => x.Status != ContentStatus.Close && x.ParentCategory.Name == "漫画" && x.Name.Contains(queryKey), skipCount, pageSize, "MediaResource");
                }
                else
                {
                    cateList = await CategoryAccessor.ListOrderByDescendingAsync<Categories>(x => x.Status != ContentStatus.Close && x.ParentCategory.Name == "漫画", skipCount, pageSize, "MediaResource");
                }

                await ResetCategoryCoverImg(cateList);

                return cateList;
            });
        }

        public async Task<List<Categories>> GetCategoryFormByP(string cateName)
        {
            return (await CategoryAccessor.ListAsync<Categories>(x => x.ParentCategory.Name == cateName)).ToList();
        }

        public async Task CreateCategoryWidth(Categories categoryEntry, string categoryName)
        {
            var parentCateg = await CategoryAccessor.OneAsync<Categories>(x => x.Name == categoryName);
            if (parentCateg == null)
            {
                parentCateg = new Categories
                {
                    Id = Guid.NewGuid(),
                    Name = categoryName,
                    CreateTime = DateTime.Now
                };
                await CategoryAccessor.Add(parentCateg);
            }
            var _cate = await CategoryAccessor.OneAsync<Categories>(x => x.Id == categoryEntry.Id);
            //var _cate = await CategoryAccessor.OneAsync<Categories>(x => x.Name == categoryEntry.Name && x.ParentCategory.Name == categoryName);
            if (_cate == null)
            {
                categoryEntry.Id = Guid.NewGuid();
                categoryEntry.CreateTime = DateTime.Now;
                categoryEntry.ParentCategory = parentCateg;
                await CategoryAccessor.Add(categoryEntry);
            }
            else
            {
                await CategoryAccessor.Update(categoryEntry);
            }
        }

        public async Task<Categories> GetCategoryForm(Guid Id)
        {
            return await CategoryAccessor.OneAsync<Categories>(x => x.Id == Id);
        }

        public async Task<Tuple<IList<Categories>, int>> GetTopSexHotWithTotalNum(int skipCount, int pageSize)
        {
            return await ApiMessage.WrapData(async () =>
            {
                IList<Categories> cateList;
                int total = 0;
                cateList = await CategoryAccessor.ListOrderByDescendingAsync<Categories>(x => x.Status != ContentStatus.Close
                && x.ParentCategory.Name == "漫画"
                && (x.Tags.Any(c => c.Name == "顶通")
                || x.Tags.Any(c => c.Name == "恋爱"))
                , skipCount, pageSize, "MediaResource");
                total = await CategoryAccessor.Count<Categories>(x => x.Status != ContentStatus.Close
                && x.ParentCategory.Name == "漫画"
                && (x.Tags.Any(c => c.Name == "顶通")
                || x.Tags.Any(c => c.Name == "恋爱")));

                await ResetCategoryCoverImg(cateList);
                return Tuple.Create(cateList, total);
            });
        }
        public async Task<Tuple<IList<Categories>, int>> GetTopCategoryWithTotalNum(string queryKey, int skipCount = 0, int pageSize = 20)
        {
            return await ApiMessage.WrapData(async () =>
            {
                IList<Categories> cateList;
                int total = 0;
                if (!string.IsNullOrEmpty(queryKey))
                {
                    cateList = await CategoryAccessor.ListOrderByDescendingAsync<Categories>(x => x.Status != ContentStatus.Close && x.ParentCategory.Name == "漫画" && x.Name.Contains(queryKey), skipCount, pageSize, "MediaResource");
                    total = await CategoryAccessor.Count<Categories>(x => x.Status != ContentStatus.Close && x.ParentCategory.Name == "漫画" && x.Name.Contains(queryKey));
                }
                else
                {
                    cateList = await CategoryAccessor.ListOrderByDescendingAsync<Categories>(x => x.Status != ContentStatus.Close && x.ParentCategory.Name == "漫画", skipCount, pageSize, "MediaResource");
                    total = await CategoryAccessor.Count<Categories>(x => x.Status != ContentStatus.Close && x.ParentCategory.Name == "漫画");
                }

                await ResetCategoryCoverImg(cateList);

                return Tuple.Create(cateList, total);
            });
        }

        public async Task<IList<Categories>> GetContentCategories()
        {
            return await CategoryAccessor.ListAsync<Categories>(x => x.Name != "漫画" && x.ParentCategory.Name != "漫画" && x.Status == ContentStatus.Normal);
        }

        public Task GetCategory(ListQuery listQuery)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ContentEntry>> GetHotpotContent()
        {
            throw new NotImplementedException();
        }

        public async Task<ContentEntry> GetLatestNews()
        {
            return await CategoryAccessor.All<ContentEntry>().Where(x => x.Category.Name == "资讯").OrderByDescending(x => x.CreateTime).Take(1).FirstOrDefaultAsync();
        }

        public async Task<IList<ContentEntry>> GetRecommandContent()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Categories>> GetTopList()
        {
            return await CategoryAccessor.ListOrderByDescendingAsync<Categories>(x => x.Status != ContentStatus.Close && x.ParentCategory == null, 0, 6, "MediaResource");
        }


        public async Task<string> GetPrevContentId(ContentEntry cEntry)
        {
            if (cEntry.Order == 0)
            {
                return null;
            }
            else
                return await ApiMessage.WrapData(async () =>
                {
                    var prevEntry = await ContentAccessor.OneAsync<ContentEntry>(x => x.Order == cEntry.Order - 1 && x.Category == cEntry.Category);
                    return prevEntry.Id.ToString();
                });
        }

        public async Task<string> GetNextContentId(ContentEntry cEntry)
        {
            var contentCountOfCategory = cEntry.Category.ContentList.Count;

            if (cEntry.Order + 1 == contentCountOfCategory)
            {
                return null;
            }
            else
                return await ApiMessage.WrapData(async () =>
                {
                    var nextEntry = await ContentAccessor.OneAsync<ContentEntry>(x => x.Order == cEntry.Order + 1 && x.Category == cEntry.Category);
                    return nextEntry.Id.ToString();

                });
        }

        public async Task<Tuple<IList<Categories>, int>> GetCategoryByTagName(string tagName, int pageSkip, int pageSize)
        {
            var categoryList = await ContentAccessor.ListOrderByDescendingAsync<Categories>(x => x.Status != ContentStatus.Close &&
                x.ContentList != null
                && x.ContentList.Count > 0
                && x.Tags.Any(c => c.Name == tagName)
            , pageSkip, pageSize, "MediaResource");
            await ResetCategoryCoverImg(categoryList);
            var count = await ContentAccessor.Count<Categories>(x => x.Status != ContentStatus.Close &&
                x.ContentList != null
                && x.ContentList.Count > 0
                && x.Tags.Any(c => c.Name == tagName));
            return Tuple.Create(categoryList, count);
        }

        public async Task CreateMultiContent(Guid pid, string contentName, int count)
        {
            var category = await ContentAccessor.OneAsync<Categories>(x => x.Status != ContentStatus.Close && x.Id == pid);
            var contentList = new List<ContentEntry>();
            for (int i = 1; i <= count; i++)
            {
                contentList.Add(new ContentEntry
                {
                    Category = category,
                    Title = $"{contentName} 第{i}话",
                    Order = i,
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid()
                });
            }
            await ContentAccessor.All<ContentEntry>().AddRangeAsync(contentList);
            await ContentAccessor.SaveAsync();
        }

        public async Task SetCategoryStatus(Guid categoryId, ContentStatus status)
        {
            var category = await CategoryAccessor.OneAsync<Categories>(x => x.Id == categoryId);
            category.Status = status;
            await CategoryAccessor.Update(category);
        }

        public async Task AppenContentMediaResource(Guid contentId, List<FileEntry> fileEntryList)
        {
            var content = await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == contentId, "MediaResource");

            if (content.MediaResource == null)
            {
                content.MediaResource = new List<FileEntry> { };
            }
            content.MediaResource.AddRange(fileEntryList);

            await ContentAccessor.Update(content);
        }

        /// <summary>
        /// 查询Category 带 详情内容, 评论内容
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CategoryOutput> GetCategoryWithDetailById(Guid Id)
        {
            return await ApiMessage.WrapData(async () =>
            {
                var categoryInfo = await CategoryAccessor.OneAsync<Categories>(x => x.Status != ContentStatus.Close && x.Id == Id, "ChildrenCategories", "ChildrenCategories.ContentList", "ContentList", "ContentList.MediaResource");
                return new CategoryOutput
                {
                    Category = categoryInfo,
                    CategoryComments = (await CategoryAccessor.ListAsync<CategoryComment>(c => c.CategoryId == categoryInfo.Id, 0, 5)).ToList()
                };
            });
        }

        public async Task<IList<UserRecentLogOutputViewModel>> GetContentUserReadLog(Guid userId, int skipCount, int pageSize)
        {
            return await ApiMessage.WrapData(async () =>
            {
                var cateList = await CategoryAccessor.GroupCategories(x => x.UserId == userId && x.ContentEntry != null && x.ContentEntry.MediaResource.Count > 0, skipCount, pageSize);

                await ResetCategoryCoverImg(cateList);

                return cateList;
            });
        }

        /// <summary>
        /// 查询Category 不带详情内容
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CategoryOutput> GetCategoryById(Guid Id)
        {
            var categoryInfo = await CategoryAccessor.OneAsync<Categories>(x => x.Status != ContentStatus.Close && x.Id == Id, "ChildrenCategories", "ChildrenCategories.ContentList", "ContentList", "MediaResource", "Tags");
            if (categoryInfo != null)
            {
                return new CategoryOutput
                {
                    Category = categoryInfo,
                    CategoryComments = (await CategoryAccessor.ListAsync<CategoryComment>(c => c.CategoryId == categoryInfo.Id, 0, 5)).ToList()
                };
            }
            else
            {
                return new CategoryOutput { };
            }
        }

        public async Task<CategoryOutput> GetCategoryByIdWithUserReadLog(Guid cid, Guid userId)
        {
            var categoryInfo = await CategoryAccessor.OneAsync<Categories>(x => x.Status != ContentStatus.Close && x.Id == cid, "ContentList", "MediaResource", "Tags");
            var userReadLog = await CategoryAccessor.ListAsync<ContentUserReadLog>(x => x.UserId == userId && x.CategoryId == cid);
            return new CategoryOutput
            {
                Category = categoryInfo,
                UserReadLog = userReadLog,
                CategoryComments = (await CategoryAccessor.ListAsync<CategoryComment>(c => c.CategoryId == categoryInfo.Id, 0, 5)).ToList()
            };
        }

        public async Task<ApiMessage> GetCategoryByParentId(Guid Id, int skipCount = 0, int pageSize = 20)
        {
            return await ApiMessage.Wrap(async () =>
            {
                return await CategoryAccessor.ListAsync<Categories>(x => x.Status != ContentStatus.Close && x.ParentCategory.Id == Id, skipCount, pageSize);
            });
        }

        private async Task<IList<Categories>> GetCategoryCore(IQueryable<Categories> categoryQurable, params string[] IncludesProperty)
        {
            if (IncludesProperty != null && IncludesProperty.Length > 0)
            {
                foreach (var item in IncludesProperty)
                {
                    categoryQurable = categoryQurable.Include(item);
                }
            }
            var categoryList = await categoryQurable.ToListAsync();
            await ResetCategoryCoverImg(categoryList);
            return categoryList;
        }

        public async Task<ContentEntry> GetNewsDetail(Guid id)
        {
            return await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == id);
        }

        public async Task<IList<ContentEntry>> GetNews(int skipCount, int pageSize)
        {
            //var targetTags = await ContentAccessor.ListOrderByDescendingAsync<Tags>(x => x.Name == "资讯");
            return await ContentAccessor.ListOrderByDescendingAsync<ContentEntry>(x => x.Category.Name == "资讯", skipCount, pageSize, "MediaResource");
        }

        public async Task<IList<ContentEntry>> GetContentListByCategoryId(Guid Id, int skipCount = 0, int pageSize = 20)
        {
            return await ContentAccessor.ListAsync<ContentEntry>(x => x.Category.Id == Id, skipCount, pageSize);
        }

        private async Task ResetCategoryCoverImg(IList<Categories> categoryList)
        {
            foreach (var item in categoryList)
            {
                await ResetCategoryCoverImg(item);
            }
        }

        private async Task ResetCategoryCoverImg(IList<UserRecentLogOutputViewModel> recentLogVM)
        {
            foreach (var item in recentLogVM)
            {
                await ResetCategoryCoverImg(item.Categories);
            }
        }

        private async Task ResetCategoryCoverImg(Categories category)
        {
            if (category.MediaResource != null && category.MediaResource.Count > 0)
            {
                return;
            }

            var firstContentImg = await CategoryAccessor.All<ContentEntry>().Where(x => x.Category.Id == category.Id).Include("MediaResource").OrderBy(x => x.CreateTime).FirstOrDefaultAsync();

            if (firstContentImg == null)
            {
                return;
            }

            category.MediaResource = new List<FileEntry> {
               firstContentImg .MediaResource?.OrderBy(x=>x.Order).FirstOrDefault()
            };
        }

        /// <summary>
        /// 阅读时写入用户阅读记录
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userId"></param>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ContentEntry> GetContentByIdSetLog(Guid Id, string userId, int skipCount = 0, int pageSize = 20)
        {
            return await ApiMessage.WrapData(async () =>
            {
                var contentEntry = await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == Id, "MediaResource", "Category", "Category.ContentList");

                if (!string.IsNullOrEmpty(userId))
                {
                    await ContentAccessor.Add(new ContentUserReadLog
                    {
                        CreateTime = DateTime.Now,
                        Id = Guid.NewGuid(),
                        Categories = contentEntry.Category,
                        UserId = Guid.Parse(userId),
                        ContentEntry = contentEntry
                    });
                }
                return contentEntry;
            });
        }

        public async Task<ContentEntry> GetContentById(Guid Id, int skipCount = 0, int pageSize = 20)
        {
            return await ApiMessage.WrapData(async () =>
            {
                return await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == Id, "MediaResource", "Category", "Category.ContentList");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="skipCount"></param>
        /// <param name="pageSize"></param>
        /// <returns>文章内容， 评论总数</returns>
        public async Task<Tuple<ContentEntry, int>> GetContentWityCommentById(Guid Id, int skipCount = 0, int pageSize = 20)
        {
            return await ApiMessage.WrapData(async () =>
            {
                var contentEnry = await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == Id, "MediaResource");
                var totalNum = await ContentAccessor.Count<Comment>(x => x.ContentEntryId == Id);
                return Tuple.Create(contentEnry, totalNum);
            });
        }

        public async Task<ApiMessage> CreateContent(ContentEntry content)
        {
            return await ApiMessage.Wrap(async () =>
            {
                content.CreateTime = DateTime.Now;
                await ContentAccessor.AddOrUpdate(content);
            });
        }



        public async Task<ApiMessage> CreateContentWithCategory(ContentEntry content, string categoryName)
        {
            return await ApiMessage.Wrap(async () =>
            {
                var categoryEntity = await CategoryAccessor.OneAsync<Categories>(x => x.Name == categoryName);
                if (categoryEntity == null)
                {
                    categoryEntity = new Categories
                    {
                        Name = categoryName,
                        CreateTime = DateTime.Now,
                        Id = Guid.NewGuid()
                    };
                    await CategoryAccessor.Add(categoryEntity);
                }

                var contentEntry = await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == content.Id);
                if (contentEntry == null)
                {
                    contentEntry = new ContentEntry
                    {
                        Id = Guid.NewGuid(),
                        Category = categoryEntity,
                        Content = content.Content,
                        CreateTime = DateTime.Now,
                        IsFaq = content.IsFaq,
                        IsTop = content.IsTop,
                        Title = content.Title
                    };
                    await ContentAccessor.Add(contentEntry);
                }
                else
                {
                    contentEntry.Category = categoryEntity;
                    contentEntry.Content = content.Content;
                    contentEntry.IsFaq = content.IsFaq;
                    contentEntry.IsTop = content.IsTop;
                    contentEntry.Title = content.Title;
                    await ContentAccessor.Update(contentEntry);
                }
            });
        }

        public async Task<ContentEntry> CreateContentWithCategoryAndTag(ContentEntry content, string categoryName, string tag)
        {
            var categoryEntity = await CategoryAccessor.OneAsync<Categories>(x => x.Name == categoryName);
            if (categoryEntity == null)
            {
                categoryEntity = new Categories
                {
                    Name = categoryName,
                    CreateTime = DateTime.Now,
                    Id = Guid.NewGuid()
                };
                await CategoryAccessor.Add(categoryEntity);
            }

            var contentEntry = await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == content.Id);



            if (contentEntry == null)
            {
                contentEntry = new ContentEntry
                {
                    Id = Guid.NewGuid(),
                    Category = categoryEntity,
                    Content = content.Content,
                    CreateTime = DateTime.Now,
                    IsFaq = content.IsFaq,
                    IsTop = content.IsTop,
                    Title = content.Title,
                    Tags = new List<Tags> {
                             new Tags {
                                Name = tag,
                                Id = Guid.NewGuid(),
                                CreateTime=DateTime.Now
                             }
                        },
                    ContentEntryInfo = new ContentEntryInfo
                    {
                        Id = content.ContentEntryInfo.Id,
                        Author = content.ContentEntryInfo.Author,
                        Source = content.ContentEntryInfo.Source,
                        Type = content.ContentEntryInfo.Type,
                        UserAccountId = content.ContentEntryInfo.UserAccountId
                    }
                };
                await ContentAccessor.Add(contentEntry);
                //if (contentEntry.MediaResource == null)
                //{
                //    contentEntry.MediaResource = new List<FileEntry>();
                //}
                if (content.MediaResource != null)
                {
                    foreach (var item in content.MediaResource)
                    {
                        item.ContentEntryId = contentEntry.Id;
                    }

                    await ContentAccessor.UpdateRange(content.MediaResource);
                }

                ////var mediaResource = ContentAccessor.Entry(content.MediaResource);
                ////contentEntry.MediaResource = mediaResource;
                //await ContentAccessor.Update(contentEntry);
            }
            else
            {
                contentEntry.Category = categoryEntity;
                contentEntry.Content = content.Content;
                contentEntry.IsFaq = content.IsFaq;
                contentEntry.IsTop = content.IsTop;
                contentEntry.Title = content.Title;
                if (content.MediaResource != null)
                {
                    foreach (var item in content.MediaResource)
                    {
                        item.ContentEntryId = contentEntry.Id;
                    }

                    await ContentAccessor.UpdateRange(content.MediaResource);
                }
                //contentEntry.MediaResource = content.MediaResource;
                await ContentAccessor.Update(contentEntry);
            }

            return contentEntry;
        }

        //public Task CreateContentWithTag(ContentEntry contentEntry, string v1, string v2)
        //{
        //    throw new NotImplementedException();
        //}
        public async Task<ApiMessage> CreateContentWithTag(ContentEntry content, string tagName)
        {
            return await ApiMessage.Wrap(async () =>
            {
                //var categoryEntity = await CategoryAccessor.OneAsync<Categories>(x => x.Name == tagName);
                //if (categoryEntity == null)
                //{
                //    categoryEntity = new Categories
                //    {
                //        Name = tagName,
                //        CreateTime = DateTime.Now,
                //        Id = Guid.NewGuid()
                //    };
                //    await CategoryAccessor.Add(categoryEntity);
                //}

                var contentEntry = await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == content.Id);
                if (contentEntry == null)
                {
                    contentEntry = new ContentEntry
                    {
                        Id = Guid.NewGuid(),
                        CategoryId = content.CategoryId,
                        Content = content.Content,
                        CreateTime = DateTime.Now,
                        IsFaq = content.IsFaq,
                        IsTop = content.IsTop,
                        Title = content.Title,
                        Tags = new List<Tags> {
                              new Tags{
                                   Name = tagName
                              }
                         }
                    };
                    await ContentAccessor.Add(contentEntry);
                }
                else
                {
                    contentEntry.CategoryId = content.CategoryId;
                    contentEntry.Content = content.Content;
                    contentEntry.IsFaq = content.IsFaq;
                    contentEntry.IsTop = content.IsTop;
                    contentEntry.Title = content.Title;
                    await ContentAccessor.Update(contentEntry);
                }
            });
        }


        public async Task<ApiMessage> CreateCategory(Categories category)
        {
            return await ApiMessage.Wrap(async () =>
            {
                category.Id = Guid.NewGuid();
                category.CreateTime = DateTime.Now;
                if (category.Tags != null)
                {
                    for (int i = 0; i < category.Tags.Count; i++)
                    {
                        var item = category.Tags[i];

                        var dbTag = await CategoryAccessor.OneAsync<Tags>(x => x.Name == item.Name);
                        if (dbTag != null)
                        {
                            category.Tags[i] = dbTag;
                        }
                        else
                        {
                            item.Id = Guid.NewGuid();
                            item.CreateTime = DateTime.Now;
                            item.CategoryId = category.Id;
                        }
                    }
                }

                await CategoryAccessor.Add(category);
            });
        }

        public async Task CreateKnowledgeCategory(Categories categories)
        {
            var parent = await CategoryAccessor.OneAsync<Categories>(x => x.Name == "论坛");
            if (parent == null)
            {
                var cateModel = new Categories
                {
                    Name = "论坛",
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now
                };
                await CategoryAccessor.Add(cateModel);
                parent = await CategoryAccessor.OneAsync<Categories>(x => x.Name == "论坛");
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

        public async Task<ApiMessage> CreateFile(FileEntry fileEntry)
        {
            return await ApiMessage.Wrap(async () =>
            {
                await FileAccessor.Add(fileEntry);
            });
        }

        public async Task DeleteContent(Guid Id)
        {
            var contentEntry = await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == Id, "Tags", "ContentEntryInfo", "Comments");
            await ContentAccessor.Delete<ContentEntry>(contentEntry);
        }


        #region 旧的评论逻辑

        public async Task CreateCategoryComment(CategoryComment comment)
        {
            await ContentAccessor.Add(comment);
            await ContentAccessor.SaveAsync();
        }

        public async Task<IList<CategoryComment>> GetCategoryComment(Guid cateId, int skipCount)
        {
            var categoryList = await ContentAccessor.ListAsync<CategoryComment>(x => x.CategoryId == cateId, skipCount);
            return categoryList;
        }

        public async Task CreateComment(Comment commentModel)
        {
            //向文章作者发送提示
            var _content = await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == commentModel.ContentEntryId, "ContentEntryInfo");
            if (_content.ContentEntryInfo != null)
            {
                await sandBoxBusiness.SendAlertTo(new SandBoxMessage
                {
                    Content = $"文章 : <a href='/Forum/PostList?Id={_content.Id}'>{_content.Title}</a> 有新的回复",
                    MessageType = SandBoxMessageType.PostEntryComment,
                    CreateTime = DateTime.Now,
                    FromUserAccountId = commentModel.UserAccountId.Value,
                    ToUserAccountId = _content.ContentEntryInfo.UserAccountId
                });
            }

            //向引用回复作者发送提示
            if (commentModel.ParentComment != null)
            {
                await sandBoxBusiness.SendAlertTo(new SandBoxMessage
                {
                    Content = $"文章 : <a href='/Forum/PostList?Id={_content.Id}'>{_content.Title}</a> 有新的回复",
                    MessageType = SandBoxMessageType.PostEntryCommentSubComment,
                    CreateTime = DateTime.Now,
                    FromUserAccountId = commentModel.UserAccountId.Value,
                    ToUserAccountId = commentModel.ParentComment.UserAccountId.Value
                });
            }
            await ContentAccessor.Add<Comment>(commentModel);
        }

        public async Task<IList<Comment>> GetComments(Guid Id, int skipCount, int pageSize)
        {
            return await CategoryAccessor.ListAsync<Comment>(x => x.ContentEntry.Id == Id, skipCount, pageSize, "UserAccount", "ParentComment", "ParentComment.UserAccount");
        }

        #endregion

        #region Comment扩展PostEntry后, 新增逻辑

        public async Task CreateCategoryCommentExtPostEntry(CategoryComment comment)
        {
            var categoryModel = await ContentAccessor.OneAsync<Categories>(x => x.Status != ContentStatus.Close && x.Id == comment.CategoryId);
            //设置话题为漫画名
            var topic = new PostEntryTopic()
            {
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Text = categoryModel.Name,
                PosterId = comment.UserAccountId
            };
            var topicExt = new ContentExtPostEntryTopic()
            {
                Id = Guid.NewGuid(),
                TopicText = categoryModel.Name,
                LinkId = categoryModel.Id,
                LinkType = "category"
            };
            //评论
            var postentryModel = new PostEntry()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                PostEntryTopic = categoryModel.Name,
                UserId = comment.UserAccountId,
                TimeStamp = TimeStamp.Get(),
                TextContent = comment.Content,
            };

            //目前是一条漫画评论对应一个话题, 为防止以后出现多话题, 保留此表
            var categoryPostentryMapping = new CategoryPostEntryMapping()
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryModel.Id,
                PostEntryId = postentryModel.Id,
                CreateTime = DateTime.Now
            };

            await ContentAccessor.Add(topic);
            await ContentAccessor.Add(topicExt);
            await ContentAccessor.Add(postentryModel);
            await ContentAccessor.Add(categoryPostentryMapping);

            var sandBoxMsg = new SandBoxMessage()
            {
                Content = $"您在漫画{topic}的回复，收到了新的评论.",
                CreateTime = DateTime.Now,
                FromUserAccountId = comment.UserAccountId,
                ToUserAccountId = postentryModel.UserId,
                Id = Guid.NewGuid(),
                IsRead = false,
                MessageType = SandBoxMessageType.PostEntryComment,
                TimeStamp = TimeStamp.Get(),
                RecieveToken = ""
            };
            await sandBoxBusiness.SendAlertTo(sandBoxMsg);
        }

        public async Task<List<PostEntryOutputViewModel>> GetCategoryCommentExtPostEntry(Guid cateId, int skipCount, int pageSize)
        {
            //通过cate 名找到话题
            var postEntryMapCategory = await ContentAccessor.ListOrderByDescendingAsync<CategoryPostEntryMapping>(x => x.CategoryId == cateId);
            var postEntryList = await ContentAccessor.ListOrderByDescendingAsync<PostEntry>(x => postEntryMapCategory.Any(c => c.PostEntryId == x.Id), skipCount, pageSize, "UserAccountEntry", "UserAccountEntry.Profile");
            //var postentrySubComments = await ContentAccessor.All<PostEntry>().Where(x => postEntryList.Select(c => c.Id).Any(c => c == x.Id))
            //    .Include("PostEntryCommentList.UserAccount")
            //    .Select(x => new
            //    {
            //        PostId = x.Id,
            //        SubComments = x.PostEntryCommentList.Select(c => new
            //        {
            //             c
            //             c.CreateTime
            //        }).Take(2).OrderByDescending(c => c.CreateTime).ToList(),
            //        SubCommentsNum = x.PostEntryCommentList.Count()
            //    }).ToListAsync();
            var postentrySubComments = ContentAccessor.All<PostEntryComments>()
                .Where(x => postEntryList.Select(c => c.Id).Any(c => c == x.PostEntryId))
                .Include("UserAccount")
                .Include("ParentComment")
                .Include("ParentComment.UserAccount")
                .OrderByDescending(x => x.CreateTime)
                .Take(2)
                .GroupBy(x => x.PostEntryId)
                .ToList();
            //var postentrySubCommentNum = new Dictionary<Guid, int>();
            //postEntryList.ToList().ForEach(item =>
            //{

            //});
            var postentrtSubCommentsNum = await ContentAccessor.All<PostEntry>()
                .Where(x => postEntryList.Select(c => c.Id).Any(c => c == x.Id))
                .Select(x => new
                {
                    x.Id,
                    Count = x.PostEntryCommentList.Count()
                })
                .ToListAsync();



            var postEntryOutput = new List<PostEntryOutputViewModel>();
            postEntryList.ToList().ForEach(item =>
            {
                var subComments = postentrySubComments.FirstOrDefault(x => x.Key == item.Id)?.ToList();
                var subCommentNum = postentrtSubCommentsNum.FirstOrDefault(x => x.Id == item.Id)?.Count ?? 0;
                postEntryOutput.Add(new PostEntryOutputViewModel
                {
                    Id = item.Id,
                    LikeSum = item.LikeSum,
                    Order = item.Order,
                    PostEntryCommentList = subComments,
                    PostEntryCommentListNum = subCommentNum,
                    PostEntryTopic = item.PostEntryTopic,
                    TextContent = item.TextContent,
                    TimeStamp = item.TimeStamp,
                    UserAccountEntry = item.UserAccountEntry
                });
            });
            return postEntryOutput;
        }

        public async Task CreateContentCommentExtPostEntry(Comment comment)
        {
            var contentModel = await ContentAccessor.OneAsync<ContentEntry>(x => x.Id == comment.ContentEntry.Id, "Category");
            var topicTxt = contentModel.Category.Name + "|" + contentModel.Title;
            //设置话题为漫画名
            var topic = new PostEntryTopic()
            {
                CreateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                Text = topicTxt,
                PosterId = comment.UserAccount.Id
            };
            var topicExt = new ContentExtPostEntryTopic()
            {
                Id = Guid.NewGuid(),
                TopicText = topicTxt,
                LinkId = contentModel.Id,
                LinkType = "category"
            };
            //评论
            var postentryModel = new PostEntry()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                PostEntryTopic = topicTxt,
                UserId = comment.UserAccount.Id,
                TimeStamp = TimeStamp.Get(),
                TextContent = comment.Content
            };

            //目前是一条漫画评论对应一个话题, 为防止以后出现多话题, 保留此表
            var contentPostentryMapping = new ContentPostEntryMapping()
            {
                Id = Guid.NewGuid(),
                ContentId = contentModel.Id,
                PostEntryId = postentryModel.Id,
                CreateTime = DateTime.Now
            };

            await ContentAccessor.Add(topic);
            await ContentAccessor.Add(topicExt);
            await ContentAccessor.Add(postentryModel);
            await ContentAccessor.Add(contentPostentryMapping);
        }

        public async Task<IList<PostEntry>> GetContentCommentExtPostEntry(Guid conId, int skipCount, int pageSize)
        {
            //通过cate 名找到话题
            var postEntryMapCategory = await ContentAccessor.ListOrderByDescendingAsync<ContentPostEntryMapping>(x => x.Id == conId);
            var postEntryList = await ContentAccessor.ListOrderByDescendingAsync<PostEntry>(x => postEntryMapCategory.Any(c => c.PostEntryId == x.Id), skipCount, pageSize, "UserAccountEntry");
            return postEntryList;
        }

        #endregion
    }
}
