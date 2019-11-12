using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAPO.Demo.MVC.Utility
{
    public static class MVCHelper
    {
        public static MvcHtmlString Image(this HtmlHelper helper,string src, string alt,string id)
        {
            var tagBuilder=new TagBuilder("img");
            tagBuilder.MergeAttribute("src",src);
            tagBuilder.MergeAttribute("alt",alt);
            tagBuilder.MergeAttribute("id", id);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
        public static MvcHtmlString HyperLink(this HtmlHelper helper, string @class, string href,string ID)
        {
            var tagBuilder = new TagBuilder("a");
            tagBuilder.MergeAttribute("@class", @class);
            tagBuilder.MergeAttribute("href", href);
            tagBuilder.MergeAttribute("ID", ID);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}