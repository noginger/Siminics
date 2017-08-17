using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Cee.Tools.Mvc
{
    /// <summary>
    /// MVC Url 路由转换小写
    /// </summary>
    public class UrlRouteLowerCase : Route
    {
        private static readonly string[] RequiredKeys = new[] { "area", "controller", "action" };

        public UrlRouteLowerCase(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        public UrlRouteLowerCase(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        public UrlRouteLowerCase(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        public UrlRouteLowerCase(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            LowerRouteValues(requestContext.RouteData.Values);
            LowerRouteValues(values);
            LowerRouteValues(Defaults);

            return base.GetVirtualPath(requestContext, values);
        }

        private void LowerRouteValues(RouteValueDictionary values)
        {
            foreach (var key in RequiredKeys)
            {
                if (values.ContainsKey(key) == false) continue;

                var value = values[key];
                if (value == null) continue;

                var valueString = Convert.ToString(value, CultureInfo.InvariantCulture);
                values[key] = valueString.ToLower();
            }

            var otherKyes = values.Keys
                .Except(RequiredKeys, StringComparer.InvariantCultureIgnoreCase)
                .ToArray();

            foreach (var key in otherKyes)
            {
                var value = values[key];
                values.Remove(key);
                values.Add(key.ToLower(), value);
            }
        }
    }
}
