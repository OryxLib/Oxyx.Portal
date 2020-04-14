using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.CommonDbOperation
{
    public class BaseBusiness<T>
        where T : class, IEntityModel, new()
    {
        private readonly CommonAccessor commonAccessor;
        public BaseBusiness(CommonAccessor _commonAccessor)
        {
            commonAccessor = _commonAccessor;
        }
    }
}
