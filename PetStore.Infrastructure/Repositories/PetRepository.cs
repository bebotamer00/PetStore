using AutoMapper;
using PetStore.Core.Dtos.PetDto;

namespace PetStore.Infrastructure.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _uploadDirectory;

        public PetRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(_uploadDirectory))
                Directory.CreateDirectory(_uploadDirectory);
        }

        public async Task<IEnumerable<DisplayPets>> GetAllAsync()
        {
            var getAllPets = await _context.Pets
                .Include(u => u.User)
                .AsNoTracking()
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<DisplayPets>>(getAllPets);

            return result;
        }

        public async Task AddPetWithImage(CreatePetDto createPetDto)
        {
            string uniqueFileName = GetUniqueFileName(createPetDto.Image.FileName);
            string imagePath = Path.Combine(_uploadDirectory, uniqueFileName);

            FileStream fileStream = new(imagePath, FileMode.Create);
            await createPetDto.Image.CopyToAsync(fileStream);

            var pet = _mapper.Map<Pet>(createPetDto);
            pet.Image = uniqueFileName;

            await Add(pet);
        }

        public async Task UpdatePetWithImage(UpdatePetDto updatePetDto)
        {
            var existingPet = await _context.Pets
                .FirstOrDefaultAsync(p => p.Id == updatePetDto.Id) ??
                throw new InvalidOperationException($"Pet with ID {updatePetDto.Id} not found.");


            if (updatePetDto.NewImage is null)
                throw new InvalidOperationException($"Image Not Found.");

            string uniqueFileName = GetUniqueFileName(updatePetDto.NewImage.FileName);
            string newImagePath = Path.Combine(_uploadDirectory, uniqueFileName);

            FileStream newImageStream = new(newImagePath, FileMode.Create);
            await updatePetDto.NewImage.CopyToAsync(newImageStream);

            if (!string.IsNullOrEmpty(existingPet.Image))
            {
                string oldImagePath = Path.Combine(_uploadDirectory, existingPet.Image);
                if (File.Exists(oldImagePath))
                    File.Delete(oldImagePath);
            }

            existingPet.Image = uniqueFileName;
            _mapper.Map(updatePetDto, existingPet);

            await Update(existingPet.Id, existingPet);
        }

        public async Task<bool> DeleteAsyncWithImage(int id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet is null)
                return false;

            if (!string.IsNullOrEmpty(pet.Image))
            {
                string imagePath = Path.Combine(_uploadDirectory, pet.Image);

                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return true;
        }

        private static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                "_",
                Guid.NewGuid().ToString().AsSpan(0, 4),
                Path.GetExtension(fileName));
        }

        public async Task<IEnumerable<Pet>> SearchPetsAsync(string searchTerm) => await _context.Pets
            .Where(p => EF.Functions.Like(p.Name, $"%{searchTerm}%"))
            .ToListAsync();
    }
}
