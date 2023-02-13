using System.Collections.Generic;
using System;
using System.Linq;
using System.Data;
using Product.Entities.Models;
using Product.Entities.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace Product.Helper
{
    ///<summary>
    ///pagination used for applying filters
    ///</summary>
    public class PaginationHandler<T> : List<ProductDetail>
    {
        public PageSortParam Param { get; }
        public PageSortResult Result { get; }

        public PaginationHandler(PageSortParam param)
        {
            Param = param;
            Result = new PageSortResult();
        }

        public List<ProductDetail> GetData(IEnumerable<ProductDetail> query)
        {
            if(string.IsNullOrEmpty(Param.Category))
            query.Any(product => product.CategoryId.ToString() == Param.Category);

            //get the total count
            Result.TotalCount = query.Count();
            //find the number of pages
            Result.TotalPages = (int)Math.Ceiling(Result.TotalCount / (double)Param.Size);
            //find previous and next page number
            if (Param.PageNo - 1 > 0)
                Result.PreviousPage = Param.PageNo - 1;
            if (Param.PageNo + 1 <= Result.TotalPages)
                Result.NextPage = Param.PageNo + 1;
            //find first row and last row on the page
            if (Result.TotalCount == 0)  //if no record found
                Result.FirstRowOnPage = Result.LastRowOnPage = 0;
            else
            {
                Result.FirstRowOnPage = (Param.PageNo - 1) * Param.Size + 1;
                Result.LastRowOnPage =
                  Math.Min(Param.PageNo * Param.Size, Result.TotalCount);
            }
                if (Param.SortOrder == "ASC")
                {
                    query = query.OrderBy(e => e.GetType().GetProperty(Param.SortBy).GetValue(e)).ToList();
                }
                else
                {
                    query = query.OrderByDescending(e => e.GetType().GetProperty(Param.SortBy).GetValue(e)).ToList();
                }

            List<ProductDetail> list = query.Skip((Param.PageNo - 1) *
                           Param.Size).Take(Param.Size).ToList();
            AddRange(list);

            return list;

        }
    }
}
