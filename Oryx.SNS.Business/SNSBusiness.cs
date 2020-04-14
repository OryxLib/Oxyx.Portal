using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.SNS.Business
{
    public class SNSBusiness
    {
        public SNSDataAccessor SNSDataAccessor;
        public SNSBusiness(SNSDataAccessor _snsDataAccessor)
        {
            SNSDataAccessor = _snsDataAccessor;
        }

        
    }
}
