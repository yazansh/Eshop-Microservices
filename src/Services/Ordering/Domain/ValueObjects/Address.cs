namespace Domain.ValueObjects;

public record Address
{
    //(string Country, string City, string Street, string Address1, string Address2, string ZipCode);

    public string Country { get; private set; } = default!;

    public string City { get; private set; } = default!;

    public string Region { get; private set; } = default!;

    public string PostalCode { get; private set; } = default!;

    public string Street { get; private set; } = default!;

    public string ZipCode { get; private set; } = default!;


    private Address(string country, string city, string region, string postalCode, string street, string zipCode)
    {
        Country = country;
        City = city;
        Region = region;
        PostalCode = postalCode;
        Street = street;
        ZipCode = zipCode;
    }

    public static Address Of(string country, string city, string region, string postalCode, string street, string zipCode)
    {
        // TODO: add required validations!

        return new Address(country, city, region, postalCode, street, zipCode);
    }
}