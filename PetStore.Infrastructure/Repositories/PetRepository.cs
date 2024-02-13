using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        public async Task<IEnumerable<DisplayPets>> GetAllAsync(int? pageNumber, int? pageSize, string? searchName, Gender? gender)
        {
            // Set default values for pageNumber and pageSize if they are not provided
            pageNumber ??= 1;
            pageSize ??= 10;

            // Calculate the number of items to skip based on the pageNumber and pageSize
            var itemsToSkip = (pageNumber.Value - 1) * pageSize.Value;

            // Start building the query
            var query = _context.Pets
                .Include(u => u.User)
                .Include(p => p.Images)
                .AsNoTracking();

            // Apply gender filter if provided
            if (gender.HasValue)
                query = query.Where(p => p.Gender == gender.Value);


            // search by name
            query = query.Where(p => EF.Functions.Like(p.Name, $"%{searchName}%"));

            // Continue building the query with pagination
            var getAllPets = await query
                .Skip(itemsToSkip)
                .Take(pageSize.Value)
                .ToListAsync();

            // Map the result to DisplayPets
            var result = _mapper.Map<IEnumerable<DisplayPets>>(getAllPets);

            return result;
        }

        public async Task AddPetWithImage(CreatePetDto createPetDto, List<IFormFile> images)
        {
            string[] uniqueFileName = images.Select(image => GetUniqueFileName(image.FileName)).ToArray();
            List<PetImage> petImages = [];

            for (int i = 0; i < images.Count; i++)
            {
                string imagePath = Path.Combine(_uploadDirectory, uniqueFileName[i]);
                FileStream fileStream = new(imagePath, FileMode.Create);
                await images[i].CopyToAsync(fileStream);

                petImages.Add(new PetImage { ImageUrl = uniqueFileName[i] });
            }

            var pet = _mapper.Map<Pet>(createPetDto);
            pet.Images = petImages;

            await Add(pet);
        }

        public async Task UpdatePetWithImage(UpdatePetDto updatePetDto, List<IFormFile> newImages)
        {
            var existingPet = await _context.Pets
                .Include(pet => pet.Images)
                .FirstOrDefaultAsync(p => p.Id == updatePetDto.Id) ??
                throw new InvalidOperationException($"Pet with ID {updatePetDto.Id} not found.");


            if (updatePetDto.NewImage is null || newImages.Count is 0)
                throw new InvalidOperationException($"Image Not Found.");

            List<PetImage> petImages = [];

            foreach (var newImage in newImages)
            {
                string uniqueFileName = GetUniqueFileName(updatePetDto.NewImage.FileName);
                string newImagePath = Path.Combine(_uploadDirectory, uniqueFileName);
                FileStream newImageStream = new(newImagePath, FileMode.Create);
                await newImage.CopyToAsync(newImageStream);
                petImages.Add(new PetImage { ImageUrl = uniqueFileName });
            }

            foreach (var oldImage in existingPet.Images)
            {
                string oldImagePath = Path.Combine(_uploadDirectory, oldImage.ImageUrl);
                if (File.Exists(oldImagePath))
                    File.Delete(oldImagePath);
            }

            existingPet.Images = petImages;
            _mapper.Map(updatePetDto, existingPet);

            await Update(existingPet.Id, existingPet);
        }

        public async Task<bool> DeleteAsyncWithImage(int id)
        {
            var pet = await _context.Pets
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pet is null)
                return false;

            foreach (var image in pet.Images)
            {
                string imagePath = Path.Combine(_uploadDirectory, image.ImageUrl);
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
    }
}