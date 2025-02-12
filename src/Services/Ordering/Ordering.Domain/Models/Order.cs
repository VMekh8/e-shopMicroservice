using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models;

public class Order : Aggregate<Guid>
{
    private readonly IList<OrderItem> _orderItems = [];

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Guid CustomerId { get; private set; } = Guid.Empty;

    public string OrderName { get; set; } = string.Empty;

    public Address ShippingAddress { get; set; } = null!;

    public Address BillingAddress { get; set; } = null!;

    public Payment Payment { get; set; } = null!;

    public OrderStatus Status { get; set; } = null!;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }
}