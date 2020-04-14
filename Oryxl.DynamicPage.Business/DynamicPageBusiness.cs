using Oryxl.DynamicPage.Accessor;
using Oryxl.DynamicPage.Model;
using System;
using System.Threading.Tasks;

namespace Oryxl.DynamicPage.Business
{
    public class DynamicPageBusiness
    {
        public RouteAccessor routeAccessor;
        public DynamicPageBusiness(RouteAccessor _routeAccessor)
        {
            routeAccessor = _routeAccessor;
        }
        public async Task<bool> MatchRoute(string route)
        {
            return await routeAccessor.Any<RouteEntry>(x => x.RouteValue == route);
        }

        public async Task<string> GetContentByRoute(string route)
        {
            var pageEntry = await routeAccessor.OneAsync<ReponseEntry>(x => x.Route.RouteValue == route, "Master");
            GeneratePageBody(pageEntry);
            return pageEntry?.PageBody;
        }

        private string GeneratePageBody(ReponseEntry pageEntry)
        {
            var pageBodyString = string.Empty;
            if (pageEntry == null)
            {
                return pageBodyString;
            }
            if (pageEntry?.Master != null)
            {
                pageBodyString = pageEntry.Master.PageBody.Replace("{PageBody}", pageEntry.PageBody);
            }
            else
            {
                pageBodyString = pageEntry.PageBody;
            }
            return pageBodyString;
        }
    }
}
