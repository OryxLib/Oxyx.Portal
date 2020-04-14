using Oryx.Shop.DataAccessor.DA.PayLog;
using Oryx.Shop.Model.Shop.Orders;
using Oryx.Shop.Model.Shop.PayLog;
using Oryx.ViewModel;
using Oryx.Wx.Service.WxPay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Shop.Business.Bs.PayLog
{
    public class PayLogBs
    {
        public DataPaylogAccessor DataPayLogAccessor { get; set; }

        public WxPayTool WxPayTool { get; set; }

        public PayLogBs(DataPaylogAccessor _DataPayLogAccessor, WxPayTool _WxPayTool)
        {
            DataPayLogAccessor = _DataPayLogAccessor;
            WxPayTool = _WxPayTool;
        }

        public async Task<ApiMessage> GetWxPayPackage(Guid orderId)
        {
            var resultMsg = new ApiMessage();
            try
            {
                var orderEntity = DataPayLogAccessor.OneAsync<OrderEntity>(x => x.Id == orderId);

                //TODO : 实现微信支付打包方法
                resultMsg.Data = WxPayTool.GetWxPayPackage();
            }
            catch (Exception exc)
            {
                resultMsg.Message = exc.Message;
                resultMsg.Success = false;
            }
            return resultMsg;
        }
    }
}
