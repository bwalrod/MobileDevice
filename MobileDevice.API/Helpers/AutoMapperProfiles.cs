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
            /*  MdaAppUser */
            CreateMap<AppUserQueryResource, MdaAppUserQuery>();

            CreateMap<AppUserSaveResource, MdaAppUser>();

            CreateMap<MdaAppUser, AppUserForReturnResource>();

            /*  MdaDepartment */

            CreateMap<DepartmentQueryResource, MdaDepartmentQuery>();

            CreateMap<DepartmentSaveResource, MdaDepartment>();

            CreateMap<MdaDepartment, DepartmentForList>()
                .ForMember(l => l.AssigneeCount, opt => opt.MapFrom(s => s.MdaDeviceAssignee.Count()));

            /*  MdaDevice */
            CreateMap<DeviceQueryResource, MdaDeviceQuery>();

            CreateMap<DeviceSaveResource, MdaDevice>()
            .ForMember(d => d.MdaDeviceDate, opt => opt.Ignore());

            CreateMap<MdaDevice, DeviceForList>()
                .ForMember(l => l.PartNum, opt => opt.MapFrom(s => s.Product.PartNum))
                .ForMember(l => l.ProductModelId, opt => opt.MapFrom(s => s.Product.ProductModelId))
                .ForMember(l => l.ProductModelName, opt => opt.MapFrom(s => s.Product.ProductModel.Name))
                .ForMember(l => l.ProductCapacityId, opt => opt.MapFrom(s => s.Product.ProductCapacityId))
                .ForMember(l => l.ProductCapacityName, opt => opt.MapFrom(s => s.Product.ProductCapacity.Name))
                .ForMember(l => l.ProductManufacturerName, opt => opt.MapFrom(s => s.Product.ProductModel.ProductManufacturer.Name))
                .ForMember(l => l.ProductTypeId, opt => opt.MapFrom(s => s.Product.ProductModel.ProductType.Id))
                .ForMember(l => l.ProductTypeName, opt => opt.MapFrom(s => s.Product.ProductModel.ProductType.Name))
                .ForMember(l => l.AssignmentType, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active ==1).AssignmentType))
                .ForMember(l => l.AssigneeId, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active ==1).AssigneeId))
                .ForMember(l => l.assigneeLastName, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.LastName))
                .ForMember(l => l.AssigneeFirstName, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.FirstName))
                .ForMember(l => l.AssigneeDepartmentId, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.DepartmentId))
                .ForMember(l => l.AssigneeDepartmentName, opt => opt.MapFrom(s => s.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.Department.Name))
                .ForMember(l => l.SimId, opt => opt.MapFrom(s => s.SimId))
                .ForMember(l => l.SimIccid, opt => opt.MapFrom(s => s.Sim.Iccid))
                .ForMember(l => l.SimPhoneNumber, opt => opt.MapFrom(s => s.Sim.PhoneNumber))
                .ForMember(l => l.SimCarrier, opt => opt.MapFrom(s => s.Sim.Carrier));                

            /* MdaDeviceAssignee */
            CreateMap<AssigneeQueryResource, MdaAssigneeQuery>();

            CreateMap<AssigneeSaveResource, MdaDeviceAssignee>();

            CreateMap<MdaDeviceAssignee, AssigneeForList>()
                .ForMember(l => l.AssignmentCount, opt => opt.MapFrom(s => s.MdaDeviceAssignments.Count()));     

            CreateMap<MdaDeviceAssignee, AssigneeForLookup>()
                .ForMember(l => l.FullNameLF, opt => opt.MapFrom(s => string.Format("{0}, {1}",s.LastName,s.FirstName)));

            /*  MdaDeviceAssignment */

            CreateMap<DeviceAssignmentQueryResource, MdaAssignmentQuery>();

            CreateMap<DeviceAssignmentSaveResource, MdaDeviceAssignment>();

            CreateMap<MdaDeviceAssignment, DeviceAssignmentForList>()
                // .ForMember(l => l.SimCard, opt => opt.MapFrom(s => s.Device.Sim))
                .ForMember(l => l.DeviceProductModelId, opt => opt.MapFrom(s => s.Device.Product.ProductModelId))
                .ForMember(l => l.DeviceProductModelName, opt => opt.MapFrom(s => s.Device.Product.ProductModel.Name))
                .ForMember(l => l.DeviceProductCapacityName, opt => opt.MapFrom(s => s.Device.Product.ProductCapacity.Name))
                .ForMember(l => l.DeviceProductManufacturerName, opt => opt.MapFrom(s => s.Device.Product.ProductModel.ProductManufacturer.Name))
                .ForMember(l => l.assigneeLastName, opt => opt.MapFrom(s => s.MdaDeviceAssignee.LastName))
                .ForMember(l => l.AssigneeFirstName, opt => opt.MapFrom(s => s.MdaDeviceAssignee.FirstName))
                .ForMember(l => l.AssigneeDepartmentName, opt => opt.MapFrom(s => s.MdaDeviceAssignee.Department.Name));

            /*  MdaDeviceAttribute */

            CreateMap<DeviceAttributeQueryResource, MdaDeviceAttributeQuery>();

            CreateMap<DeviceAttributeSaveResource, MdaDeviceAttribute>();

            CreateMap<MdaDeviceAttribute, DeviceAttributeForList>();

            /*  MdaDeviceAttributeType */

            CreateMap<DeviceAttributeTypeQueryResource, MdaDeviceAttributeTypeQuery>();

            CreateMap<DeviceAttributeTypeSaveResource, MdaDeviceAttributeType>(); 

            CreateMap<MdaDeviceAttributeType, DeviceAttributeTypeForList>()
                .ForMember(l => l.DeviceAttributeCount, opt => opt.MapFrom(s => s.MdaDeviceAttribute.Count()));           

            /*  MdaDeviceDate */

            CreateMap<DeviceDateQueryResource, MdaDeviceDateQuery>();

            CreateMap<DeviceDateSaveResource, MdaDeviceDate>();

            CreateMap<MdaDeviceDate, DeviceDateForList>();

            /*  MdaDeviceDateType */

            CreateMap<DeviceDateTypeSaveResource, MdaDeviceDateType>();

            CreateMap<DeviceDateTypeQueryResource, MdaDeviceDateTypeQuery>();

            CreateMap<MdaDeviceDateType, DeviceDateTypeForList>()
                .ForMember(l => l.DeviceDateCount, opt => opt.MapFrom(s => s.MdaDeviceDate.Count()));

            /*  MdaDeviceNote */

            CreateMap<DeviceNoteSaveResource, MdaDeviceNote>();

            CreateMap<MdaDeviceNote, DeviceNoteForList>()
                .ForMember(n => n.SerialNumber, opt => opt.MapFrom(s => s.Device.SerialNumber))
                .ForMember(n => n.Esn, opt => opt.MapFrom(s => s.Device.Esn))
                .ForMember(n => n.Os, opt => opt.MapFrom(s => s.Device.Os))
                .ForMember(n => n.ProductId, opt => opt.MapFrom(s => s.Device.ProductId))
                .ForMember(l => l.ProductModelId, opt => opt.MapFrom(s => s.Device.Product.ProductModelId))
                .ForMember(l => l.ProductModelName, opt => opt.MapFrom(s => s.Device.Product.ProductModel.Name))
                .ForMember(l => l.ProductCapacityName, opt => opt.MapFrom(s => s.Device.Product.ProductCapacity.Name))
                .ForMember(l => l.ProductManufacturerName, opt => opt.MapFrom(s => s.Device.Product.ProductModel.ProductManufacturer.Name))
                .ForMember(l => l.AssignmentType, opt => opt.MapFrom(s => s.Device.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active ==1).AssignmentType))
                .ForMember(l => l.AssigneeId, opt => opt.MapFrom(s => s.Device.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active ==1).AssigneeId))
                .ForMember(l => l.assigneeLastName, opt => opt.MapFrom(s => s.Device.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.LastName))
                .ForMember(l => l.AssigneeFirstName, opt => opt.MapFrom(s => s.Device.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.FirstName))
                .ForMember(l => l.AssigneeDepartmentName, opt => opt.MapFrom(s => s.Device.MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.Department.Name))
                .ForMember(l => l.SimId, opt => opt.MapFrom(s => s.Device.SimId))
                .ForMember(l => l.SimIccid, opt => opt.MapFrom(s => s.Device.Sim.Iccid))
                .ForMember(l => l.SimPhoneNumber, opt => opt.MapFrom(s => s.Device.Sim.PhoneNumber))
                .ForMember(l => l.SimCarrier, opt => opt.MapFrom(s => s.Device.Sim.Carrier));


            /*  MdaDeviceStatus */

            CreateMap<DeviceStatusQueryResource, MdaDeviceStatusQuery>();

            CreateMap<DeviceStatusSaveResource, MdaDeviceStatus>();

            CreateMap<MdaDeviceStatus, DeviceStatusForList>()
                .ForMember(l => l.DeviceCount, opt => opt.MapFrom(s => s.MdaDevice.Count()));

            /*  MdaProduct */

            CreateMap<ProductQueryResource, MdaProductQuery>();

            CreateMap<ProductSaveResource, MdaProduct>();

            CreateMap<MdaProduct, ProductForList>()
                .ForMember(l => l.ProductManufacturerId, opt => opt.MapFrom(s => s.ProductModel.ProductManufacturerId))
                .ForMember(l => l.ProductManufacturerName, opt => opt.MapFrom(s => s.ProductModel.ProductManufacturer.Name))
                .ForMember(l => l.ProductTypeId, opt => opt.MapFrom(s => s.ProductModel.ProductTypeId))
                .ForMember(l => l.ProductTypeName, opt => opt.MapFrom(s => s.ProductModel.ProductType.Name))
                .ForMember(l => l.DeviceCount, opt => opt.MapFrom(s => s.MdaDevice.Count()));

            /*  MdaProductCapacity */

            CreateMap<ProductCapacityQueryResource, MdaProductCapacityQuery>();

            CreateMap<ProductCapacitySaveResource, MdaProductCapacity>();

            CreateMap<MdaProductCapacity, ProductCapacityForList>()
                .ForMember(l => l.ProductTypeId, opt => opt.MapFrom(s => s.ProductModel.ProductTypeId))
                .ForMember(l => l.ProductTypeName, opt => opt.MapFrom(s => s.ProductModel.ProductType.Name))
                .ForMember(l => l.ProductManufacturerId, opt => opt.MapFrom(s => s.ProductModel.ProductManufacturer.Id))
                .ForMember(l => l.ProductManufacturerName, opt => opt.MapFrom(s => s.ProductModel.ProductManufacturer.Name))
                .ForMember(l => l.ProductCount, opt => opt.MapFrom(s => s.MdaProduct.Count()));

            /*  MdaProductManufacturer */

            CreateMap<ProductManufacturerQueryResource, MdaProductManufacturerQuery>();

            CreateMap<ProductManufacturerSaveResource, MdaProductManufacturer>();

            CreateMap<MdaProductManufacturer, ProductManufacturerForList>()
                .ForMember(l => l.ProductCount, opt => opt.MapFrom(s => s.MdaProductModel.Count()));

            /*  MdaProductModel */

            CreateMap<ProductModelQueryResource, MdaProductModelQuery>();

            CreateMap<ProductModelSaveResource, MdaProductModel>();

            CreateMap<MdaProductModel, ProductModelForList>()
                .ForMember(l => l.ProductCount, opt => opt.MapFrom(s => s.MdaProduct.Count()));            

            /*  MdaProductType */

            CreateMap<ProductTypeQueryResource, MdaProductTypeQuery>();

            CreateMap<ProductTypeSaveResource, MdaProductType>();

            CreateMap<MdaProductType, ProductTypeForList>()
                .ForMember(l => l.ProductModelCount, opt => opt.MapFrom(s => s.MdaProductModel.Count()));

            /*  MdaSimCard */

            CreateMap<SimCardQueryResource, MdaSimCardQuery>();

            CreateMap<SimCardSaveResource, MdaSimCard>();

            CreateMap<MdaSimCard, SimCardForList>()
                .ForMember(l => l.DeviceId, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).Id))
                .ForMember(l => l.SerialNumber, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).SerialNumber))
                .ForMember(l => l.Esn, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).Esn))
                .ForMember(l => l.Os, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).Os))
                .ForMember(l => l.ProductId, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).ProductId))
                .ForMember(l => l.ProductModelName, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).Product.ProductModel.Name))
                .ForMember(l => l.ProductModelId, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).Product.ProductModelId))
                .ForMember(l => l.ProductCapacityName, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).Product.ProductCapacity.Name))
                .ForMember(l => l.ProductManufacturerName, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).Product.ProductModel.ProductManufacturer.Name))
                .ForMember(l => l.AssigneeId, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).AssigneeId))
                .ForMember(l => l.AssigneeFirstName, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.FirstName))
                .ForMember(l => l.AssigneeLastName, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.LastName))
                .ForMember(l => l.AssigneeDepartmentName, opt => opt.MapFrom(s => s.MdaDevice.FirstOrDefault(d => d.Active == 1).MdaDeviceAssignment.FirstOrDefault(mda => mda.Active == 1).MdaDeviceAssignee.Department.Name));                

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