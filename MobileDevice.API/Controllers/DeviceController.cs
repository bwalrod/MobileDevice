using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources;
using MobileDevice.API.Data;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _repo;
        private readonly IMapper _mapper;
        public DeviceController(IMapper mapper, IDeviceRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices([FromQuery] DeviceQueryResource filterResource)
        {
            var filter = _mapper.Map<DeviceQueryResource, MdaDeviceQuery>(filterResource);
            var devices = await _repo.GetDevices(filter);
            return Ok(devices);
        }
        // public async Task<IActionResult> GetDevices()
        // {
        //     var devices = await _repo.GetDevices();

        //     return Ok(devices);
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id)
        {
            var device = await _repo.GetDevice(id);
            return Ok(device);
        }
    }
}