using AutoMapper;
using Models.DataModels.Core;
using Services.DTOs;

namespace Services.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch, BranchResponseDTO>()
                .ForMember(x => x.Id, expression => expression.MapFrom(y => y.Id))
                .ForMember(x => x.AddedDate, expression => expression.MapFrom(y => y.AddedDate))
                .ForMember(x => x.ModifiedDate, expression => expression.MapFrom(y => y.ModifiedDate))
                .ForMember(x => x.BookingLink, expression => expression.MapFrom(y => y.BookingLink))
                .ForMember(x => x.SocialLink, expression => expression.MapFrom(y => y.SocialLink))
                .ForMember(x => x.PhoneNumber, expression => expression.MapFrom(y => y.PhoneNumber))
                .ForMember(x => x.MainPhotoId, expression => expression.MapFrom(y => y.MainPhotoId))
                .ForPath(x => x.AddressText, expression => expression.MapFrom(y => y.Address.ToString()))
                .ForPath(x => x.AddressLink, expression => expression.MapFrom(y => y.Address.AddressLink))
                .ReverseMap();
        }
    }
}