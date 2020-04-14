using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.SpiderCartoon.Core.V3.SpiderGroup
{
    public class SpiderDbContext : DbContext
    {
        public DbSet<SpiderLog> SpiderLog { get; set; }
    }
}
