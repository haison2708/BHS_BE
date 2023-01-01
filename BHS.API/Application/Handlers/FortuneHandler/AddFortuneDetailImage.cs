using BHS.API.Application.Commands.FortuneCommand;
using BHS.API.Services;
using BHS.API.ViewModels.Fortunes;
using BHS.Domain.Entities.Fortunes;
using BHS.Domain.SeedWork;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.FortuneHandler;

public class AddFortuneDetailImageHandler : IRequestHandler<AddFortuneDetailImage, FortuneDetailViewModel?>
{
    private readonly IConfiguration _configuration;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AddFortuneDetailImageHandler(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<FortuneDetailViewModel?> Handle(AddFortuneDetailImage request,
        CancellationToken cancellationToken)
    {
        var fortuneDetail = await _unitOfWork.Repository<FortuneDetail>().Get()
            .FirstOrDefaultAsync(x => x.Id == request.FortuneDetailId, cancellationToken);
        if (fortuneDetail is null)
            return null;
        var fileName = _configuration["FolderUpload:FortuneDetail"] + $"{fortuneDetail.Id}";
        var isUploaded = await _fileService.UploadBlob(fileName, request.Image!, _configuration["BlobContainerName"]);
        fortuneDetail.Image = isUploaded ? _fileService.GetBlob(fileName, _configuration["BlobContainerName"]) : "";
        return await _unitOfWork.SaveChangesAsync(cancellationToken)
            ? _mapper.Map<FortuneDetailViewModel>(fortuneDetail)
            : null;
    }
}