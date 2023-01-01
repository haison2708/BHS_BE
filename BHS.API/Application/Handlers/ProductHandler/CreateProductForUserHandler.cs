using BHS.API.Application.Commands.ProductCommand;
using BHS.API.Services;
using BHS.Domain.Entities.Products;
using BHS.Domain.Enumerate;
using BHS.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BHS.API.Application.Handlers.ProductHandler;

public class CreateProductForUserHandler : IRequestHandler<CreateProductForUser, bool>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductForUserHandler(IUnitOfWork unitOfWork, IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    /// <summary>
    ///     Kiểm tra user đã xem sản phẩm chưa, nếu chưa thì thêm vào vs Type = StatusList.ProductViewed.Id
    ///     và lấy hết sản phẩm cùng loại, cùng nhà cung cấp của sản phẩm vừa xem thêm vào vs Type =
    ///     StatusList.ProductSuggestion.Id
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> Handle(CreateProductForUser request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var isSaved = false;
        var product = await _unitOfWork.Repository<Product>().Get().Include(x => x.ParentProduct)
            .ThenInclude(x => x!.Category).FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is not null)
        {
            var productViewed = await _unitOfWork.Repository<ProductForUser>().Get().FirstOrDefaultAsync(x =>
                x.ProductId == request.ProductId
                && x.UserId == userId, cancellationToken);
            if (productViewed != null)
            {
                productViewed.Type = ProductForUserType.Viewed;
                return true;
            }

            var listProductForUser = new List<ProductForUser>
            {
                new()
                {
                    UserId = userId,
                    ProductId = request.ProductId,
                    Type = ProductForUserType.Viewed
                }
            };
            var listProductForUserExists = await _unitOfWork.Repository<ProductForUser>().Get().Include(x => x.Product)
                .Where(x =>
                    x.UserId == userId)
                .ToListAsync(cancellationToken);
            var listProduct = await _unitOfWork.Repository<Product>().Get().Include(x => x.ParentProduct).Where(x =>
                    x.ParentProduct!.CategoryId == product.ParentProduct!.CategoryId && x.Id
                    != product.Id && x.ParentProduct.VendorId == product.ParentProduct.VendorId &&
                    listProductForUserExists.All(p => p.ProductId != x.Id))
                .ToListAsync(cancellationToken);
            listProductForUser.AddRange(listProduct.Select(item => new ProductForUser
                { UserId = userId, ProductId = item.Id, Type = ProductForUserType.Suggestion }));
            await _unitOfWork.Repository<ProductForUser>().InsertRangeAsync(listProductForUser.AsEnumerable());
            isSaved = await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return isSaved;
    }
}