using System;
using MobileDevice.API.Extensions;

namespace MobileDevice.API.Models.Query
{
    public class MdaDeviceDateTypeQuery : IQueryObject
    {
        public string Name { get; set; }        
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }        

        public MdaDeviceDateTypeQuery()
        {
            Page = 1;
            PageSize = 5;
        }
    }
}