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
using MobileDevice.API.Controllers.Resources.DeviceNote;
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

            CreateMap<MdaDevice, DeviceForList>()
                .ForMember(l => l.ProductModelId, opt => opt.MapFrom(s => s.Product.ProductModelId))
                .ForMember(l => l.ProductModelName, opt => opt.MapFrom(s => s.Product.ProductModel.Name))
                .ForMember(l => l.ProductCapacityName, opt => opt.MapFrom(s => s.Product.ProductCapacity.Name))
                .ForMember(l => l.ProductManufacturerName, opt => opt.MapFrom(s => s.Product.ProductModel.ProductManufacturer.Name))
                .ForMember(l => l.AssignmentType, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active ==1).AssignmentType))
                .ForMember(l => l.AssigneeId, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active ==1).AssigneeId))
                .ForMember(l => l.AssingeeLastName, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.LastName))
                .ForMember(l => l.AssigneeFirstName, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.FirstName))
                .ForMember(l => l.AssigneeDepartmentName, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.Department.Name))
                .ForMember(l => l.SimId, opt => opt.MapFrom(s => s.SimId))
                .ForMember(l => l.SimIccid, opt => opt.MapFrom(s => s.Sim.Iccid))
                .ForMember(l => l.SimPhoneNumber, opt => opt.MapFrom(s => s.Sim.PhoneNumber))
                .ForMember(l => l.SimCarrier, opt => opt.MapFrom(s => s.Sim.Carrier))                
            ;

            CreateMap<AssigneeSaveResource, MdaDeviceAssignee>();

            CreateMap<MdaDeviceAssignee, AssigneeForList>();

            CreateMap<DeviceAssignmentQueryResource, MdaAssignmentQuery>();

            CreateMap<DeviceAssignmentSaveResource, MdaDeviceAssignment>();

            CreateMap<MdaDeviceAssignment, DeviceAssignmentForList>()
                // .ForMember(l => l.SimCard, opt => opt.MapFrom(s => s.Device.Sim))
                .ForMember(l => l.DeviceProductModelId, opt => opt.MapFrom(s => s.Device.Product.ProductModelId))
                .ForMember(l => l.DeviceProductModelName, opt => opt.MapFrom(s => s.Device.Product.ProductModel.Name))
                .ForMember(l => l.DeviceProductCapacityName, opt => opt.MapFrom(s => s.Device.Product.ProductCapacity.Name))
                .ForMember(l => l.DeviceProductManufacturerName, opt => opt.MapFrom(s => s.Device.Product.ProductModel.ProductManufacturer.Name))
                .ForMember(l => l.AssingeeLastName, opt => opt.MapFrom(s => s.MdaDeviceAssignee.LastName))
                .ForMember(l => l.AssigneeFirstName, opt => opt.MapFrom(s => s.MdaDeviceAssignee.FirstName))
                .ForMember(l => l.AssigneeDepartmentName, opt => opt.MapFrom(s => s.MdaDeviceAssignee.Department.Name))
                ;

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

            CreateMap<MdaDepartment, DepartmentForList>()
                .ForMember(l => l.AssigneeCount, opt => opt.MapFrom(s => s.MdaDeviceAssignee.Count()));

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

            CreateMap<MdaProductModel, ProductModelForList>();

            CreateMap<MdaDeviceNote, DeviceNoteForList>();

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