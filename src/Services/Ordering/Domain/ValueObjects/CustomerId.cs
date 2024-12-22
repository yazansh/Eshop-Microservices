namespace Ordering.Domain.ValueObjects;
public record CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid id) => Value = id;

    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value.Equals(Guid.Empty))
            throw new DomainException("CustomerId cannot be empty");

        return new CustomerId(value);
    }
}
