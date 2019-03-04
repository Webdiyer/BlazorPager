namespace Webdiyer.AspNetCore
{
    public partial class BlazorPager
    {
        internal enum PagerItemType : byte
        {
            Navigation,
            More,
            Number,
            Current,
            Disabled
        }
    }
}
