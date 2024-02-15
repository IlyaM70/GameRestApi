using AutoMapper;
using GameRestApi.Model;
using GameRestApi.Model.DTOs;

namespace GameRestApi
{
    /// <summary>
    /// Конфигурация автомапера
    /// </summary>
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            /////////// GameTransaction and GameTransactionDto
            CreateMap<GameTransaction, GameTransactionDto>();
            CreateMap<GameTransactionDto, GameTransaction>();
        }
    }
}
