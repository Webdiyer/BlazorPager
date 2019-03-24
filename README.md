# BlazorPager | [简体中文](README_ZH-CN.md)

BlazorPager is a Pagination component for Blazor and Razor Components application, it supports Blazor version 0.8.0+ and still under active development.

[![License](https://img.shields.io/badge/license-MIT-green.svg)](https://github.com/Webdiyer/BlazorPager/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/BlazorPager.svg)](https://www.nuget.org/packages/BlazorPager/)


<img src="/Demo/RazorComponents/wwwroot/images/blazorpager.gif" alt="blazorpager demo"/>

---
## Prerequisites: ##

1. [Visual Studio 2019 RC](https://visualstudio.microsoft.com/)
2. [.Net Core 3.0 SDK preview 3](https://dotnet.microsoft.com/download/dotnet-core/3.0)

## Nuget Package: ##
Nuget packages can be found at: https://www.nuget.org/packages/BlazorPager/

## Installation: ##

Install via Visual Studio Package manager console:

```
Install-Package BlazorPager
```
or via .NET Core command-line interface (CLI) tools:
```
dotnet add package BlazorPager
```
## Usage ##

1. Register tag helper:

```
@addTagHelper *,Webdiyer.AspNetCore.BlazorPager
```

2. Add namespace If you need to reference BlazorPager in your server side code:

```
@using Webdiyer.AspNetCore;
```

3. A simple BlazorPager pagination sample without displaying any data actually:


```
@page "/"
@page "/index/{page:int}"
@addTagHelper *,Webdiyer.AspNetCore.BlazorPager

<BlazorPager TotalItemCount="188" InitPageIndex="@Page" RoutePattern="index/{0}"></BlazorPager>

@functions{
    [Parameter]
    private int Page { get; set; }
    
}
```

---

## Properties: ##

---
##### TagName

 Gets or sets the HTML tag name for the container element of the pager, default value is div. 



---
##### PageSize

Gets or sets the number of data items that are displayed for each page of data.



---
##### InitPageIndex

 Gets or sets the current page index value when the pager component initialized first time. 



---
##### TotalItemCount

Gets or sets the total number of data items that are available for paging.



---
##### NumericPagerItemTextFormatString

 Gets or sets the format string for all numeric pager items. 



---
##### CurrentPagerItemTextFormatString

 Gets or sets the format string for the current page number. 



---
##### RoutePattern

 Gets or sets the route template that the pager component used to generate pagination links, it must contains a placeholder "{0}" for page index. 



---
##### NumericPagerItemCount

 Gets or sets the maximum number of the numeric pager items to be displayed. 



---
##### OnPageChanged

 Gets or sets an action that been invoked when page index changed. 



---
##### PagerItemCssClass

 Gets or sets the css class to be applied to every pager items. 



---
##### MorePagerItemCssClass

 Gets or sets the css class to be applied to the more pager items. 



---
##### NumericPagerItemCssClass

 Gets or sets the css class to be applied to the numeric pager items. 



---
##### NavigationPagerItemCssClass

 Gets or sets the css class to be applied to the first, previous, next and last page pager items. 



---
##### CurrentPagerItemCssClass

 Gets or sets the css class to be applied to the current page pager item. 



---
##### DisabledPagerItemCssClass

 Gets or sets the css class to be applied to the disabled navigation pager items. 



---
##### AutoHide

 Gets or sets a value indicating whether MvcPager should be hidden if there's only one page of data. 



---
##### ShowNumericPagerItems

 Gets or sets a value indicating whether the numeric pager items should be displayed. 



---
##### ShowMorePagerItems

 Gets or sets a value indicating whether the more pager items should be displayed. 



---
##### ShowFirstLast

 Gets or sets a value indicating whether the first page and the last page pager items should be displayed. 



---
##### ShowPrevNext

 Gets or sets a value indicating whether the previous page and the next page pager items should be displayed. 



---
##### FirstPageText

 Gets or sets the text displayed for the first page button. 



---
##### PrevPageText

 Gets or sets the text displayed for the previous page pager item. 



---
##### NextPageText

 Gets or sets the text displayed for the next page pager item. 



---
##### LastPageText

 Gets or sets the text displayed for the last page pager item. 



---
##### MorePageText

 Gets or sets the text displayed for the more pager items. 



---
##### PagerItemContainerTagName

 Gets or sets the tag name of the container element that wraps every pager items, default value is null. 



---
##### PagerItemContainerCssClass

 Gets or sets the css class to be applied to the container element of pager items. 



---
##### NumericPagerItemContainerTagName

 Gets or sets the tag name of the container element that wraps every numeric pager items, default value is null. 



---
##### NumericPagerItemContainerCssClass

 Gets or sets the css class to be applied to the container element of numeric pager items. 



---
##### CurrentPagerItemContainerTagName

 Gets or sets the tag name of the container element that wraps current pager item, default value is null. 



---
##### CurrentPagerItemContainerCssClass

 Gets or sets the css class to be applied to the container element of the current pager item. 



---
##### NavigationPagerItemContainerTagName

 Gets or sets the tag name of the container element that wraps first,last,next and previous pager items, default value is null. 



---
##### NavigationPagerItemContainerCssClass

 Gets or sets the css class to be applied to the container element of first,last,next and previous pager items. 



---
##### MorePagerItemContainerTagName

 Gets or sets the tag name of the container element that wraps more pager items, default value is null. 



---
##### MorePagerItemContainerCssClass

 Gets or sets the css class to be applied to the container element of more pager items. 



---
##### DisabledPagerItemContainerTagName

 Gets or sets the tag name of the container element that wraps disabled pager items, default value is null. 



> When the current page index is 1, the first and the previous navigation pager items will be disabled; When the current page index is the last page index, then the next and the last navigation pager items will be disabled; 



---
##### DisabledPagerItemContainerCssClass

 Gets or sets the css class to be applied to the container element of the disabled pager item. 



---
##### RenderAfterBeginTag

 Gets or sets the delegate that render the content after the begin tag of the pager component. 



---
##### RenderBeforeEndTag

 Gets or sets the delegate that render the content before the end tag of the pager component. 



---
##### CurrentPageIndex

Gets the current page index.



---
##### TotalPageCount

Gets the total number of pages that are available for paging.



---

## Methods: ##

---
##### GoToPage(System.Int32)

 Go to the specified page index. 

|Name | Description |
|-----|------|
|pageIndex: |1-based number of the page to go to.|


---
