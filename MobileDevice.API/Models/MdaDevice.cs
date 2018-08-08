using System;
using System.Collections.Generic;

namespace MobileDevice.API.Models
{
    public partial class MdaDevice
    {
        public MdaDevice()
        {
            MdaDeviceAssignment = new HashSet<MdaDeviceAssignment>();
            MdaDeviceAttribute = new HashSet<MdaDeviceAttribute>();
            MdaDeviceDate = new HashSet<MdaDeviceDate>();
            MdaDeviceNote = new HashSet<MdaDeviceNote>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? SimId { get; set; }
        public string SerialNumber { get; set; }
        public string Esn { get; set; }
        public string Os { get; set; }
        public int DeviceStatusId { get; set; }
        public byte Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public MdaDeviceStatus DeviceStatus { get; set; }
        public MdaProduct Product { get; set; }
        public MdaSimCard Sim { get; set; }
        public ICollection<MdaDeviceAssignment> MdaDeviceAssignment { get; set; }
        public ICollection<MdaDeviceAttribute> MdaDeviceAttribute { get; set; }
        public ICollection<MdaDeviceDate> MdaDeviceDate { get; set; }
        public ICollection<MdaDeviceNote> MdaDeviceNote { get; set; }
    }
}
