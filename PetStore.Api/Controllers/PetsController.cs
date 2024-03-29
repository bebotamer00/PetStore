﻿using PetStore.Core.Dtos.PetDto;

namespace PetStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet("AllPets")]
        public async Task<IActionResult> GetAllPets(int? pageNumber, int? pageSize, string? searchName, Gender? gender)
        {
            var pets = await _unitOfWork.PetRepository.GetAllAsync(pageNumber, pageSize, searchName, gender);

            if (pets is null)
                return NotFound();

            var model = _mapper.Map<IEnumerable<DisplayPets>>(pets);

            return Ok(model);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var pet = await _unitOfWork.PetRepository.GetByIdAsync(p => p.Id == id, p => p.User, p => p.Images);

            if (pet is null)
                return NotFound();

            var model = _mapper.Map<DisplayPets>(pet);

            return Ok(model);
        }

        [HttpPost("AddNewPet")]
        public async Task<IActionResult> AddPet([FromForm] CreatePetDto createPetDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            List<IFormFile> images = createPetDto.Images;

            await _unitOfWork.PetRepository.AddPetWithImage(createPetDto, images);

            return Ok(createPetDto);
        }

        [HttpPut("UpdatePet")]
        public async Task<IActionResult> UpdatePet([FromForm] UpdatePetDto updatePetDto,
            [FromForm(Name = "newImages")] List<IFormFile> newImages)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _unitOfWork.PetRepository.UpdatePetWithImage(updatePetDto, newImages);

            return Ok(updatePetDto);
        }

        [HttpDelete("DeletePet/{id:int}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var deletedPet = await _unitOfWork.PetRepository.DeleteAsyncWithImage(id);

            if (!deletedPet)
                return NotFound();

            return Ok("Deleted Sucessfully");
        }
    }
}
