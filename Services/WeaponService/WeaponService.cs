namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public WeaponService(IMapper mapper, DataContext context, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _context = context;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContext.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<GetCharacterResponseDto>> AddWeapon(AddWeaponRequestDto newWeapon)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();

            try
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User!.Id == GetUserId());

                if (character is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character could not be found.";
                    return serviceResponse;
                }

                _context.Weapons.Add(_mapper.Map<Weapon>(newWeapon));
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(character);
                serviceResponse.Success = true;
                serviceResponse.Message = "Weapon added successfully.";

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