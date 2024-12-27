namespace Basket.API.DTOs;
public record AddressDto : BasketCheckoutEventAddress
{
    [SetsRequiredMembers]
    public AddressDto(string firstName, string lastName, string? emailAddress, string addressLine, string country,
        string state, string zipCode) : base(firstName, lastName, emailAddress, addressLine, country, state, zipCode)
    {
    }
}