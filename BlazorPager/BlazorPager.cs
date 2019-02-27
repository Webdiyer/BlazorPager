using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Services;

namespace Webdiyer.AspNetCore
{
    public class BlazorPager : ComponentBase
    {
        #region properties

        [Inject]
        protected IUriHelper uriHelper { get; set; }

        [Parameter]
        private string TagName { get; set; } = "div";
        
        [Parameter]
        public int PageSize { get; private set; } = 10;

        [Parameter]
        public int TotalItemCount { get; private set; }
        
        [Parameter]
        private string NumericPagerItemTextFormatString { get; set; }

        [Parameter]
        private string CurrentPagerItemTextFormatString { get; set; }

        [Parameter]
        private string RoutePattern { get; set; } = "{0}";

        [Parameter]
        private int NumericPagerItemCount { get; set; } = 10;

        [Parameter]
        private Action<int> OnPageChanged { get; set; }

        [Parameter]
        private string PagerItemCssClass { get; set; }

        [Parameter]
        private string MorePagerItemCssClass { get; set; }

        [Parameter]
        private string NumericPagerItemCssClass { get; set; }

        [Parameter]
        private string NavigationPagerItemCssClass { get; set; }

        [Parameter]
        private string CurrentPagerItemCssClass { get; set; }

        [Parameter]
        private string DisabledPagerItemCssClass { get; set; }

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

        [Parameter]
        public int CurrentPageIndex { get; private set; } = 1;

        [Parameter]
        private string PagerItemContainerTagName { get; set; }

        [Parameter]
        private string PagerItemContainerCssClass { get; set; }

        [Parameter]
        private string NumericPagerItemContainerTagName { get; set; }

        [Parameter]
        private string NumericPagerItemContainerCssClass { get; set; }


        [Parameter]
        private string CurrentPagerItemContainerTagName { get; set; }

        [Parameter]
        private string CurrentPagerItemContainerCssClass { get; set; }


        [Parameter]
        private string NavigationPagerItemContainerTagName { get; set; }

        [Parameter]
        private string NavigationPagerItemContainerCssClass { get; set; }

        [Parameter]
        private string MorePagerItemContainerTagName { get; set; }

        [Parameter]
        private string MorePagerItemContainerCssClass { get; set; }

        [Parameter]
        private string DisabledPagerItemContainerTagName { get; set; }

        [Parameter]
        private string DisabledPagerItemContainerCssClass { get; set; }
        #endregion

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

        public int TotalPageCount { get; set; }

        private int startPageIndex { get; set; }

        private int endPageIndex { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            if (!AutoHide || TotalItemCount > PageSize)
            {
                var seq = 0;
                builder.OpenElement(seq, TagName);
                if (customAttributes != null)
                {
                    foreach(var attr in customAttributes)
                    {
                        builder.AddAttribute(++seq, attr.Key, attr.Value);
                    }
                }
                //first page
                if (ShowFirstLast)
                {
                    createPagerItem(builder, ref seq, CurrentPageIndex>1?1:0,FirstPageText,CurrentPageIndex==1?PagerItemType.Disabled: PagerItemType.Navigation);
                }
                //prev page
                if (ShowPrevNext)
                {
                    createPagerItem(builder,ref seq,CurrentPageIndex-1,PrevPageText, CurrentPageIndex == 1 ? PagerItemType.Disabled : PagerItemType.Navigation);
                }
                //more page
                if (ShowMorePagerItems && startPageIndex>1)
                {
                    createPagerItem(builder, ref seq, startPageIndex-1, MorePageText, PagerItemType.More);
                }
                if (ShowNumericPagerItems)
                {
                    for (int i = startPageIndex; i <= endPageIndex; i++)
                    {
                        var pageIndex = i;
                        createPagerItem(builder,ref seq, pageIndex,i.ToString(),CurrentPageIndex==i?PagerItemType.Current:PagerItemType.Number);                        
                    }
                }
                //more page
                if (ShowMorePagerItems && endPageIndex < TotalPageCount)
                {
                    createPagerItem(builder, ref seq, endPageIndex + 1, MorePageText, PagerItemType.More);
                }
                //next page
                if (ShowPrevNext)
                {
                    createPagerItem(builder, ref seq, CurrentPageIndex<TotalPageCount?CurrentPageIndex + 1:0,NextPageText, CurrentPageIndex<TotalPageCount? PagerItemType.Navigation:PagerItemType.Disabled);
                }
                //last page
                if (ShowFirstLast)
                {
                    createPagerItem(builder, ref seq, CurrentPageIndex<TotalPageCount ? TotalPageCount : 0,LastPageText, CurrentPageIndex<TotalPageCount ? PagerItemType.Navigation: PagerItemType.Disabled );
                }
                builder.CloseElement();
            }
        }

