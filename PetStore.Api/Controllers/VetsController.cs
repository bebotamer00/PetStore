using PetStore.Core.Dtos.FeedbackDto;
using PetStore.Core.Dtos.VetDto;

namespace PetStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet("All Vets")]
        public async Task<IActionResult> GetAllVets()
        {
            var vets = await _unitOfWork.VetRepository.GetAllAsync();

            if (vets is null)
                return NotFound();

            return Ok(vets);
        }

        [HttpPost("Add Vet")]
        public async Task<IActionResult> AddVet([FromForm] AddVetDto addVetDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var model = _mapper.Map<AddVetDto, Vet>(addVetDto);

            await _unitOfWork.VetRepository.Add(model);

            return Ok(addVetDto);
        }

        [HttpPut("Edit Vet/{id:int}")]
        public async Task<IActionResult> UpdateVet(int id, [FromForm] UpdateVetDto updateVetDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var vet = await _unitOfWork.VetRepository.GetByIdAsync(id);

            if (vet is null)
                return BadRequest();

            _mapper.Map(updateVetDto, vet);

            await _unitOfWork.VetRepository.Update(id, vet);

            return Ok(updateVetDto);
        }

        [HttpPost("{vetId}/AddWorkingHours")]
        public async Task<IActionResult> AddWorkingHoursToVet(int vetId, [FromBody] WorkingHoursDto workingHoursDto)
        {
            var vet = await _unitOfWork.VetRepository.GetByIdAsync(vetId);

            if (vet is null)
                return NotFound();

            var workingHoursEntity = _mapper.Map<WorkingHoursDto, WorkingHours>(workingHoursDto);

            await _unitOfWork.VetRepository.AddWorkingHours(vet, workingHoursEntity);

            return Ok(workingHoursDto);
        }

        [HttpPost("AddFeedbackToVet")]
        public async Task<IActionResult> AddFeeback([FromForm] AddFeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _unitOfWork.VetRepository.AddFeedback(feedbackDto);

            return Ok(feedbackDto);
        }

        [HttpGet("GetVetWithFeedback/{vetId}")]
        public async Task<IActionResult> GetVetWithFeedback(int vetId)
        {
            var vetWithFeedback = await _unitOfWork.VetRepository.GetVetWithFeedbackAsync(vetId);

            if (vetWithFeedback is null)
                return NotFound();

            return Ok(vetWithFeedback);
        }

    }
}
