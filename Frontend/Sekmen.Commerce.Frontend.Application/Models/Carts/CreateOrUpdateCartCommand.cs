namespace Sekmen.Commerce.Frontend.Application.Models.Carts;

public record CreateOrUpdateCartCommand(string UserId, int ProductId, int Count);
public record ApplyCouponCommand(string UserId, string CouponCode);
