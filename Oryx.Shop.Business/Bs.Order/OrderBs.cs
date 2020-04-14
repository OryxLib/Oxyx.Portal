using Oryx.Shop.DataAccessor.DA.Order;
using Oryx.Shop.Model.Shop.Goods;
using Oryx.Shop.Model.Shop.Orders;
using Oryx.Shop.Model.Shop.PayLog;
using Oryx.UserAccount.Model;
using Oryx.ViewModel;
using Oryx.ViewModel.Shop.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Oryx.Shop.Business.Bs.Order
{
    public class OrderBs
    {
        public DataOrderAccessor DataOrderAccessor { get; set; }

        public OrderBs(DataOrderAccessor _DataOrderAccessor)
        {
            DataOrderAccessor = _DataOrderAccessor;
        }

        public async Task<ApiMessage> OrderList(Guid userCode, int skipCount, int pageSize = 10)
        {
            return await ApiMessage.Wrap(async () => await DataOrderAccessor.ListAsync<OrderEntity>(x => x.UserCoder == userCode, skipCount, pageSize, "OrderGoods", "OrderGoods.Good", "UserAccountEntry"));
        }

        public async Task<ApiMessage> OrderList(Guid userCode, OrderStatus status, int skipCount, int pageSize = 10)
        {
            return await ApiMessage.Wrap(async () => await DataOrderAccessor.ListAsync<OrderEntity>(x => x.UserCoder == userCode && x.Status == status, skipCount, pageSize, "OrderGoods", "OrderGoods.Good", "UserAccountEntry"));
        }


        public async Task<OrderEntity> GetDetailData(Guid orderId)
        {
            return await ApiMessage.WrapData(async () => await GetDetailData(x => x.Id == orderId, "OrderGoods", "OrderGoods.Good", "UserAccountEntry"));
        }

        public async Task<OrderEntity> GetDetailData(Expression<Func<OrderEntity, bool>> expression)
        {
            return await ApiMessage.WrapData(async () => await GetDetailData(expression, "OrderGoods", "OrderGoods.Good", "UserAccountEntry"));
        }

        public async Task<OrderEntity> GetDetailData(Expression<Func<OrderEntity, bool>> expression, params string[] IncludeProperty)
        {
            return await ApiMessage.WrapData(async () => await DataOrderAccessor.OneAsync(expression, IncludeProperty));
        }

        public async Task<ApiMessage> GetDetail(Guid Id)
        {
            return await ApiMessage.Wrap(async () => await DataOrderAccessor.OneAsync<OrderEntity>(x => x.Id == Id, "OrderGoods"));
        }

        public async Task<bool> Update(OrderEntity orderEntity)
        {
            return await ApiMessage.WrapData(async () => await DataOrderAccessor.Update(orderEntity));
        }

        public async Task<bool> WriteLog(OrderEntity orderEntity)
        {
            try
            {

                await DataOrderAccessor.Add(new PayLogEntity
                {
                    UserAccountEntry = orderEntity.UserAccountEntry,
                    Order = orderEntity,
                    ProcessResult = "true",
                    TradeNo = orderEntity.OutTradeNo,
                    TotalFee = (int)(orderEntity.OrderGoods.Sum(x => x.Number * x.Good.Price) * 100)
                });
            }
            catch (Exception exc)
            {
                return false;
            }
            return true;
        }

        public async Task<ApiMessage> CreateOrder(CreateOrderModel createOrderModel)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var orderId = Guid.NewGuid();
                var _userAccountEntry = await DataOrderAccessor.OneAsync<UserAccountEntry>(x => x.Id == createOrderModel.UserCode);
                var orderGoods = new List<OrderGoodMapping>();

                var orderEntity = new OrderEntity()
                {
                    UserAccountEntry = _userAccountEntry,
                    Id = orderId,
                    IsDelete = true,
                    Name = createOrderModel.OrderName,
                    Status = OrderStatus.Created,
                    UserCoder = createOrderModel.UserCode,

                };

                foreach (var item in createOrderModel.GoodsList)
                {
                    orderGoods.Add(new OrderGoodMapping
                    {
                        Good = await DataOrderAccessor.OneAsync<GoodEntity>(x => x.Id == item.GoodsId),
                        Number = item.Num,
                        Order = orderEntity
                    });
                }
                orderEntity.OrderGoods = orderGoods;
                orderEntity.OrderExpressInfo = new OrderExpressInfo
                {
                    CustomerAddress = createOrderModel.CustomerAddress,
                    CustomerName = createOrderModel.CustomerName,
                    CustomerPhone = createOrderModel.CustomerPhone
                };

                await DataOrderAccessor.Add(orderEntity);
                resultMsg.Data = new { orderId };
            }
            catch (Exception exc)
            {
                resultMsg.Success = false;
                resultMsg.Message = exc.Message;
            }
            return resultMsg;
        }

        public async Task<ApiMessage> SendExpress(Guid Id)
        {
            var resultMessage = new ApiMessage();
            try
            {
                var orderEntity = await DataOrderAccessor.OneAsync<OrderEntity>(x => x.Id == Id);
                orderEntity.Status = OrderStatus.Expressing;
                await DataOrderAccessor.Update(orderEntity);
            }
            catch (Exception exc)
            {
                resultMessage.Success = false;
                resultMessage.Message = exc.Message;
            }
            return resultMessage;
        }
    }
}
