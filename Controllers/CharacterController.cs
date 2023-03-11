using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> GetAll()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> AddCharacter(AddCharacterRequestDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            return Ok(await _characterService.UpdateCharacter(updatedCharacter));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            return Ok(await _characterService.DeleteCharacter(id));
        }
    }
}