using AutoMapper;
using MobileDevice.API.Controllers.Resources;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AssigneeQueryResource, MdaAssigneeQuery>();

            CreateMap<DeviceQueryResource, MdaDeviceQuery>();
        }
    }
}