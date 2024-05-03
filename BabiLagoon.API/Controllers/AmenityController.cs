using AutoMapper;
using BabiLagoon.API.Filters;
using BabiLagoon.Application.Common.DTOs;
using BabiLagoon.Application.Common.Interfaces;
using BabiLagoon.Domain.Entities;
using BabiLagoon.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabiLagoon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityRepository amenityRepository;
        private readonly IMapper mapper;

        public AmenityController(IAmenityRepository amenityRepository,IMapper mapper)
        {
            this.amenityRepository = amenityRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("AllAmenity")]
        //[Authorize(Roles = "Writer,Reader")]
        // GET: /api/villa?filterOn=Name&filterQuery=Track&isAscending=true

        public async Task<IActionResult> GetAllAmenity()
        {
            var amenities = await amenityRepository.GetAllAsync();
            return Ok(mapper.Map<List<AmenityDto>>(amenities));


        }
        [HttpGet]
        [Route("{id}")]
        //[Authorize(Roles = "Writer,Reader")]

        public async Task<IActionResult> GetAmenityById([FromRoute] int id)
        {
            var amenity = await amenityRepository.GetByIdAsync(id);
            if (amenity is null)
            {
                return NotFound("Amenity not found");
            }
            return Ok(mapper.Map<AmenityDto>(amenity));
        }
        [HttpPost]
        [Route("createamenity")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> CreateAmenity([FromBody]CreateAmenityDto createAmenityDto)
        {
            var amenity = mapper.Map<Amenity>(createAmenityDto);
            await amenityRepository.AddAsync(amenity);
            return Ok(mapper.Map<AmenityDto>(amenity));
        }
        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]


        public async Task<IActionResult> UpdateAmenity([FromRoute] int id, [FromBody] UpdateAmenityDto updateAmenityDto)
        {
            var amenity = mapper.Map<Amenity>(updateAmenityDto);
            var result = await amenityRepository.UpdateAsync(id, amenity);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]


        public async Task<IActionResult> DeleteAmenity([FromRoute] int id)
        {
            var amenity = await amenityRepository.DeleteAsync(id);
            return Ok("Amenity Deleted Successfuly");
        }

        [HttpDelete]
        //[Authorize(Roles = "Writer")]

        public async Task<IActionResult> DeleteAllAmenity()
        {
            await amenityRepository.DeleteAllAsync();
            return NoContent();
        }
    }
}
