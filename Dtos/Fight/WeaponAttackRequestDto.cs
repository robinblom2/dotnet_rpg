using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Fight
{
    public class WeaponAttackRequestDto
    {
        public int AttackerId { get; set; }
        public int OpponentId { get; set; }
    }
}