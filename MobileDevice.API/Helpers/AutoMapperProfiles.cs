using AutoMapper;
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

            CreateMap<DeviceUpdateResource, MdaDevice>();

            CreateMap<AssigneeUpdateResource, MdaDeviceAssignee>();

            CreateMap<AssigneeAddResource, MdaDeviceAssignee>();
        }
    }
}