# BlazorPager  | [English](README.md)

BlazorPager 是一个应用于 Razor Components（即Blazor服务器端） 项目的分页组件，支持Blazor 0.8.0+，目前还在逐步完善中！

[![License](https://img.shields.io/badge/license-MIT-green.svg)](https://github.com/Webdiyer/BlazorPager/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/BlazorPager.svg)](https://www.nuget.org/packages/BlazorPager/)

<img src="/Demo/RazorComponents/wwwroot/images/blazorpager.gif" alt="blazorpager demo"/>

---


## 开发运行先决条件 ##

1. [Visual Studio 2019 RC](https://visualstudio.microsoft.com/)
2. [.Net Core 3.0 SDK preview 3](https://dotnet.microsoft.com/download/dotnet-core/3.0)

## Nuget包 ##

Nuget包可从 https://www.nuget.org/packages/BlazorPager/ 下载。

## 安装 ##

可以通过VS的Nuget包管理器界面中搜索并添加 BlazorPager 引用，也可以在VS的Nuget包管理器控制台中输入如下命令安装：

```
Install-Package BlazorPager
```

或者通过.net core命令行安装：
```
dotnet add package BlazorPager
```

## 使用 ##

1. 注册 TagHelper：

```
@addTagHelper *,Webdiyer.AspNetCore.BlazorPager
```

2. 如果需要在服务器端代码中引用 BlazorPager，请引入如下命名空间：

```
@using Webdiyer.AspNetCore;
```

3. 一个最简单的 BlazorPager 使用示例（不获取和显示数据）：


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

更多使用示例，请查看Demo文件夹下的示例项目。

---

## 属性： ##

---
##### TagName

分页控件html容器标签名，默认为div。 



---
##### PageSize

获取或设置每页显示的记录数。



---
##### InitPageIndex

获取或设置分页组件初始化时的当前页索引。 



---
##### TotalItemCount

获取或设置要分页的数据总数。



---
##### NumericPagerItemTextFormatString

获取或设置数字页索引文本格式化字符串。 



---
##### CurrentPagerItemTextFormatString

获取或设置当前页数字页索引文本格式化字符串。 



---
##### RoutePattern

获取或设置用于生成分页链接的路由模板，该值必须包含一个“{0}”占位符。 



---
##### NumericPagerItemCount

获取或设置要显示的数字页索引分页按钮的数量。 



---
##### OnPageChanged

获取或设置分页事件处理程序。 



---
##### PagerItemCssClass

获取或设置要应用于所有分页按钮上的CSS样式类名。 



---
##### MorePagerItemCssClass

获取或设置应用于更多页分页导航按钮的CSS样式类。 



---
##### NumericPagerItemCssClass

获取或设置要应用于数字页索引分页按钮上的CSS样式类名。 



---
##### NavigationPagerItemCssClass

获取或设置应用于分页导航按钮的CSS样式类。 



---
##### CurrentPagerItemCssClass

获取或设置要应用于当前页索引分页按钮上的CSS样式类名。 



---
##### DisabledPagerItemCssClass

获取或设置应用于已禁用的分页元素的CSS样式类。 



---
##### AutoHide

获取或设置一个值，该值指示当总页数只有一页时，是否自动隐藏分页组件。 



---
##### ShowNumericPagerItems

获取或设置一个值，该值指示是否显示数字页索引分页按钮。 



---
##### ShowMorePagerItems

获取或设置一个值，该值指示是否显示更多页分页导航按钮。 



---
##### ShowFirstLast

获取或设置一个值，该值指示是否显示首页和尾页分页导航按钮。 



---
##### ShowPrevNext

获取或设置一个值，该值指示是否显示上一页和下一页分页导航按钮。 



---
##### FirstPageText

获取或设置首页分页按钮上显示的文本。 



---
##### PrevPageText

获取或设置上一页分页按钮上显示的文本。 



---
##### NextPageText

获取或设置下一页分页按钮上显示的文本。 



---
##### LastPageText

获取或设置最后一页分页按钮上显示的文本。 



---
##### MorePageText

获取或设置更多页按钮上显示的文本。 



---
##### PagerItemContainerTagName

获取或设置分页元素的父元素的标签名，默认值为null，意即没有父元素。 



---
##### PagerItemContainerCssClass

获取或设置要应用于分页元素父元素的CSS样式类。 



---
##### NumericPagerItemContainerTagName

获取或设置数字页索引分页元素的父元素的标签名，默认值为null，意即没有父元素。 



---
##### NumericPagerItemContainerCssClass

获取或设置要应用于数字页索引分页元素父元素的CSS样式类。 



---
##### CurrentPagerItemContainerTagName

获取或设置当前页分页元素的父元素的标签名，默认值为null，意即没有父元素。 



---
##### CurrentPagerItemContainerCssClass

获取或设置要应用于当前页分页元素父元素的CSS样式类。 



---
##### NavigationPagerItemContainerTagName

获取或设置首页、尾页、上页和下页四个分页元素的父元素的标签名，默认值为null，意即没有父元素。 



---
##### NavigationPagerItemContainerCssClass

获取或设置要应用于首页、尾页、上页和下页四个分页元素父元素的CSS样式类。 



---
##### MorePagerItemContainerTagName

获取或设置更多页分页元素的父元素的标签名，默认值为null，意即没有父元素。 



---
##### MorePagerItemContainerCssClass

获取或设置要应用于更多页分页元素父元素的CSS样式类。 



---
##### DisabledPagerItemContainerTagName

获取或设置被禁用的分页元素的父元素的标签名，默认值为null，意即没有父元素。 



> 若当前页为第一页时，则首页和上一页分页导航按钮将被禁用；若当前页为最后一页，则尾页和下一页分页导航按钮将被禁用； 



---
##### DisabledPagerItemContainerCssClass

获取或设置要应用于被禁用的分页元素父元素的CSS样式类。 



---
##### RenderAfterBeginTag

获取或设置一个用于在分页组件开始标签后写入内容的委托。 



---
##### RenderBeforeEndTag

获取或设置一个用于在分页组件结束标签前写入内容的委托。 



---
##### CurrentPageIndex

获取当前页索引。



---
##### TotalPageCount

获取要分页数据的总页数。



---
## 方法： ##

---

##### GoToPage(System.Int32)

跳转到指定页。 

|Name | Description |
|-----|------|
|pageIndex: |要跳转到的页索引，从1开始。|


---
