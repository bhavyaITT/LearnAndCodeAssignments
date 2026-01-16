public class OrderProcessor
{
    private readonly IPaymentGateway _paymentGateway;
    private readonly IInventoryService _inventoryService;
    private readonly INotificationService _notificationService;

    public OrderProcessor(IPaymentGateway paymentGateway,
        IInventoryService inventoryService,
        INotificationService notificationService)
    {
        _paymentGateway = paymentGateway;
        _inventoryService = inventoryService;
        _notificationService = notificationService;
    }

    public async Task<OrderResult> ProcessOrder(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        if (!IsValidOrder(order))
        {
            return OrderResult.Invalid("Order validation failed");
        }

        bool hasInventory = await _inventoryService.CheckAvailability(order.Items);

        if (!hasInventory)
        {
            return OrderResult.Failed("Insufficient inventory");
        }

        await _inventoryService.ReserveItems(order.Items);

        try
        {
            var paymentResult = await _paymentGateway.ProcessPayment(
                order.CustomerId,
                order.TotalAmount,
                order.PaymentMethod);

            if (paymentResult.IsSuccessful)
            {
                await _inventoryService.CommitReservation(order.Items);

                await _notificationService.SendOrderConfirmationEmail(order);

                return OrderResult.Success(paymentResult.TransactionId);
            }
            else
            {
                await _inventoryService.ReleaseReservation(order.Items);

                return OrderResult.Failed($"Payment failed: {paymentResult.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            await _inventoryService.ReleaseReservation(order.Items);

            Console.WriteLine($"Error: {ex.Message}");

            throw;
        }
    }

    private bool IsValidOrder(Order order)
    {
        return order.Items?.Count > 0 && order.TotalAmount > 0;
    }

    public async Task CancelOrder(string orderId)
    {

        var order = await GetOrderById(orderId);

        if (order.Status == OrderStatus.Paid)
        {
            await _paymentGateway.RefundPayment(order.TransactionId);

            await _inventoryService.RestoreInventory(order.Items);
        }

        order.Status = OrderStatus.Cancelled;

        await SaveOrder(order);
    }

    private async Task<Order> GetOrderById(string orderId)
    {
        return await Task.FromResult(new Order());
    }

    private async Task SaveOrder(Order order)
    {
        await Task.CompletedTask;
    }
}

// Reflection on learning:-
// while reading the code, I got to know how can comments be misleading, noisy and redundant
// we should not add them, until there's a intent, edge case or some important imformation
// adding noisy and redundant comments not only consumes space,s as well as it also takes a 
// lot of time of developers. With these comments it obviously took me more time to read, than 
// without them.