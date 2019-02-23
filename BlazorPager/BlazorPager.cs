using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Webdiyer.AspNetCore
{
    public class BlazorPager : ComponentBase
    {
        [Parameter]
        private string TagName { get; set; } = "div";

        [Parameter]
        public int PageSize { get; private set; } = 10;

        [Parameter]
        public int TotalItemCount { get; private set; }


        [Parameter]
        private string PageNumberFormatString { get; set; }

        [Parameter]
        private string RoutePattern { get; set; }

        [Parameter]
        private int NumericPagerItemCount { get; set; } = 10;

        [Parameter]
        private Action<int> OnPageChanged { get; set; }

        [Parameter]
        private string CssClass { get; set; }

        [Parameter]
        private string NumericPagerItemCssClass { get; set; }

        [Parameter]
        private string NavigationPagerItemCssClass { get; set; }

        [Parameter]
        private bool AutoHide { get; set; } = true;

        [Parameter]
        private string MorePageText { get; set; } = "...";

        [Parameter]
        private bool ShowNumericPagerItems { get; set; } = true;

        [Parameter]
        private bool ShowMorePagerItems { get; set; } = true;
        
        [Parameter]
        private bool ShowFirstLast { get; set; } = true;

        [Parameter]
        private bool ShowPrevNext { get; set; } = true;

        [Parameter]
        private string FirstPageText { get; set; } = "首页";

        [Parameter]
        private string NextPageText { get; set; } = "下页";

        [Parameter]
        private string PrevPageText { get; set; } = "上页";

        [Parameter]
        private string LastPageText { get; set; } = "尾页";


        void ChangePage(int pageIndex)
        {
            CurrentPageIndex = pageIndex;
            OnPageChanged?.Invoke(pageIndex);
            StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            var _startPageIndex = CurrentPageIndex - (NumericPagerItemCount / 2);
            if (_startPageIndex + NumericPagerItemCount > TotalPageCount)
                _startPageIndex = TotalPageCount + 1 - NumericPagerItemCount;
            if (_startPageIndex < 1)
                _startPageIndex = 1;
            startPageIndex = _startPageIndex;
            var _endPageIndex = _startPageIndex + NumericPagerItemCount - 1;
            if (_endPageIndex > TotalPageCount)
                _endPageIndex = TotalPageCount;
            endPageIndex = _endPageIndex;
            base.OnParametersSet();
        }
        public int CurrentPageIndex { get; private set; } = 1;

        private int startPageIndex { get; set; }

        private int endPageIndex { get; set; }

        public int TotalPageCount { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            if (!AutoHide || TotalItemCount > PageSize)
            {
                var seq = 0;
                builder.OpenElement(seq, TagName);
                if (!string.IsNullOrWhiteSpace(CssClass))
                {
                    builder.AddAttribute(++seq, "class", CssClass);
                }
                var navAttr = new Dictionary<string, object> { { "class", NavigationPagerItemCssClass }};
                //first page
                if (ShowFirstLast)
                {
                    createPagerItem(builder, ref seq, CurrentPageIndex>1?1:0,FirstPageText,navAttr);
                }
                //prev page
                if (ShowPrevNext)
                {
                    createPagerItem(builder,ref seq,CurrentPageIndex-1,PrevPageText, navAttr);
                }
                //more page
                if (ShowMorePagerItems && startPageIndex>1)
                {
                    createPagerItem(builder, ref seq, startPageIndex-1, MorePageText, navAttr);
                }
                if (ShowNumericPagerItems)
                {
                    for (int i = startPageIndex; i <= endPageIndex; i++)
                    {
                        var pageIndex = i;
                        createPagerItem(builder,ref seq, pageIndex,i.ToString(), new Dictionary<string, object> { { "class", NumericPagerItemCssClass } });                        
                    }
                }
                //more page
                if (ShowMorePagerItems && endPageIndex < TotalPageCount)
                {
                    createPagerItem(builder, ref seq, endPageIndex + 1, MorePageText, navAttr);
                }
                //next page
                if (ShowPrevNext)
                {
                    createPagerItem(builder, ref seq, CurrentPageIndex<TotalPageCount?CurrentPageIndex + 1:0,NextPageText, navAttr);
                }
                //last page
                if (ShowFirstLast)
                {
                    createPagerItem(builder, ref seq, CurrentPageIndex<TotalPageCount ? TotalPageCount : 0,LastPageText, navAttr);
                }
                builder.CloseElement();
            }
        }

        private void createPagerItem(RenderTreeBuilder builder,ref int seq,int pageIndex,string text,Dictionary<string,object> attributes)
        {
            builder.OpenElement(++seq, "a");
            foreach(var de in attributes)
            {
                builder.AddAttribute(++seq, de.Key, de.Value);
            }
            if (pageIndex > 0)
            {
                builder.AddAttribute(++seq, "href", string.Format(RoutePattern, pageIndex));
                builder.AddAttribute(++seq, "onclick", BindMethods.GetEventHandlerValue<UIMouseEventArgs>(() => ChangePage(pageIndex)));
            }
            builder.AddContent(++seq, string.Format((PageNumberFormatString ?? "{0}"), text));
            builder.CloseElement();
        }

    }
}