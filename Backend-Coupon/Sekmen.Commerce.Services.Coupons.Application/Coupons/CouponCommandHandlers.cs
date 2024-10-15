namespace Sekmen.Commerce.Services.Coupons.Application.Coupons;

public record CreateCouponCommand(CouponDto CouponDto) : ICommand<bool>;
public record UpdateCouponCommand(CouponDto CouponDto) : ICommand<bool>;
public record DeleteCouponCommand(int Id) : ICommand<bool>;

internal sealed class CreateCouponCommandHandler(
    CouponDbContext context,
    IMapper mapper
) : ICommandHandler<CreateCouponCommand, bool>,
    ICommandHandler<UpdateCouponCommand, bool>,
    ICommandHandler<DeleteCouponCommand, bool>
{
    public async Task<bool> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request.CouponDto);
        _ = await context.AddAsync(coupon, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<bool> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request.CouponDto);
        _ = context.Update(coupon);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = await context.Coupons.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (coupon is null)
            return true;

        _ = context.Remove(coupon);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}