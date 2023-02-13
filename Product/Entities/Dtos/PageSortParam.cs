namespace Product.Entities.Dtos
{
    public class PageSortParam
    {
        ///<summary>
        ///count of Product to display
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
        public string SortOrder { get; set; }

        ///<summary>
        ///sort in by category
        ///</summary>
        public string Category { get; set; }
    }
}
