using AutoMapper;
using TransferApi.Models;

namespace TransferApi.Mapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<TransferTransactionDto, TransferTransaction>();
            CreateMap<TransferTransaction, TransferTransactionDto>();
            ///
            CreateMap<TransferDto, Transfer>();
            CreateMap<Transfer, TransferDto>();
            //for after signing
            CreateMap<TransferTransaction, Transfer>();
        }
    }
}