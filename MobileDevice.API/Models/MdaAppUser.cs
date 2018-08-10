using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaAppUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int AccessLevel { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
