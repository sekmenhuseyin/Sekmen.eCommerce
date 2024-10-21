namespace Sekmen.Commerce.Services.Coupons.Application.Coupons;

public record CreateCouponCommand(CouponDto CouponDto) : ICommand<Result<CouponDto>>;
public record UpdateCouponCommand(CouponDto CouponDto) : ICommand<Result<CouponDto>>;
public record DeleteCouponCommand(int Id) : ICommand<Result<bool>>;

internal sealed class CreateCouponCommandHandler(
    CouponDbContext context,
    IMapper mapper
) : ICommandHandler<CreateCouponCommand, Result<CouponDto>>,
    ICommandHandler<UpdateCouponCommand, Result<CouponDto>>,
    ICommandHandler<DeleteCouponCommand, Result<bool>>
{
    public async Task<Result<CouponDto>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request.CouponDto);
        await context.AddAsync(coupon, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? Result.Ok(request.CouponDto)
            : Result.Fail<CouponDto>("DB exception");
    }

    public async Task<Result<CouponDto>> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request.CouponDto);
        context.Update(coupon);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? Result.Ok(request.CouponDto)
            : Result.Fail<CouponDto>("DB exception");
    }

    public async Task<Result<bool>> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = await context.Coupons.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (coupon is null)
            return Result.Ok(true);

        context.Remove(coupon);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? Result.Ok(true)
            : Result.Fail<bool>("DB exception");
    }
}