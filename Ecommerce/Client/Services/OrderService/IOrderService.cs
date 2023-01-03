﻿namespace Ecommerce.Client.Services.OrderService;

public interface IOrderService
{
    Task PlaceOrder();
    Task<List<OrderOverviewResponse>> GetOrders();
    Task<OrderDetailsResponse> GetOrderDetails(int orderId);
}
