namespace AwesomeShop.Services.Orders.Application.UseCases.AddOrder;

public class AddOrderOutput
{
    public Guid Id { get; private set; }

    public AddOrderOutput(Guid id)
    {
        Id = id;
    }
}
