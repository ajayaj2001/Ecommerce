namespace Product.Entities.Dtos
{
    public class PageSortResult
    {
        ///<summary>
        ///total Product found in db
        ///</summary>
        public int TotalCount { get; set; } = 0;

        ///<summary>
        ///total page available of display all address book
        ///</summary>
        public int TotalPages { get; set; } = 1;

        ///<summary>
        ///previous page number of address book 
        ///</summary>
        public int? PreviousPage { get; set; }

        ///<summary>
        ///next page number of address book 
        ///</summary>
        public int? NextPage { get; set; }

        ///<summary>
        ///first row number of address book 
        ///</summary>
        public int FirstRowOnPage { get; set; }

        ///<summary>
        ///last row number of address book 
        ///</summary>
        public int LastRowOnPage { get; set; }
    }
}
