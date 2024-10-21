namespace Sekmen.Commerce.Frontend.Application.Models.Carts;

public record CreateOrUpdateCartCommand(CartDto Cart, CartDetailDto Details);