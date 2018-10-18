using System;
using System.Collections.Generic;
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

            var simCardsList = _mapper.Map<IEnumerable<SimCardForList>>(simCards);

            return Ok(simCardsList);
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
            if (!_auth.IsAppAdmin(User))
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

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateSimCard(int id)
        {
            if(!_auth.IsAppAdmin(User))
                return NoContent();

            var sc = await _repo.GetSimCard(id);

            if (sc == null)
                return BadRequest($"Sim Card {id} could not be found.");

            sc.Active = 0;
            sc.ModifiedBy = User.Identity.Name.Replace("\\\\","\\");
            sc.ModifiedDate = DateTime.Now;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Failed to delete Sim Card");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSimCard(int id, [FromBody] SimCardSaveResource simCardSaveResource)
        {
            if (!_auth.IsAppAdmin(User))
                return NoContent();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var simCardFromRepo  = await _repo.GetSimCard(id);

            if (simCardFromRepo == null)
                return BadRequest($"Sim Card {id} could not be found.");                

            /* Test for prexistence */
            var filter = new MdaSimCardQuery(){
                Iccid = simCardSaveResource.Iccid,
                PhoneNumber = simCardSaveResource.PhoneNumber,
                Carrier = simCardSaveResource.Carrier,
                Active = 2
            };
            var simCardFromRepoExisting = await _repo.GetSimCards(filter);
            if (simCardFromRepoExisting.Any()){
                var existingSim = simCardFromRepoExisting.FirstOrDefault();
                if (existingSim.Id != id)
                    return BadRequest($"Sim Card with ICCID {simCardSaveResource.Iccid} already exists");
                else {
                    if (existingSim.PhoneNumber.ToLower() == simCardSaveResource.PhoneNumber.ToLower() && existingSim.Carrier.ToLower() == simCardSaveResource.Carrier.ToLower()) {
                        if (existingSim.Active == Convert.ToByte(simCardSaveResource.Active == true ? 1 : 0))
                        return BadRequest("Nothing has changed");
                    }
                }
                

            }

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
            if (!_auth.IsAppAdmin(User))
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