using Entities.Models;
using Service.Contracts.DTOs.EquipmentSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;   
namespace Service.Profiles
{
    public class EquipmentSetProfile :Profile
    {
        public EquipmentSetProfile()
        {
            CreateMap<EquipmentSet, EquipmentSetDto>()
            .ForMember(d => d.EquipmentName, o => o.MapFrom(s => s.Equipment.EquipmentName))
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Equipment.Category));
        }
    }
}
