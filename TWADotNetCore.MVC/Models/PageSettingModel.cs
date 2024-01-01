namespace TWADotNetCore.MVC.Models
{
    public class PageSettingModel
    {
        public PageSettingModel()
        {
        }

        public PageSettingModel(int pageNo,int pageSize,int pageCount,string pageUrl)
        {
            this.PageNo = pageNo;
            this.PageSize = pageSize;   
            this.PageCount = pageCount;
            this.PageUrl = pageUrl;
        }

        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public string PageUrl { get; set; }
    }
}
