using Oryx.Shop.DataAccessor.DA.Goods;
using Oryx.Shop.Model.Shop.Goods;
using Oryx.ViewModel;
using Oryx.ViewModel.Shop.Goods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Shop.Business.Bs.Goods
{
    public class GoodsBs
    {
        public DataGoodsAccessor DataGoodsAccessor { get; set; }

        public GoodsBs(DataGoodsAccessor _DataGoodsAccessor)
        {
            DataGoodsAccessor = _DataGoodsAccessor;
        }

        public async Task<ApiMessage> AddGood(GoodEntity goodEntity)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var category = await DataGoodsAccessor.OneAsync<GoodCategory>(x => x.Id == goodEntity.Category.Id);
                goodEntity.Category = category;
                await DataGoodsAccessor.Add(goodEntity);
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> AddGoodCategory(GoodCategory category)
        {
            var resultMsg = new ApiMessage();
            try
            {
                await DataGoodsAccessor.Add(category);
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> GetInfo(Guid id)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var goodInfo = await DataGoodsAccessor.OneAsync<GoodEntity>(x => x.Id == id);
                resultMsg.Data = goodInfo;
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> UpdateCategory(GoodCategory category)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var cate = await DataGoodsAccessor.Update<GoodCategory>(category);
                resultMsg.Data = cate;
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> UpdateGood(GoodEntity goodEntity)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var goodInfo = await DataGoodsAccessor.Update<GoodEntity>(goodEntity);
                resultMsg.Data = goodInfo;
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> DeleteGood(Guid id)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var goodInfo = await DataGoodsAccessor.OneAsync<GoodEntity>(x => x.Id == id);
                await DataGoodsAccessor.Delete(goodInfo);
                resultMsg.Data = goodInfo;
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> ListGoodCategory()
        {
            var resultMsg = new ApiMessage();
            try
            {
                var catList = await DataGoodsAccessor.ListAsync<GoodCategory>();
                resultMsg.Data = catList;
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> ListGoods(int categoryId, int skipCount, int pageSize = 10)
        {
            var resultMsg = new ApiMessage();
            IEnumerable<GoodEntity> catList;
            try
            {
                if (categoryId == 0)
                {
                    catList = await DataGoodsAccessor.ListAsync<GoodEntity>(skipCount, pageSize);
                }
                else
                {
                    catList = await DataGoodsAccessor.ListAsync<GoodEntity>(x => x.Category.Id == categoryId, skipCount, pageSize);
                }
                resultMsg.Data = catList;
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> ListGoods(GoodsQueryModel query)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var catList = await DataGoodsAccessor.ListAsync<GoodEntity>(query.Size * (query.Index - 1), query.Size);
                resultMsg.Data = catList;
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }
    }
}
