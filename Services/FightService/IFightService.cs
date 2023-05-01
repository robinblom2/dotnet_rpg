using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultResponseDto>> WeaponAttack(WeaponAttackRequestDto request);
        Task<ServiceResponse<AttackResultResponseDto>> SkillAttack(SkillAttackRequestDto request);

        Task<ServiceResponse<FightResultResponseDto>> Fight(FightRequestDto request);

        Task<ServiceResponse<List<HighscoreResponseDto>>> GetHighscore();
    }
}