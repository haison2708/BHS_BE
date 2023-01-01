using BHS.API.Application.Commands.FortuneCommand;
using BHS.API.Application.Commands.NotifyCommand;
using BHS.API.Application.Commands.UserCommand;
using BHS.API.ViewModels.Fortunes;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.API.ViewModels.Products;
using BHS.API.ViewModels.Users;
using BHS.API.ViewModels.Vendor;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.Entities.Notify;
using BHS.Domain.Entities.Products;
using BHS.Domain.Entities.Users;
using BHS.Domain.Entities.Vendors;
using AutoMapper;

namespace BHS.API.Mappers;

public class MapperInitialize : Profile
{
    public MapperInitialize()
    {
        //Notify
        CreateMap<CreateNotificationSetUp, NotificationSetUp>();

        //Vendor
        CreateMap<Vendor, VendorViewModel>();

        //Loyalty
        CreateMap<LoyaltyProgram, LoyaltyProgramViewModel>();
        CreateMap<ProductParticipatingLoyalty, ProductParticipatingLoyaltyViewModel>();
        CreateMap<LoyaltyProgramImage, LoyaltyProgramImageViewModel>();

        //Product
        CreateMap<Product, ProductViewModel>();
        CreateMap<ParentProduct, ParentProductViewModel>();

        //User
        CreateMap<CreateUser, User>();
        CreateMap<CreateUserSettings, UserSettings>();
        CreateMap<UserSettings, UserSettingsViewModel>();

        //Fortune
        CreateMap<CreateFortune, Fortune>();
        CreateMap<Fortune, FortuneViewModel>();
        CreateMap<CreateFortuneDetail, FortuneDetail>();
        CreateMap<FortuneDetail, FortuneDetailViewModel>();
        CreateMap<CreateFortuneUserReward, FortuneUserReward>();
        CreateMap<FortuneUserReward, FortuneUserRewardViewModel>();
        CreateMap<FortuneTurnOfUser, FortuneTurnOfUserViewModel>();
    }
}