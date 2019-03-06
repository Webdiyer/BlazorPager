using System.Collections.Generic;
using X.PagedList;

namespace BlazorDemo.Shared
{
    public class PagedListData<T>
    {
        public IEnumerable<T> Items { get; set; }

        public PagedListMetaData MetaData { get; set; }
    }
}
