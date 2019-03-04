using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System;

namespace Webdiyer.AspNetCore
{
    public partial class BlazorPager
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Parameter]
        private string TagName { get; set; } = "div";

        [Parameter]
        public int PageSize { get; private set; } = 10;

        [Parameter]
        private int TotalItemCount { get; set; }

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


        public int TotalPageCount { get; set; }

        private int startPageIndex { get; set; }

        private int endPageIndex { get; set; }
    }
}
