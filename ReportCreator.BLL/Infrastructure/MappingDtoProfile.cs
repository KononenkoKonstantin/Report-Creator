using AutoMapper;
using ReportCreator.BLL.DTOs;
using ReportCreator.Domain.Entities;

namespace ReportCreator.BLL.Infrastructure
{
    public class MappingDtoProfile : Profile
    {
        public MappingDtoProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();                
            CreateMap<Expenditure, ExpenditureDto>().ReverseMap();            
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
        
    }
}
