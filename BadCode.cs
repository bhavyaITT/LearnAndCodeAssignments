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

    // This method processes an order 
    public async Task<OrderResult> ProcessOrder(Order order)
    {
        // Check if order is null 
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        // Validate the order 
        if (!IsValidOrder(order))
        {
            return OrderResult.Invalid("Order validation failed");
        }

        // Check inventory 
        bool hasInventory = await _inventoryService.CheckAvailability(order.Items);

        // If no inventory, return failure 
        if (!hasInventory)
        {
            return OrderResult.Failed("Insufficient inventory");
        }

        // Reserve inventory 
        await _inventoryService.ReserveItems(order.Items);

        try
        {
            // Process payment 
            var paymentResult = await _paymentGateway.ProcessPayment(
                order.CustomerId,
                order.TotalAmount,
                order.PaymentMethod);

            // Check if payment succeeded 
            if (paymentResult.IsSuccessful)
            {
                // Update inventory 
                await _inventoryService.CommitReservation(order.Items);

                // Send confirmation email 
                await _notificationService.SendOrderConfirmation(order);

                // Return success 
                return OrderResult.Success(paymentResult.TransactionId);
            }
            else
            {
                // Payment failed, release inventory 
                await _inventoryService.ReleaseReservation(order.Items);

                // Return failure 
                return OrderResult.Failed($"Payment failed: {paymentResult.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            // Something went wrong 
            await _inventoryService.ReleaseReservation(order.Items);

            // Log the error 
            Console.WriteLine($"Error: {ex.Message}");

            // Throw it 
            throw;
        }
    }

    private bool IsValidOrder(Order order)
    {
        // TODO: Fix this later 
        return order.Items?.Count > 0 && order.TotalAmount > 0;
    }

    // Added by John on 12/15/2023 - needed for the new feature 
    public async Task CancelOrder(string orderId)
    {
        // Get the order 
        var order = await GetOrderById(orderId);

        // John says we need to refund here 
        if (order.Status == OrderStatus.Paid)
        {
            // Refund the payment 
            await _paymentGateway.RefundPayment(order.TransactionId);

            // Give back the items 
            await _inventoryService.RestoreInventory(order.Items);
        }

        // Update status 
        order.Status = OrderStatus.Cancelled;

        // This is important!!! 
        await SaveOrder(order);
    }

    // Gets order by ID 
    private async Task<Order> GetOrderById(string orderId)
    {
        // Implementation here 
        return await Task.FromResult(new Order());
    }

    // Saves the order 
    private async Task SaveOrder(Order order)
    {
        // Implementation here 
        await Task.CompletedTask;
    }
}


// This method processes an order - noise
// Check if order is null - noise
// Validate the order - noise
// Check inventory - noise
// If no inventory, return failure - redundant
// Reserve inventory - noise
// Process payment - noise
// Check if payment succeeded - noise
// Update inventory - redundant
// Send confirmation email - noise
// Return success - noise
// Payment failed, release inventory - noise
// Return failure - noise
// Something went wrong - redundant
// Log the error - noise
// Throw it - noise
// TODO: Fix this later - misleading
// Added by John on 12/15/2023 - needed for the new feature - Journal comment
// Get the order - misleading
// John says we need to refund here - noise
// Refund the payment - noise
// Give back the items - noise
// Update status - noise
// This is important!!! - misleading
// Gets order by ID - noise
// Implementation here - noise
// Saves the order - noise
// Implementation here - noise