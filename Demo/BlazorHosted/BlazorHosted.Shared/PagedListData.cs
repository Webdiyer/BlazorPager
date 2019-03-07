using System.Collections.Generic;
using X.PagedList;

namespace BlazorHosted.Shared
{
    public class PagedListData<T>
    {
        public IEnumerable<T> Items { get; set; }

        public PagedListMetaData MetaData { get; set; }
    }
}
