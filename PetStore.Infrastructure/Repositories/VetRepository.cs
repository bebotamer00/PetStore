
using AutoMapper;
using PetStore.Core.Dtos.AddressDto;
using PetStore.Core.Dtos.FeedbackDto;
using PetStore.Core.Dtos.VetDto;

namespace PetStore.Infrastructure.Repositories
{
    public class VetRepository(ApplicationDbContext context, IMapper mapper) : Repository<Vet>(context), IVetRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<DisplayVetDto>> GetAllAsync()
        {
            var vets = await _context.Vets
                .Include(v => v.Address)
                .Include(v => v.User)
                .Include(v => v.WorkingHours)
                .ToListAsync();

            if (!vets.Any())
                return [];

            var result = _mapper.Map<IEnumerable<DisplayVetDto>>(vets);

            return result;
        }

        public async Task AddWorkingHours(Vet vet, WorkingHours workingHours)
        {
            ArgumentNullException.ThrowIfNull(vet);
            ArgumentNullException.ThrowIfNull(workingHours);

            vet.WorkingHours.Add(workingHours);
            await _context.SaveChangesAsync();
        }

        public async Task<DisplayVetWithFeedbackDto> AddFeedback(AddFeedbackDto feedbackDto)
        {
            ArgumentNullException.ThrowIfNull(feedbackDto);

            var feedbackToAdd = _mapper.Map<Feedback>(feedbackDto);

            await _context.Feedbacks.AddAsync(feedbackToAdd);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<DisplayVetWithFeedbackDto>(feedbackToAdd);

            return result;
        }

        public async Task<DisplayVetWithFeedbackDto> GetVetWithFeedbackAsync(int vetId)
        {
            var vet = await _context.Vets
                .Include(v => v.Feedbacks)
                .ThenInclude(f => f.User)
                .Where(v => v.Id == vetId)
                .Select(v => _mapper.Map<DisplayVetWithFeedbackDto>(v))
                .FirstOrDefaultAsync();

            return vet!;
        }
    }
}
