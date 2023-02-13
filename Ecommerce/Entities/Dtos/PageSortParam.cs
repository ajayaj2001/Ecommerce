namespace Customer.Entities.Dtos
{
    public class PageSortParam
    {
        ///<summary>
        ///count of Customer to display
        ///</summary>
        public int Size { get; set; } = 10;  //default page size

        ///<summary>
        ///page number of display all address book
        ///</summary>
        public int PageNo { get; set; } = 1;

        ///<summary>
        ///sort by which field
        ///</summary>
        public string SortBy { get; set; } = null;

        ///<summary>
        ///sort in which direction asc or desc
        ///</summary>
        public SortDirection SortOrder { get; set; }
    }

    public enum SortDirection
    {
        ASC = 0,   //default as ascending
        DESC
    }
}
