namespace Sekmen.Commerce.Services.Products.Application.Products;

public record CreateProductCommand(ProductDto ProductDto) : ICommand<Result<ProductDto>>;
public record UpdateProductCommand(ProductDto ProductDto) : ICommand<Result<ProductDto>>;
public record DeleteProductCommand(int Id) : ICommand<Result<bool>>;

internal sealed class CreateProductCommandHandler(
    ProductDbContext context,
    IMapper mapper
) : ICommandHandler<CreateProductCommand, Result<ProductDto>>,
    ICommandHandler<UpdateProductCommand, Result<ProductDto>>,
    ICommandHandler<DeleteProductCommand, Result<bool>>
{
    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request.ProductDto);
        await context.AddAsync(product, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? Result.Ok(request.ProductDto)
            : Result.Fail<ProductDto>("DB exception");
    }

    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request.ProductDto);
        context.Update(product);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? Result.Ok(request.ProductDto)
            : Result.Fail<ProductDto>("DB exception");
    }

    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await context.Products.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (product is null)
            return Result.Ok(true);

        context.Remove(product);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? Result.Ok(true)
            : Result.Fail<bool>("DB exception");
    }
}