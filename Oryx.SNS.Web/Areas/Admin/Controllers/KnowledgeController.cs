using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oryx.Content.Business;
using Oryx.Content.Model;
using Oryx.ViewModel;
using Oryx.ViewModel.CRUD;

namespace Oryx.SNS.Web.Admin.Controllers
{
    [Area("Admin")]
    public class KnowledgeController : Controller
    {
        private readonly KnowledgeBusiness knowledgeBusiness;
        public KnowledgeController(KnowledgeBusiness _knowledgeBusiness)
        {
            knowledgeBusiness = _knowledgeBusiness;
        }

        public async Task<IActionResult> CreateKLCategory(Categories categories)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
             {
                 await knowledgeBusiness.CreateKnowledgeCategory(categories);
             });

            return Json(apiMsg);
        }

        public async Task<IActionResult> GetKLCategoryList(ListQuery listQuery)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await knowledgeBusiness.GetKnowledgeCategoryList(listQuery);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> GetKLCategoryById(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await knowledgeBusiness.GetKnowledgeCategory(Id);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> UpdateKLCategory(Categories categories)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await knowledgeBusiness.EditKnowledgeCategory(categories);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> DeleteKLCategory(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await knowledgeBusiness.DeleteKnowledgeCategory(Id);
            });

            return Json(apiMsg);
        }


        public async Task<IActionResult> CreateKLArticle(ContentEntry contentEntry)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await knowledgeBusiness.CreateKnowledgeArticle(contentEntry);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> GetKLArticleList(ListQuery listQuery)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await knowledgeBusiness.GetKnowledgeArticleList(listQuery);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> GetKLArticleById(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await knowledgeBusiness.GetKnowledgeArticle(Id);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> UpdateKLArticle(ContentEntry contentEntry)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await knowledgeBusiness.UpdateKnowledgeArticle(contentEntry);
            });

            return Json(apiMsg);
        }

        public async Task<IActionResult> DeleteKLArticle(Guid Id)
        {
            var apiMsg = await ApiMessage.Wrap(async () =>
            {
                await knowledgeBusiness.DeleteKnowledgeArticle(Id);
            });

            return Json(apiMsg);
        }
    }
}