// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
namespace Sekmen.Commerce.Services.Auth.Domain;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}