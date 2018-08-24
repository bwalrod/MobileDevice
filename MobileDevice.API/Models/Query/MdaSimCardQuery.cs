using MobileDevice.API.Extensions;

namespace MobileDevice.API.Models.Query
{
    public class MdaSimCardQuery: IQueryObject
    {
        public string Iccid { get; set; }
        public string PhoneNumber { get; set; }
        public string Carrier { get; set; }        
        public byte Active { get; set; }        
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }     

        public MdaSimCardQuery()
        {
            Page = 1;
            PageSize = 5;
        }   
    }
}