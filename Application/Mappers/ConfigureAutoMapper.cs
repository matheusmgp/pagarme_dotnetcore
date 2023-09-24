using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ConfigureAutoMapper : Profile
    {
        public ConfigureAutoMapper()
        {
            CreateMap<TransactionEntity, TransactionDto>().ReverseMap();
            CreateMap<PayableEntity, PayableDto>().ReverseMap();           
            CreateMap<PayablesDto, PayableEntity>();
        }
    }
}
