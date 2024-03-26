namespace AwesomeShop.Services.Orders.Domain.ValueObjects;

public record PaymentAddress(string Street, string Number, string City, string State, string ZipCode);
