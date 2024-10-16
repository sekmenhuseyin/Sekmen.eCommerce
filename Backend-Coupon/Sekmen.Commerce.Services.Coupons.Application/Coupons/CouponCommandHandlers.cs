namespace Sekmen.Commerce.Services.Coupons.Application.Coupons;

public record CreateCouponCommand(CouponDto CouponDto) : ICommand<ResponseDto<CouponDto>>;
public record UpdateCouponCommand(CouponDto CouponDto) : ICommand<ResponseDto<CouponDto>>;
public record DeleteCouponCommand(int Id) : ICommand<ResponseDto<bool>>;

internal sealed class CreateCouponCommandHandler(
    CouponDbContext context,
    IMapper mapper
) : ICommandHandler<CreateCouponCommand, ResponseDto<CouponDto>>,
    ICommandHandler<UpdateCouponCommand, ResponseDto<CouponDto>>,
    ICommandHandler<DeleteCouponCommand, ResponseDto<bool>>
{
    public async Task<ResponseDto<CouponDto>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request.CouponDto);
        _ = await context.AddAsync(coupon, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? ResponseDto<CouponDto>.Success(request.CouponDto)
            : ResponseDto<CouponDto>.NotFound();
    }

    public async Task<ResponseDto<CouponDto>> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = mapper.Map<Coupon>(request.CouponDto);
        _ = context.Update(coupon);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? ResponseDto<CouponDto>.Success(request.CouponDto)
            : ResponseDto<CouponDto>.Error("error");
    }

    public async Task<ResponseDto<bool>> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = await context.Coupons.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);
        if (coupon is null)
            return ResponseDto<bool>.Success(true);

        _ = context.Remove(coupon);
        var result = await context.SaveChangesAsync(cancellationToken);
        return result > 0
            ? ResponseDto<bool>.Success(true)
            : ResponseDto<bool>.Error("error");
    }
}