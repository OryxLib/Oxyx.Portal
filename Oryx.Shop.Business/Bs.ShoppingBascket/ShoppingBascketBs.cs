using Oryx.Shop.DataAccessor.DA.ShoppingBascket;
using Oryx.Shop.Model.Shop.Goods;
using Oryx.Shop.Model.Shop.ShoppingBascket;
using Oryx.UserAccount.Model;
using Oryx.ViewModel;
using System;
using System.Threading.Tasks;

namespace Oryx.Shop.Business.Bs.ShoppingBascket
{
    public class ShoppingBascketBs
    {
        public DataShoppingBascketAccessor DataShoppingBascketAccessor { get; set; }

        public ShoppingBascketBs(DataShoppingBascketAccessor _DataShoppingBascketAccessor)
        {
            DataShoppingBascketAccessor = _DataShoppingBascketAccessor;
        }

        public async Task<ApiMessage> AddBascket(Guid Id, Guid UserCode)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var customerEntity = await DataShoppingBascketAccessor.OneAsync<UserAccountEntry>(x => x.Id == UserCode);
                var GoodsEntity = await DataShoppingBascketAccessor.OneAsync<GoodEntity>(x => x.Id == Id);
                await DataShoppingBascketAccessor.Add(new ShoppingBasketEntity
                {
                    UserAccountEntry = customerEntity,
                    Good = GoodsEntity
                });
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
                return resultMsg;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> DeleteBascketItem(Guid Id)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var shoppingBascketEntity = await DataShoppingBascketAccessor.OneAsync<ShoppingBasketEntity>(x => x.Id == Id);
                var result = await DataShoppingBascketAccessor.Delete(shoppingBascketEntity);
                if (result)
                {
                    resultMsg.Success = false;
                    resultMsg.Message = "操作失败";
                }
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
