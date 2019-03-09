using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace Webdiyer.AspNetCore
{
    ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Class[@name="BlazorPager"]/*'/>
    public partial class BlazorPager : ComponentBase
    {
        private void ChangePage(int pageIndex)
        {
            if (pageIndex > 0&&pageIndex!=CurrentPageIndex)
            {
                CurrentPageIndex = pageIndex;
                OnPageChanged?.Invoke(pageIndex);
                StateHasChanged();
            }
        }

        IDictionary<string, object> customAttributes=new Dictionary<string,object>();

        public override Task SetParametersAsync(ParameterCollection parameters)
        {
            var props = this.GetType().GetProperties(BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Public).Where(p => p.GetCustomAttributes(false).Any(a => a is ParameterAttribute)).Select(p=>p.Name).ToArray();
            var allPrms=(IDictionary<string, object>)parameters.ToDictionary();
            var validPrms = new Dictionary<string,object>();
            //remove custom attributes from parameter collection and add to customAttributes variable for rendering in BuildRenderTree method

            foreach (var prm in allPrms)
            {
                if (props.Contains(prm.Key))
                {
                    validPrms.Add(prm.Key, prm.Value);
                }
                else
                {
                    if (!customAttributes.ContainsKey(prm.Key))
                    {
                        customAttributes.Add(prm);
                    }
                }
            }
            return base.SetParametersAsync(ParameterCollection.FromDictionary(validPrms));
        }

        protected override void OnInit()
        {
            if (InitPageIndex > 1)
            {
                CurrentPageIndex = InitPageIndex;
            }
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
                RenderAfterBeginTag?.Invoke(builder);
                //first page
                if (ShowFirstLast)
                {
                    createPagerItem(builder, ref seq, CurrentPageIndex > 1?1:0,FirstPageText, CurrentPageIndex == 1?PagerItemType.Disabled: PagerItemType.Navigation);
                }
                //prev page
                if (ShowPrevNext)
                {
                    createPagerItem(builder,ref seq, CurrentPageIndex - 1,PrevPageText, CurrentPageIndex == 1 ? PagerItemType.Disabled : PagerItemType.Navigation);
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
                        var pageNum = i;
                        createPagerItem(builder,ref seq, pageNum,pageNum.ToString(), CurrentPageIndex == pageNum?PagerItemType.Current:PagerItemType.Number);                        
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
                    createPagerItem(builder, ref seq, CurrentPageIndex < TotalPageCount? CurrentPageIndex + 1:0,NextPageText, CurrentPageIndex < TotalPageCount? PagerItemType.Navigation:PagerItemType.Disabled);
                }
                //last page
                if (ShowFirstLast)
                {
                    createPagerItem(builder, ref seq, CurrentPageIndex < TotalPageCount ? TotalPageCount : 0,LastPageText, CurrentPageIndex < TotalPageCount ? PagerItemType.Navigation: PagerItemType.Disabled );
                }
                RenderBeforeEndTag?.Invoke(builder);
                builder.CloseElement();
            }
        }

        public void GoToPage(int pageIndex)
        {
            if (pageIndex > 0 && pageIndex != CurrentPageIndex)
            {
                UriHelper.NavigateTo(string.Format(RoutePattern, pageIndex));
                ChangePage(pageIndex);
            }
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
                if (!string.IsNullOrWhiteSpace(containerClass))
                {
                    builder.AddAttribute(++seq, "class", containerClass);
                }
            }
            builder.OpenElement(++seq, "a");
            if (!string.IsNullOrWhiteSpace(itemClass))
            {
                builder.AddAttribute(++seq, "class", itemClass);
            }
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
            builder.AddMarkupContent(++seq,pagerItemText);
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
}