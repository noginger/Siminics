using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cee.Tools.Mvc
{
    public static class UrlRouteLowerCaseMaper
    {
        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url)
        {
            return routes.MapLowerCaseUrlRoute(name, url, null, null);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, null);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            return routes.MapLowerCaseUrlRoute(name, url, null, null, namespaces);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, constraints, null);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            return routes.MapLowerCaseUrlRoute(name, url, defaults, null, namespaces);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            UrlRouteLowerCase route2 = new UrlRouteLowerCase(url, new MvcRouteHandler());
            route2.Defaults = new RouteValueDictionary(defaults);
            route2.Constraints = new RouteValueDictionary(constraints);
            route2.DataTokens = new RouteValueDictionary();
            UrlRouteLowerCase item = route2;
            if ((namespaces != null) && (namespaces.Length > 0))
            {
                item.DataTokens["Namespaces"] = namespaces;
            }
            routes.Add(name, item);
            return item;
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url)
        {
            return context.MapLowerCaseUrlRoute(name, url, null);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, object defaults)
        {
            return context.MapLowerCaseUrlRoute(name, url, defaults, null);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, string[] namespaces)
        {
            return context.MapLowerCaseUrlRoute(name, url, null, namespaces);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints)
        {
            return context.MapLowerCaseUrlRoute(name, url, defaults, constraints, null);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, object defaults, string[] namespaces)
        {
            return context.MapLowerCaseUrlRoute(name, url, defaults, null, namespaces);
        }

        public static UrlRouteLowerCase MapLowerCaseUrlRoute(this AreaRegistrationContext context, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if ((namespaces == null) && (context.Namespaces != null))
            {
                namespaces = context.Namespaces.ToArray<string>();
            }
            UrlRouteLowerCase route = context.Routes.MapLowerCaseUrlRoute(name, url, defaults, constraints, namespaces);
            route.DataTokens["area"] = context.AreaName;
            bool flag = (namespaces == null) || (namespaces.Length == 0);
            route.DataTokens["UseNamespaceFallback"] = flag;
            return route;
        }
    }
}
