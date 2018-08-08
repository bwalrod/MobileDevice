using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Data;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceRepository _repo;
        public DeviceController(IDeviceRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            var devices = await _repo.GetDevices();

            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice(int id) 
        {
            var device = await _repo.GetDevice(id);
            return Ok(device);
        }
    }
}