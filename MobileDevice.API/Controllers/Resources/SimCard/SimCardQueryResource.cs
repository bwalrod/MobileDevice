namespace MobileDevice.API.Controllers.Resources.SimCard
{
    public class SimCardQueryResource
    {
        public string Iccid { get; set; }
        public string PhoneNumber { get; set; }
        public string Carrier { get; set; }        
        public byte Active { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }          
    }
}