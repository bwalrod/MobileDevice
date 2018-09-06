using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MobileDevice.API.Controllers.Resources.SimCard;
using MobileDevice.API.Data.SimCard;
using MobileDevice.API.Helpers;
using MobileDevice.API.Models;
using MobileDevice.API.Models.Query;

namespace MobileDevice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]       
    public class SimCardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISimCardRepository _repo;
        private readonly IAuthority _auth;

        public SimCardController(IMapper mapper, ISimCardRepository repo, IAuthority auth)
        {
            _mapper = mapper;
            _repo = repo;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetSimCards([FromQuery] SimCardQueryResource filterResource)
        {
            if(!_auth.IsValidUser(User))
                return NoContent();

            var filter = _mapper.Map<SimCardQueryResource, MdaSimCardQuery>(filterResource);

            var simCards = await _repo.GetSimCards(filter);

            Response.AddPagination(simCards.CurrentPage, simCards.PageSize, 
                    simCards.TotalCount, simCards.TotalPages);             

            return Ok(simCards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSimCard(int id)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            var simCard = await _repo.GetSimCard(id);
            return Ok(simCard);
        }

        [HttpPost]
        public async Task<IActionResult> AddSimCard([FromBody] SimCardSaveResource simCardSaveResource)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Test for prexistence */
            var filter = new MdaSimCardQuery(){
                Iccid = simCardSaveResource.Iccid,
                PhoneNumber = simCardSaveResource.PhoneNumber,
                Carrier = simCardSaveResource.Carrier
            };
            var simCardFromRepo = await _repo.GetSimCards(filter);
            if (simCardFromRepo.Any())
                return BadRequest($"Sim Card with ICCID {simCardSaveResource.Iccid} already exists");

            var simCard = _mapper.Map<MdaSimCard>(simCardSaveResource);
            simCard.CreatedBy = User.Identity.Name;

            _repo.Add(simCard);

            if (await _repo.SaveAll())
                return Ok(simCard);

            return BadRequest("Failed to add Sim Card");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSimCard(int id, [FromBody] SimCardSaveResource simCardSaveResource)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            /* Test for prexistence */
            var filter = new MdaSimCardQuery(){
                Iccid = simCardSaveResource.Iccid,
                PhoneNumber = simCardSaveResource.PhoneNumber,
                Carrier = simCardSaveResource.Carrier
            };
            var simCardFromRepoExisting = await _repo.GetSimCards(filter);
            if (simCardFromRepoExisting.Any())
                return BadRequest($"Sim Card with ICCID {simCardSaveResource.Iccid} already exists");

            var simCardFromRepo  = await _repo.GetSimCard(id);

            if (simCardFromRepo == null)
                return BadRequest($"Sim Card {id} could not be found.");

            _mapper.Map<SimCardSaveResource, MdaSimCard>(simCardSaveResource, simCardFromRepo);
            simCardFromRepo.ModifiedBy = User.Identity.Name;
            simCardFromRepo.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to update Sim Card");
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSimCard(int id)
        {
            if (!_auth.IsValidUser(User))
                return NoContent();

            var simCardFromRepo = await _repo.GetSimCard(id);

            if (simCardFromRepo == null)
                return BadRequest($"Sim Card {id} could not be found");

            _repo.Delete(simCardFromRepo);

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete Sim Card");
        }
    }
}