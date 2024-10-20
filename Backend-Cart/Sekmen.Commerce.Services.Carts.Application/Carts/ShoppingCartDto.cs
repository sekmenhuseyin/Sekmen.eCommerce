namespace Sekmen.Commerce.Services.Carts.Application.Carts;

public record ShoppingCartDto(
    CartDto Cart,
    IEnumerable<CartDetailDto> Items
);