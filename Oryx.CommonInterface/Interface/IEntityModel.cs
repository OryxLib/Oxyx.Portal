using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.CommonInterface.Interface
{
    public interface IEntityModel
    {
        Guid Id { get; set; }
        DateTime CreateTime { get; set; }
    }
}
