using Adminsite.Extension;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using oShopSolution.ViewModels.Common;
using System.Text;

namespace Adminsite.PageTagHelpers
{
    [HtmlTargetElement("pagenavigation", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("pagenavigation", Attributes = QueryParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = PaginationParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = ShowTotalSummaryParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = ShowPagerItemsParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = ShowFirstParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = ShowPreviousParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = ShowNextParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = ShowLastParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = ShowIndividualPagesParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = RenderEmptyParametersParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = IndividualPagesDisplayedCountParamAttributeName)]
    [HtmlTargetElement("pagenavigation", Attributes = BooleanParamAttributeName)]
    public class PageNavigationTagHelper : TagHelper
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private const string QueryParamAttributeName = "asp-query-param";

        [HtmlAttributeName(QueryParamAttributeName)]
        public string QueryParam { get; set; } = "page";

        private const string PaginationParamAttributeName = "asp-pagination";

        [HtmlAttributeName(PaginationParamAttributeName)]
        public IPageableModel Pagination { get; set; }

        private readonly IHtmlHelper _htmlHelper;

        private const string ShowTotalSummaryParamAttributeName = "asp-show-total-summary";

        [HtmlAttributeName(ShowTotalSummaryParamAttributeName)]
        public bool ShowTotalSummary { get; set; }

        private const string ShowPagerItemsParamAttributeName = "asp-show-pager-items";

        [HtmlAttributeName(ShowPagerItemsParamAttributeName)]
        public bool ShowPagerItems { get; set; } = true;

        private const string ShowFirstParamAttributeName = "asp-show-first";

        [HtmlAttributeName(ShowFirstParamAttributeName)]
        public bool ShowFirst { get; set; } = true;

        private const string ShowPreviousParamAttributeName = "asp-show-previous";

        [HtmlAttributeName(ShowPreviousParamAttributeName)]
        public bool ShowPrevious { get; set; } = true;

        private const string ShowNextParamAttributeName = "asp-show-next";

        [HtmlAttributeName(ShowNextParamAttributeName)]
        public bool ShowNext { get; set; } = true;

        private const string ShowLastParamAttributeName = "asp-show-last";

        [HtmlAttributeName(ShowLastParamAttributeName)]
        public bool ShowLast { get; set; } = true;

        private const string ShowIndividualPagesParamAttributeName = "asp-show-individual-pages";

        [HtmlAttributeName(ShowIndividualPagesParamAttributeName)]
        public bool ShowIndividualPages { get; set; } = true;

        private const string RenderEmptyParametersParamAttributeName = "asp-render-empty-parameters";

        [HtmlAttributeName(RenderEmptyParametersParamAttributeName)]
        public bool RenderEmptyParameters { get; set; } = true;

        private const string IndividualPagesDisplayedCountParamAttributeName = "asp-individual-pages-displayed-count";

        [HtmlAttributeName(IndividualPagesDisplayedCountParamAttributeName)]
        public int IndividualPagesDisplayedCount { get; set; } = 5;

        private const string BooleanParamAttributeName = "asp-boolean-params";

        [HtmlAttributeName(BooleanParamAttributeName)]
        public IList<string> BooleanParameterNames { get; set; } = new List<string>();

        public PageNavigationTagHelper(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();

            (_htmlHelper as IViewContextAware).Contextualize(ViewContext);

            var links = GenerateHtmlString();

            if (!string.IsNullOrEmpty(links))
            {
                var sb = new StringBuilder();
                _ = sb.AppendFormat("<nav aria-label=\"Page navigation\">");
                _ = sb.Append(Environment.NewLine);
                _ = sb.AppendFormat(links);
                _ = sb.Append(Environment.NewLine);
                _ = sb.AppendFormat("</nav>");
                _ = sb.Append(Environment.NewLine);
                _ = output.Content.SetHtmlContent(sb.ToString());
            }
            return Task.CompletedTask;
        }

        public virtual string GenerateHtmlString()
        {
            if (Pagination.TotalItems == 0)
                return null;

            var links = new StringBuilder();
            if (ShowTotalSummary && (Pagination.TotalPages > 0))
            {
                _ = links.Append("<li class=\"total-summary\">");
                _ = links.Append(string.Format("CurrentPage", Pagination.PageIndex + 1, Pagination.TotalPages, Pagination.TotalItems));
                _ = links.Append("</li>");
            }
            if (ShowPagerItems && (Pagination.TotalPages > 1))
            {
                if (ShowFirst)
                {
                    //first page
                    if ((Pagination.PageIndex >= 3) && (Pagination.TotalPages > IndividualPagesDisplayedCount))
                    {
                        _ = links.Append(CreatePageLink(1, "First", "first-page page-item"));
                    }
                }
                if (ShowPrevious)
                {
                    //previous page
                    if (Pagination.PageIndex > 0)
                    {
                        _ = links.Append(CreatePageLink(Pagination.PageIndex, "Previous", "previous-page page-item"));
                    }
                }
                if (ShowIndividualPages)
                {
                    //individual pages
                    int firstIndividualPageIndex = GetFirstIndividualPageIndex();
                    int lastIndividualPageIndex = GetLastIndividualPageIndex();
                    for (int i = firstIndividualPageIndex; i <= lastIndividualPageIndex; i++)
                    {
                        if (Pagination.PageIndex == i)
                        {
                            _ = links.AppendFormat("<li class=\"current-page page-item\"><a class=\"page-link\">{0}</a></li>", (i + 1));
                        }
                        else
                        {
                            _ = links.Append(CreatePageLink(i + 1, (i + 1).ToString(), "individual-page page-item"));
                        }
                    }
                }
                if (ShowNext)
                {
                    //next page
                    if ((Pagination.PageIndex + 1) < Pagination.TotalPages)
                    {
                        _ = links.Append(CreatePageLink(Pagination.PageIndex + 2, "Next", "next-page page-item"));
                    }
                }
                if (ShowLast)
                {
                    //last page
                    if (((Pagination.PageIndex + 3) < Pagination.TotalPages) && (Pagination.TotalPages > IndividualPagesDisplayedCount))
                    {
                        _ = links.Append(CreatePageLink(Pagination.TotalPages, "Last", "last-page page-item"));
                    }
                }
            }

            var result = links.ToString();
            if (!string.IsNullOrEmpty(result))
            {
                result = "<ul class=\"pagination\">" + result + "</ul>";
            }
            return result;
        }

        protected virtual string CreatePageLink(int pageNumber, string text, string cssClass)
        {
            var liBuilder = new TagBuilder("li");
            if (!string.IsNullOrWhiteSpace(cssClass))
                liBuilder.AddCssClass(cssClass);

            var aBuilder = new TagBuilder("a");
            _ = aBuilder.InnerHtml.AppendHtml(text);
            aBuilder.AddCssClass("page-link");
            aBuilder.MergeAttribute("href", CreateDefaultUrl(pageNumber));

            _ = liBuilder.InnerHtml.AppendHtml(aBuilder);
            return liBuilder.RenderHtmlContent();
        }

        protected virtual string CreateDefaultUrl(int pageNumber)
        {
            var routeValues = new RouteValueDictionary();

            var parametersWithEmptyValues = new List<string>();
            foreach (var key in _htmlHelper.ViewContext.HttpContext.Request.Query.Keys.Where(key => key != null))
            {
                var value = _htmlHelper.ViewContext.HttpContext.Request.Query[key].ToString();
                if (RenderEmptyParameters && string.IsNullOrEmpty(value))
                {
                    parametersWithEmptyValues.Add(key);
                }
                else
                {
                    if (BooleanParameterNames.Contains(key, StringComparer.OrdinalIgnoreCase))
                    {
                        //find more info here: http://www.mindstorminteractive.com/pages/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
                        if (!string.IsNullOrEmpty(value) && value.Equals("true,false", StringComparison.OrdinalIgnoreCase))
                        {
                            value = "true";
                        }
                    }
                    routeValues[key] = value;
                }
            }

            if (pageNumber > 1)
            {
                routeValues[QueryParam] = pageNumber;
            }
            else
            {
                //SEO. we do not render pageindex query string parameter for the first page
                if (routeValues.ContainsKey(QueryParam))
                {
                    _ = routeValues.Remove(QueryParam);
                }
            }

            var url = $"{ViewContext.HttpContext?.Request?.Scheme}://{ViewContext.HttpContext?.Request?.Host}{ViewContext.HttpContext?.Request?.Path}";
            foreach (var routeValue in routeValues)
            {
                url = oShopSolution.Utilities.Exceptions.CommonExtensions.ModifyQueryString(url, routeValue.Key, routeValue.Value?.ToString());
            }
            if (RenderEmptyParameters && parametersWithEmptyValues.Any())
            {
                foreach (var key in parametersWithEmptyValues)
                {
                    url = oShopSolution.Utilities.Exceptions.CommonExtensions.ModifyQueryString(url, key, null);
                }
            }
            return url;
        }

        protected virtual int GetFirstIndividualPageIndex()
        {
            if ((Pagination.TotalPages < IndividualPagesDisplayedCount) ||
                ((Pagination.PageIndex - (IndividualPagesDisplayedCount / 2)) < 0))
            {
                return 0;
            }
            if ((Pagination.PageIndex + (IndividualPagesDisplayedCount / 2)) >= Pagination.TotalPages)
            {
                return (Pagination.TotalPages - IndividualPagesDisplayedCount);
            }
            return (Pagination.PageIndex - (IndividualPagesDisplayedCount / 2));
        }

        protected virtual int GetLastIndividualPageIndex()
        {
            int num = IndividualPagesDisplayedCount / 2;
            if ((IndividualPagesDisplayedCount % 2) == 0)
            {
                num--;
            }
            if ((Pagination.TotalPages < IndividualPagesDisplayedCount) ||
                ((Pagination.PageIndex + num) >= Pagination.TotalPages))
            {
                return (Pagination.TotalPages - 1);
            }
            if ((Pagination.PageIndex - (IndividualPagesDisplayedCount / 2)) < 0)
            {
                return (IndividualPagesDisplayedCount - 1);
            }
            return (Pagination.PageIndex + num);
        }
    }
}
