using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System;

namespace Webdiyer.AspNetCore
{
    public partial class BlazorPager
    {
        [Inject]
        private IUriHelper UriHelper { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="TagName"]/*'/>
        [Parameter]
        private string TagName { get; set; } = "div";

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="PageSize"]/*'/>
        [Parameter]
        public int PageSize { get; private set; } = 10;

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="InitPageIndex"]/*'/>
        [Parameter]
        private int InitPageIndex { get; set; } = 1;

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="TotalItemCount"]/*'/>
        [Parameter]
        private int TotalItemCount { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="NumericPagerItemTextFormatString"]/*'/>
        [Parameter]
        private string NumericPagerItemTextFormatString { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="CurrentPagerItemTextFormatString"]/*'/>
        [Parameter]
        private string CurrentPagerItemTextFormatString { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="RoutePattern"]/*'/>
        [Parameter]
        private string RoutePattern { get; set; } = "{0}";

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="NumericPagerItemCount"]/*'/>
        [Parameter]
        private int NumericPagerItemCount { get; set; } = 10;

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="OnPageChanged"]/*'/>
        [Parameter]
        private Action<int> OnPageChanged { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="PagerItemCssClass"]/*'/>
        [Parameter]
        private string PagerItemCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="MorePagerItemCssClass"]/*'/>
        [Parameter]
        private string MorePagerItemCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="NumericPagerItemCssClass"]/*'/>
        [Parameter]
        private string NumericPagerItemCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="NavigationPagerItemCssClass"]/*'/>
        [Parameter]
        private string NavigationPagerItemCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="CurrentPagerItemCssClass"]/*'/>
        [Parameter]
        private string CurrentPagerItemCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="DisabledPagerItemCssClass"]/*'/>
        [Parameter]
        private string DisabledPagerItemCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="AutoHide"]/*'/>
        [Parameter]
        private bool AutoHide { get; set; } = true;
        
        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="ShowNumericPagerItems"]/*'/>
        [Parameter]
        private bool ShowNumericPagerItems { get; set; } = true;

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="ShowMorePagerItems"]/*'/>
        [Parameter]
        private bool ShowMorePagerItems { get; set; } = true;

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="ShowFirstLast"]/*'/>
        [Parameter]
        private bool ShowFirstLast { get; set; } = true;

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="ShowPrevNext"]/*'/>
        [Parameter]
        private bool ShowPrevNext { get; set; } = true;

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="FirstPageText"]/*'/>
        [Parameter]
        private string FirstPageText { get; set; } = "<<";

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="PrevPageText"]/*'/>
        [Parameter]
        private string PrevPageText { get; set; } = "<";

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="NextPageText"]/*'/>
        [Parameter]
        private string NextPageText { get; set; } = ">";

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="LastPageText"]/*'/>
        [Parameter]
        private string LastPageText { get; set; } = ">>";

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="MorePageText"]/*'/>
        [Parameter]
        private string MorePageText { get; set; } = "...";

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="PagerItemContainerTagName"]/*'/>
        [Parameter]
        private string PagerItemContainerTagName { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="PagerItemContainerCssClass"]/*'/>
        [Parameter]
        private string PagerItemContainerCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="NumericPagerItemContainerTagName"]/*'/>
        [Parameter]
        private string NumericPagerItemContainerTagName { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="NumericPagerItemContainerCssClass"]/*'/>
        [Parameter]
        private string NumericPagerItemContainerCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="CurrentPagerItemContainerTagName"]/*'/>
        [Parameter]
        private string CurrentPagerItemContainerTagName { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="CurrentPagerItemContainerCssClass"]/*'/>
        [Parameter]
        private string CurrentPagerItemContainerCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="NavigationPagerItemContainerTagName"]/*'/>
        [Parameter]
        private string NavigationPagerItemContainerTagName { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="NavigationPagerItemContainerCssClass"]/*'/>
        [Parameter]
        private string NavigationPagerItemContainerCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="MorePagerItemContainerTagName"]/*'/>
        [Parameter]
        private string MorePagerItemContainerTagName { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="MorePagerItemContainerCssClass"]/*'/>
        [Parameter]
        private string MorePagerItemContainerCssClass { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="DisabledPagerItemContainerTagName"]/*'/>
        [Parameter]
        private string DisabledPagerItemContainerTagName { get; set; }

        ///<include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="DisabledPagerItemContainerCssClass"]/*'/>
        [Parameter]
        private string DisabledPagerItemContainerCssClass { get; set; }

        /// <include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="RenderAfterBeginTag"]/*'/>
        [Parameter]
        private RenderFragment RenderAfterBeginTag { get; set; }

        /// <include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="RenderBeforeEndTag"]/*'/>
        [Parameter]
        private RenderFragment RenderBeforeEndTag { get; set; }

        /// <include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="CurrentPageIndex"]/*'/>
        public int CurrentPageIndex { get; private set; } = 1;

        /// <include file='docs/BlazorPagerDoc.xml' path='BlazorPagerDoc/Property[@name="TotalPageCount"]/*'/>
        public int TotalPageCount { get; private set; }

        private int startPageIndex { get; set; }

        private int endPageIndex { get; set; }
    }
}
