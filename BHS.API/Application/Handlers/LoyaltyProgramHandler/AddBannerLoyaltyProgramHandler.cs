using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.API.Services;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.LoyaltyProgramHandler;

public class AddBannerLoyaltyProgramHandler : IRequestHandler<AddBannerLoyaltyProgram, LoyaltyProgramViewModel?>
{
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddBannerLoyaltyProgramHandler(IUnitOfWork unitOfWork, IConfiguration configuration,
        IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<LoyaltyProgramViewModel?> Handle(AddBannerLoyaltyProgram request,
        CancellationToken cancellationToken)

    {
        var loyaltyProgram = await _unitOfWork.Repository<LoyaltyProgram>().Get()
            .FirstOrDefaultAsync(x => x.Id == request.LoyaltyProgramId, cancellationToken);
        if (loyaltyProgram is null)
            return null!;
        var fileName = _configuration["FolderUpload:LoyaltyProgram"] + $"banner-{loyaltyProgram.Id}";
        var isUploaded =
            await _fileService.UploadBlob(fileName, request.ImageBanner!, _configuration["BlobContainerName"]);
        loyaltyProgram.ImgBannerUrl =
            isUploaded ? _fileService.GetBlob(fileName, _configuration["BlobContainerName"]) : "";
        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? _mapper.Map<LoyaltyProgramViewModel>(loyaltyProgram)
            : null;
    }
}