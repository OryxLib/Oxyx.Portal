using Oryx.Shop.DataAccessor.DA.Customer;
using Oryx.Shop.Model.Shop.Customer;
using Oryx.UserAccount.Business;
using Oryx.UserAccount.Model;
using Oryx.UserAccount.Model.UserBusinessExtend;
using Oryx.ViewModel;
using Oryx.ViewModel.Shop.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oryx.Shop.Business.Bs.Customer
{
    public class CustomerBs
    {
        public const string UserRole = "ShopCustomer";

        private readonly DataCustomerAccessor DataCustomerAccessor;

        public UserAccountBusiness userAccountBusiness;

        public CustomerBs(DataCustomerAccessor _DataCustomerAccessor, UserAccountBusiness _userAccountBusiness)
        {
            DataCustomerAccessor = _DataCustomerAccessor;
            userAccountBusiness = _userAccountBusiness;
        }

        public async Task<ApiMessage> RegisterUser(CustomerAccountViewModel caViewModel)
        {
            var apiMsg = new ApiMessage();
            try
            {
                var customerAccount = await DataCustomerAccessor.OneAsync<CustomerAccount>(x => x.OpenId == caViewModel.OpenId);

                if (customerAccount == null)
                {
                    customerAccount = new CustomerAccount
                    {
                        OpenId = caViewModel.OpenId,
                        NickName = caViewModel.NickName,
                        Avatar = caViewModel.Avatar,
                        Id = Guid.NewGuid()
                    };
                    await DataCustomerAccessor.Add(customerAccount);
                }
                apiMsg.Data = customerAccount;
            }
            catch (Exception exc)
            {
                apiMsg.Message = exc.Message;
                apiMsg.Success = false;
            }
            return apiMsg;
        }

        public async Task RegisterUser(Guid UserAccountId, string WeChatKeyName)
        {
            var userAccount = await DataCustomerAccessor.OneAsync<UserAccountEntry>(x => x.Id == UserAccountId, "Roles", "WeChat", "WeChat.OpenIdMapping");

            await userAccountBusiness.SetRole(UserAccountId, UserRole);

            var customerAccount = await DataCustomerAccessor.OneAsync<CustomerAccount>(x => x.UserAccountId == UserAccountId);

            if (customerAccount == null)
            {
                customerAccount = new CustomerAccount
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Avatar = userAccount.Avatar,
                    NickName = userAccount.NickName,
                    OpenId = userAccount.WeChat.OpenIdMapping.FirstOrDefault(x => x.Source == WeChatKeyName).OpenId,
                    UserAccountId = UserAccountId
                };
                await DataCustomerAccessor.Add(customerAccount);
            }
        }

        public async Task<ApiMessage> GetAddressList(string openId)
        {
            var apiMessage = new ApiMessage();
            try
            {
                var customerAccount = await DataCustomerAccessor.ListAsync<CustomerAccount>(x => x.OpenId == openId, 0, 10, "CustomerInfoList");
                apiMessage.Data = customerAccount.Select(x => x.CustomerInfoList).ToList();
            }
            catch (Exception exc)
            {
                apiMessage.Success = false;
                apiMessage.Message = exc.Message;
            }
            return apiMessage;
        }

        public async Task<ApiMessage> GetDefaultAddress(string openId)
        {
            var apiMsg = new ApiMessage();
            try
            {
                apiMsg.Data = await DataCustomerAccessor.OneAsync<CustomerInfo>(x => x.IsDefault && x.Customer.OpenId == openId);
                if (apiMsg.Data == null)
                {
                    var customerInfo = await DataCustomerAccessor.OneAsync<CustomerInfo>(x => x.Customer.OpenId == openId);
                    customerInfo.IsDefault = true;
                    apiMsg.Data = customerInfo;
                    await DataCustomerAccessor.Update(customerInfo);
                }
            }
            catch (Exception exc)
            {
                apiMsg.Message = exc.Message;
                apiMsg.Success = false;
            }
            return apiMsg;
        }

        public async Task<ApiMessage> AppendAddress(AppendAddressViewModel appendAddressViewModel)
        {
            var apiMessage = new ApiMessage();

            try
            {
                var customerAccount = await DataCustomerAccessor.OneAsync<CustomerAccount>(x => x.OpenId == appendAddressViewModel.OpenId);

                if (customerAccount == null)
                {
                    apiMessage.Message = "用户未注册";
                    apiMessage.Success = false;
                }
                else
                {
                    var isDefault = false;
                    if (customerAccount.CustomerInfoList?.Count(x => x.IsDefault) > 0)
                    {
                        isDefault = true;
                    }

                    var customerInfo = new CustomerInfo
                    {
                        Address = appendAddressViewModel.Address,
                        City = appendAddressViewModel.City,
                        Customer = customerAccount,
                        District = appendAddressViewModel.Distribute,
                        IsDefault = isDefault,
                        Name = appendAddressViewModel.Name,
                        PhoneNumber = appendAddressViewModel.Phone,
                        Province = appendAddressViewModel.Province
                    };
                    await DataCustomerAccessor.Add(customerInfo);
                }
            }
            catch (Exception exc)
            {
                apiMessage.Message = exc.Message;
                apiMessage.Success = false;
            }
            return apiMessage;
        }

        public async Task<ApiMessage> DeleteAddress(Guid Id)
        {
            var apiMessage = new ApiMessage();
            try
            {
                var customerInfo = await DataCustomerAccessor.OneAsync<CustomerInfo>(x => x.Id == Id);
                await DataCustomerAccessor.Delete(customerInfo);
            }
            catch (Exception exc)
            {
                apiMessage.Success = false;
                apiMessage.Message = exc.Message;
            }
            return apiMessage;
        }
    }
}
