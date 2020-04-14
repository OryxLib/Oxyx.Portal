using Oryx.CommonInterface.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.SNS.Model
{
    public class ReportEntry : IEntityModel
    {
        public Guid Id { get; set; }


        public DateTime CreateTime { get; set; }
    }
}
