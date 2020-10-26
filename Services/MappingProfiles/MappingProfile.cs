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
                .ForPath(x => x.AddressLink, expression => expression.MapFrom(y => y.Address.AddressLink));

            CreateMap<MenuItem, MenuItemResponseDTO>()
               .ForMember(x => x.Id, expression => expression.MapFrom(y => y.Id))
               .ForMember(x => x.AddedDate, expression => expression.MapFrom(y => y.AddedDate))
               .ForMember(x => x.ModifiedDate, expression => expression.MapFrom(y => y.ModifiedDate))
               .ForMember(x => x.BranchId, expression => expression.MapFrom(y => y.BranchId))
               .ForMember(x => x.Description, expression => expression.MapFrom(y => y.Description))
               .ForMember(x => x.ImageBase64, expression => expression.MapFrom(y => y.ImageBase64))
               .ForMember(x => x.Name, expression => expression.MapFrom(y => y.Name))
               .ForMember(x => x.Price, expression => expression.MapFrom(y => y.Price));

            CreateMap<MenuItemRequestDTO, MenuItem>()
               .ForMember(x => x.BranchId, expression => expression.MapFrom(y => y.BranchId))
               .ForMember(x => x.Description, expression => expression.MapFrom(y => y.Description))
               .ForMember(x => x.ImageBase64, expression => expression.MapFrom(y => y.ImageBase64))
               .ForMember(x => x.Name, expression => expression.MapFrom(y => y.Name))
               .ForMember(x => x.Price, expression => expression.MapFrom(y => y.Price));

            CreateMap<BranchImage, BranchImageDTO>()
               .ForMember(x => x.BranchId, expression => expression.MapFrom(y => y.BranchId))
               .ForMember(x => x.Base64Content, expression => expression.MapFrom(y => y.Base64Content))
               .ForMember(x => x.Extension, expression => expression.MapFrom(y => y.Extension))
               .ReverseMap();

        }
    }
}