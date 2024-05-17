using AutoMapper;
using BabiLagoon.API.Filters;
using BabiLagoon.Application.Common.DTOs;
using BabiLagoon.Application.Common.Interfaces;
using BabiLagoon.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabiLagoon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly IVillaRepository villaRepository;
        private readonly IMapper mapper;

        public VillaController(IVillaRepository villaRepository , IMapper mapper)
        {
            this.villaRepository = villaRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("AllVilla")]
        [Authorize(Roles = "Writer,Reader")]
        // GET: /api/villa?filterOn=Name&filterQuery=Track&isAscending=true

        public async Task<IActionResult> GetAllVilla()
        {
            var villas = await villaRepository.GetAllAsync();
            return Ok(mapper.Map<List<VillaDto>>(villas));


        }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Writer,Reader")]

        public async Task<IActionResult> GetVillaById([FromRoute] int id)
        {
            var villa = await villaRepository.GetByIdAsync(id);
            if (villa is null)
            {
                return NotFound("Villa not found");
            }
            return Ok(mapper.Map<VillaDto>(villa));
        }
        [HttpPost]
        [Route("createvilla")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> CreateVilla([FromBody] CreateVillaDto createVillaDto)
        {
            var villa = mapper.Map<Villa>(createVillaDto);
            await villaRepository.AddAsync(villa);
            return Ok(mapper.Map<VillaDto>(villa));
        }
        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]


        public async Task<IActionResult> UpdateVilla([FromRoute] int id, [FromBody] UpdateVillaDto updateVillaDto)
        {
            var villa = mapper.Map<Villa>(updateVillaDto);
            var result = await villaRepository.UpdateAsync(id, villa);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        

        public async Task<IActionResult> DeleteVilla([FromRoute] int id)
        {
            var villa = await villaRepository.DeleteAsync(id);
            return Ok("Villa Deleted Successfuly");
        }

        [HttpDelete]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteAllVilla()
        {
            await villaRepository.DeleteAllAsync();
            return NoContent();
        }
    }
}
