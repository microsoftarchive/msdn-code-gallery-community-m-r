using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Helper
{
    public static class HtmlHelperRepeater
    {
        public static void Repeater<T>(this HtmlHelper html
        , IEnumerable<T> items
        , string className
        , string classNameAlt
        , Action<T, string> render)
    {
        if (items == null)
        return;

          int i = 0;
        items.ForEach(item =>
        {
            render(item, (i++ % 2 == 0) ? className : classNameAlt);
        });
    }

        public static void Repeater<T>(this HtmlHelper html
          , string viewDataKey
          , string cssClass
          , string altCssClass
          , Action<T, string> render)
        {
            var items = GetViewDataAsEnumerable<T>(html, viewDataKey);

            int i = 0;
            items.ForEach(item =>
            {
                render(item, (i++ % 2 == 0) ? cssClass : altCssClass);
            });
        }
      
            public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
            {
                foreach (T item in source)
                    action(item);
            }
      

        static IEnumerable<T> GetViewDataAsEnumerable<T>(HtmlHelper html, string viewDataKey)
        {
            var items = html.ViewContext.ViewData as IEnumerable<T>;
            var viewData = html.ViewContext.ViewData as IDictionary<string, object>;
            if (viewData != null)
            {
                items = viewData[viewDataKey] as IEnumerable<T>;
            }
            else
            {
                items = new Dictionary<string, object>(viewData)[viewDataKey]
                  as IEnumerable<T>;
            }
            return items;
        }
    }
}