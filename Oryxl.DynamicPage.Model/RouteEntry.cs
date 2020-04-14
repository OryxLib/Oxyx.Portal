using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryxl.DynamicPage.Model
{

    public class RouteEntry
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string RouteValue { get; set; }

        public string ParentValue { get; set; }

        public string TotalRouteValue { get; set; }

        public string RouteLevel { get; set; }

        public int Order { get; set; }

        public List<RouteEntry> Children { get; set; }

        public Guid? ParentRouteId { get; set; }

        [ForeignKey("ParentRouteId")]
        public RouteEntry ParentRoute { get; set; }

        public ReponseEntry Page { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
