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
using MobileDevice.API.Controllers.Resources.Department;
using MobileDevice.API.Controllers.Resources.SimCard;
using MobileDevice.API.Controllers.Resources.Product;
using MobileDevice.API.Controllers.Resources.ProductCapacity;
using MobileDevice.API.Controllers.Resources.ProductManufacturer;
using MobileDevice.API.Controllers.Resources.ProductModel;
using MobileDevice.API.Controllers.Resources.ProductType;
using MobileDevice.API.Controllers.Resources.DeviceAssignment;

namespace MobileDevice.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AssigneeQueryResource, MdaAssigneeQuery>();

            CreateMap<DeviceQueryResource, MdaDeviceQuery>();
            
            CreateMap<AppUserQueryResource, MdaAppUserQuery>();

            CreateMap<AppUserSaveResource, MdaAppUser>();

            CreateMap<MdaAppUser, AppUserForReturnResource>();

            // CreateMap<DeviceAddResource, MdaDevice>();

            // CreateMap<MdaDeviceDate, MdaDeviceDate>();

            CreateMap<DeviceSaveResource, MdaDevice>()
            .ForMember(d => d.MdaDeviceDate, opt => opt.Ignore())
            // .AfterMap((dr, d) => AddOrUpdateDate(dr,d))
            ;

            CreateMap<AssigneeSaveResource, MdaDeviceAssignee>();

            CreateMap<DeviceAssignmentQueryResource, MdaAssignmentQuery>();

            CreateMap<DeviceAssignmentSaveResource, MdaDeviceAssignment>();

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

            CreateMap<DepartmentQueryResource, MdaDepartmentQuery>();

            CreateMap<DepartmentSaveResource, MdaDepartment>();

            CreateMap<SimCardQueryResource, MdaSimCardQuery>();

            CreateMap<SimCardSaveResource, MdaSimCard>();

            CreateMap<ProductTypeQueryResource, MdaProductTypeQuery>();

            CreateMap<ProductTypeSaveResource, MdaProductType>();

            CreateMap<ProductQueryResource, MdaProductQuery>();
            CreateMap<ProductSaveResource, MdaProduct>();
            CreateMap<ProductCapacityQueryResource, MdaProductCapacityQuery>();
            CreateMap<ProductCapacitySaveResource, MdaProductCapacity>();
            CreateMap<ProductManufacturerQueryResource, MdaProductManufacturerQuery>();
            CreateMap<ProductManufacturerSaveResource, MdaProductManufacturer>();
            CreateMap<ProductModelQueryResource, MdaProductModelQuery>();
            CreateMap<ProductModelSaveResource, MdaProductModel>();

        }

        private void AddOrUpdateDate(DeviceSaveResource dto, MdaDevice device)
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