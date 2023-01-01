using BHS.API.Application.Commands.LoyaltyProgramCommand;
using BHS.API.Services;
using BHS.API.ViewModels.LoyaltyPrograms;
using BHS.Domain.Entities.LoyaltyPrograms;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.LoyaltyProgramHandler;

public class CreateLoyaltyProgramImageHandler : IRequestHandler<CreateLoyaltyProgramImage, LoyaltyProgramViewModel>
{
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoyaltyProgramImageHandler(IUnitOfWork unitOfWork, IConfiguration configuration,
        IFileService fileService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<LoyaltyProgramViewModel> Handle(CreateLoyaltyProgramImage request,
        CancellationToken cancellationToken)

    {
        var loyaltyProgram = await _unitOfWork.Repository<LoyaltyProgram>().Get()
            .FirstOrDefaultAsync(x => x.Id == request.LoyaltyProgramId, cancellationToken);
        if (loyaltyProgram is null)
            return null!;
        loyaltyProgram.LoyaltyProgramImages = new List<LoyaltyProgramImage>();
        var i = 1;
        foreach (var item in request.LoyaltyProgramImages!)
        {
            var fileName = _configuration["FolderUpload:AccumulatePoint"] + $"loyalty-program-{loyaltyProgram.Id}-{i}";
            var isUploaded = await _fileService.UploadBlob(fileName, item, _configuration["BlobContainerName"]);
            loyaltyProgram.LoyaltyProgramImages.Add(new LoyaltyProgramImage
            {
                LoyaltyProgram = loyaltyProgram,
                ImageUrl = isUploaded ? _fileService.GetBlob(fileName, _configuration["BlobContainerName"]) : ""
            });
            i++;
        }

        var isSave = await _unitOfWork.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map<LoyaltyProgramViewModel>(loyaltyProgram);
        return (isSave ? result : null)!;
    }
}