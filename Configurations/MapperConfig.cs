namespace dotnet_rpg.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Character, AddCharacterRequestDto>().ReverseMap();
            CreateMap<Character, GetCharacterResponseDto>().ReverseMap();
            CreateMap<Character, UpdateCharacterRequestDto>().ReverseMap();
            CreateMap<Weapon, AddWeaponRequestDto>().ReverseMap();
            CreateMap<Weapon, GetWeaponResponseDto>().ReverseMap();
            CreateMap<Skill, GetSkillResponseDto>().ReverseMap();
            CreateMap<Character, HighscoreResponseDto>().ReverseMap();
        }
    }
}