        public void GoToPage(int pageIndex)
        {
            uriHelper.NavigateTo(string.Format(RoutePattern, pageIndex));
            ChangePage(pageIndex);
        }

        private void createPagerItem(RenderTreeBuilder builder, ref int seq, int pageIndex, string text, PagerItemType itemType)
        {
            var containerTag = PagerItemContainerTagName;
            var containerClass = PagerItemContainerCssClass;
            var itemClass = PagerItemCssClass;
            switch (itemType)
            {
                case PagerItemType.Number:
                    containerTag = GetFirstNonNullString(new[] { NumericPagerItemContainerTagName, containerTag });
                    containerClass = GetFirstNonNullString(new[] { NumericPagerItemContainerCssClass, containerClass });
                    itemClass = GetFirstNonNullString(new[] { NumericPagerItemCssClass, itemClass });
                    break;
                case PagerItemType.Navigation:
                    containerTag = GetFirstNonNullString(new[] { NavigationPagerItemContainerTagName, containerTag });
                    containerClass = GetFirstNonNullString(new[] { NavigationPagerItemContainerCssClass, containerClass });
                    itemClass = GetFirstNonNullString(new[] { NavigationPagerItemCssClass,itemClass });
                    break;
                case PagerItemType.Current:
                    containerTag = GetFirstNonNullString(new[] { CurrentPagerItemContainerTagName, NumericPagerItemContainerTagName, containerTag });
                    containerClass = GetFirstNonNullString(new[] { CurrentPagerItemContainerCssClass, NumericPagerItemContainerCssClass, containerClass });
                    itemClass = GetFirstNonNullString(new[] { CurrentPagerItemCssClass, NumericPagerItemCssClass, itemClass });
                    break;
                case PagerItemType.More:
                    containerTag = GetFirstNonNullString(new[] { MorePagerItemContainerTagName, containerTag });
                    containerClass = GetFirstNonNullString(new[] { MorePagerItemContainerCssClass, containerClass });
                    itemClass = GetFirstNonNullString(new[] { MorePagerItemCssClass, itemClass });
                    break;
                case PagerItemType.Disabled:
                    containerTag = GetFirstNonNullString(new[] { DisabledPagerItemContainerTagName,NavigationPagerItemContainerTagName, containerTag });
                    containerClass = GetFirstNonNullString(new[] { DisabledPagerItemContainerCssClass,NavigationPagerItemContainerCssClass, containerClass });
                    itemClass = GetFirstNonNullString(new[] { DisabledPagerItemCssClass, NavigationPagerItemCssClass, itemClass });
                    break;
            }
            bool hasContainerTag = !string.IsNullOrWhiteSpace(containerTag);
            if (hasContainerTag)
            {
                builder.OpenElement(++seq, containerTag);
                builder.AddAttribute(++seq, "class", containerClass);
            }
            builder.OpenElement(++seq, "a");
            //if (attributes != null)
            //{
            //    foreach (var de in attributes)
            //    {
            //        builder.AddAttribute(++seq, de.Key, de.Value);
            //    }
            //}
            builder.AddAttribute(++seq, "class", itemClass);
            if (pageIndex > 0)
            {
                builder.AddAttribute(++seq, "href", string.Format(RoutePattern, pageIndex));
                builder.AddAttribute(++seq, "onclick", BindMethods.GetEventHandlerValue<UIMouseEventArgs>(() => ChangePage(pageIndex)));
            }
            var pagerItemText = text;
            if (itemType == PagerItemType.Number || itemType == PagerItemType.Current)
            {
                var numberFormat = string.IsNullOrWhiteSpace(NumericPagerItemTextFormatString) ? "{0}" : NumericPagerItemTextFormatString;
                if (itemType == PagerItemType.Current && !string.IsNullOrWhiteSpace((CurrentPagerItemTextFormatString)))
                {
                    numberFormat = CurrentPagerItemTextFormatString;
                }
                pagerItemText = string.Format(numberFormat, text);
            }
            builder.AddContent(++seq,pagerItemText);
            builder.CloseElement();
            if (hasContainerTag)
            {
                builder.CloseElement();
            }
        }

        private string GetFirstNonNullString(string[] values)
        {
            for(var i = 0; i < values.Length; i++)
            {
                if (values[i]!=null)
                    return values[i];
            }
            return string.Empty;
        }
    }
    internal enum PagerItemType : byte
    {
        Navigation,
        More,
        Number,
        Current,
        Disabled
    }
}