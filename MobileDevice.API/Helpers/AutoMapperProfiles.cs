using AutoMapper;
using System.Linq;
using MobileDevice.API.Controllers.Resources.Assignee;
using MobileDevice.API.Controllers.Resources.AppUser;
using MobileDevice.API.Controllers.Resources.Device;
using MobileDevice.API.Controllers.Resources.DeviceDate;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;
using MobileDevice.API.Controllers.Resources.DeviceAttribute;
using MobileDevice.API.Controllers.Resources.DeviceDateType;
using MobileDevice.API.Controllers.Resources.DeviceAttributeType;
using MobileDevice.API.Controllers.Resources.DeviceStatus;

namespace MobileDevice.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AssigneeQueryResource, MdaAssigneeQuery>();

            CreateMap<DeviceQueryResource, MdaDeviceQuery>();
            
            CreateMap<AppUserQueryResource, MdaAppUserQuery>();

            CreateMap<AppUserAddResource, MdaAppUser>();

            CreateMap<MdaAppUser, AppUserForReturnResource>();

            CreateMap<AppUserUpdateResource, MdaAppUser>();

            CreateMap<DeviceAddResource, MdaDevice>();

            // CreateMap<MdaDeviceDate, MdaDeviceDate>();

            CreateMap<DeviceUpdateResource, MdaDevice>()
            .ForMember(d => d.MdaDeviceDate, opt => opt.Ignore())
            // .AfterMap((dr, d) => AddOrUpdateDate(dr,d))
            ;

            CreateMap<AssigneeUpdateResource, MdaDeviceAssignee>();

            CreateMap<AssigneeAddResource, MdaDeviceAssignee>();

            CreateMap<DeviceDateQueryResource, MdaDeviceDateQuery>();

            CreateMap<DeviceDateSaveResource, MdaDeviceDate>();

            CreateMap<DeviceAttributeQueryResource, MdaDeviceAttributeQuery>();

            CreateMap<DeviceAttributeSaveResource, MdaDeviceAttribute>();

            CreateMap<DeviceDateTypeSaveResource, MdaDeviceDateType>();

            CreateMap<DeviceDateTypeQueryResource, MdaDeviceDateTypeQuery>();

            CreateMap<DeviceAttributeTypeQueryResource, MdaDeviceAttributeTypeQuery>();

            CreateMap<DeviceAttributeTypeSaveResource, MdaDeviceAttributeType>();

            CreateMap<DeviceStatusQueryResource, MdaDeviceStatusQuery>();

            CreateMap<DeviceStatusSaveResource, MdaDeviceStatus>();
        }

        private void AddOrUpdateDate(DeviceUpdateResource dto, MdaDevice device)
        {
              foreach (var dateDTO in dto.MdaDeviceDate)  
              {
                  if (dateDTO.Id == 0)
                  {
                      device.MdaDeviceDate.Add(Mapper.Map<MdaDeviceDate>(dateDTO));
                  }
                  else
                  {
                      Mapper.Map(dateDTO, device.MdaDeviceDate.SingleOrDefault(m => m.Id == dateDTO.Id));
                  }
              }
        }
    }
}