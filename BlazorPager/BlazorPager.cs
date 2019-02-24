using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Webdiyer.AspNetCore
{
    public class BlazorPager : ComponentBase
    {
        [Parameter]
        private string ContainerTagName { get; set; } = "div";
        
        [Parameter]
        public int PageSize { get; private set; } = 10;

        [Parameter]
        public int TotalItemCount { get; private set; }
        
        [Parameter]
        private string PageNumberFormatString { get; set; }

        [Parameter]
        private string CurrentPageNumberFormatString { get; set; }

        [Parameter]
        private string RoutePattern { get; set; } = "{0}";

        [Parameter]
        private int NumericPagerItemCount { get; set; } = 10;

        [Parameter]
        private Action<int> OnPageChanged { get; set; }
        
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
        private string FirstPageText { get; set; } = "<<";

        [Parameter]
        private string PrevPageText { get; set; } = "<";

        [Parameter]
        private string NextPageText { get; set; } = ">";

        [Parameter]
        private string LastPageText { get; set; } = ">>";

        //[Parameter]
        //private string NumericPagerItemTemplate { get; set; }


        //[Parameter]
        //private string CurrentPagerItemTemplate { get; set; }


        //[Parameter]
        //private string NavigationPagerItemTemplate { get; set; }


        //[Parameter]
        //private string MorePagerItemTemplate { get; set; }


        //[Parameter]
        //private string DisabledPagerItemTemplate { get; set; }


        void ChangePage(int pageIndex)
        {
            CurrentPageIndex = pageIndex;
            OnPageChanged?.Invoke(pageIndex);
            StateHasChanged();
        }

        IDictionary<string, object> customAttributes=new Dictionary<string,object>();

        public override Task SetParametersAsync(ParameterCollection parameters)
        {
            var props = this.GetType().GetProperties(BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Public).Where(p => p.GetCustomAttributes(false).Any(a => a is ParameterAttribute)).Select(p=>p.Name).ToArray();
            var prms=(IDictionary<string, object>)parameters.ToDictionary();

            //remove custom attributes from parameter collection and add to customAttributes variable for rendering in BuildRenderTree method
            foreach (var prm in prms)
            {
                if (!props.Contains(prm.Key))
                {
                    prms.Remove(prm);
                    customAttributes.Add(prm);
                }
            }
            return base.SetParametersAsync(ParameterCollection.FromDictionary(prms));
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

        public int TotalPageCount { get; set; }

        private int startPageIndex { get; set; }

        private int endPageIndex { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            if (!AutoHide || TotalItemCount > PageSize)
            {
                var seq = 0;
                builder.OpenElement(seq, ContainerTagName);
                if (customAttributes != null)
                {
                    foreach(var attr in customAttributes)
                    {
                        builder.AddAttribute(++seq, attr.Key, attr.Value);
                    }
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
                var npAttr = string.IsNullOrWhiteSpace(NumericPagerItemCssClass) ? null : new Dictionary<string, object> { { "class", NumericPagerItemCssClass } };
                if (ShowNumericPagerItems)
                {
                    for (int i = startPageIndex; i <= endPageIndex; i++)
                    {
                        var pageIndex = i;
                        createPagerItem(builder,ref seq, pageIndex,i.ToString(),npAttr,true);                        
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

        private void createPagerItem(RenderTreeBuilder builder,ref int seq,int pageIndex,string text,Dictionary<string,object> attributes=null, bool isNumericPage = false)
        {
            builder.OpenElement(++seq, "a");
            if (attributes != null)
            {
                foreach (var de in attributes)
                {
                    builder.AddAttribute(++seq, de.Key, de.Value);
                }
            }
            if (pageIndex > 0)
            {
                builder.AddAttribute(++seq, "href", string.Format(RoutePattern, pageIndex));
                builder.AddAttribute(++seq, "onclick", BindMethods.GetEventHandlerValue<UIMouseEventArgs>(() => ChangePage(pageIndex)));
            }
            var numberFormat = string.IsNullOrWhiteSpace(PageNumberFormatString)?"{0}":PageNumberFormatString;
            if (pageIndex == CurrentPageIndex&&!string.IsNullOrWhiteSpace(CurrentPageNumberFormatString))
            {
                numberFormat = CurrentPageNumberFormatString;
            }
            builder.AddContent(++seq, isNumericPage?string.Format(numberFormat,text):text);
            builder.CloseElement();
        }

    }
}