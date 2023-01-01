using BHS.API.Application.Commands.FortuneCommand;
using BHS.API.Services;
using BHS.API.ViewModels.Fortunes;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.FortuneHandler;

public class AddBannerFortuneHandler : IRequestHandler<AddBannerFortune, FortuneViewModel?>
{
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddBannerFortuneHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<FortuneViewModel?> Handle(AddBannerFortune request, CancellationToken cancellationToken)
    {
        var fortune = await _unitOfWork.Repository<Fortune>().Get()
            .FirstOrDefaultAsync(x => x.Id == request.FortuneId, cancellationToken);
        if (fortune is null)
            return null!;
        var fileName = _configuration["FolderUpload:Fortune"] + $"banner-{fortune.Id}";
        var isUploaded =
            await _fileService.UploadBlob(fileName, request.ImageBanner!, _configuration["BlobContainerName"]);
        fortune.ImageBanner = isUploaded ? _fileService.GetBlob(fileName, _configuration["BlobContainerName"]) : "";
        return await _unitOfWork.SaveChangesAsync(cancellationToken) ? _mapper.Map<FortuneViewModel>(fortune) : null;
    }
}