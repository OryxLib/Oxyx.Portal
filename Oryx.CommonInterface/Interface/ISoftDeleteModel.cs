using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.CommonInterface.Interface
{
    public interface ISoftDeleteModel
    {
        bool IsSoftDelete { get; set; }
    }
}
