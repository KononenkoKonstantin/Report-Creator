using ReportCreator.WebUI.Models;
using System;
using System.Text;
using System.Web.Mvc;

namespace ReportCreator.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks (
            this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl
            )
        {
            if (pagingInfo.TotalPages < 2)
                return null;
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));                
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag.ToString());
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}