using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryxl.DynamicPage.Model
{

    public class ReponseEntry
    {
        public Guid Id { get; set; }

        public Guid? RouteId { get; set; }

        [ForeignKey("RouteId")]
        public RouteEntry Route { get; set; }

        public string Title { get; set; }

        public string PageBody { get; set; }

        public Guid? MasterId { get; set; }

        [ForeignKey("MasterId")]
        public ReponseEntry Master { get; set; }

        public bool IsMaster { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
