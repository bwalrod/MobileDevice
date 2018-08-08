﻿using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaDepartment
    {
        public MdaDepartment()
        {
            MdaDeviceAssignee = new HashSet<MdaDeviceAssignee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<MdaDeviceAssignee> MdaDeviceAssignee { get; set; }
    }
}
