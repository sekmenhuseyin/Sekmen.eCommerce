namespace Sekmen.Commerce.Frontend.Application.Models.Carts;

public record CartViewModel(
    CartDto Cart = default!,
    IEnumerable<CartDetailDto> Items = default!
);