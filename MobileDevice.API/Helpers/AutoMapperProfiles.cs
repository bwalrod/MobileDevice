using AutoMapper;
using System.Linq;
using MobileDevice.API.Controllers.Resources;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

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