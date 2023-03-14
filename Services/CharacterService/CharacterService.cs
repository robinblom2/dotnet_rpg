using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var character = _mapper.Map<Character>(newCharacter);

            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<List<GetCharacterResponseDto>>(await _context.Characters.Where(c => c.User!.Id == GetUserId()).ToListAsync());
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

                if (character is null)
                {
                    throw new Exception($"Character with Id: {id} not found.");
                }

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<List<GetCharacterResponseDto>>(await _context.Characters.ToListAsync());
                serviceResponse.Success = true;
                serviceResponse.Message = "Character deleted successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
            var characters = await _context.Characters.Where(c => c.User!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetCharacterResponseDto>>(characters);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

                if (character == null)
                {
                    throw new Exception($"Character with Id: {id} not found.");
                }

                serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(character);
                serviceResponse.Success = true;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if (character == null)
                {
                    throw new Exception($"Character with Id: {updatedCharacter.Id} not found.");
                }
                _mapper.Map(updatedCharacter, character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(character);
                serviceResponse.Success = true;
                serviceResponse.Message = "Character updated successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}