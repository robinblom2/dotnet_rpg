using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Character, AddCharacterRequestDto>().ReverseMap();
            CreateMap<Character, GetCharacterResponseDto>().ReverseMap();
            CreateMap<Character, UpdateCharacterRequestDto>().ReverseMap();
        }
    }
}