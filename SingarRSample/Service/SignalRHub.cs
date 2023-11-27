using Microsoft.AspNetCore.SignalR;
using SignalRSmple.Models;

namespace SignalRSmple.Service
{
    public class SignalRHub : Hub
    {
        public async Task InsertOrder(string foodId, string customerName)
        {
            OrderListModel.OrderModels = OrderListModel.OrderModels ?? new List<OrderModel>();
            var lastOrder = OrderListModel.OrderModels.OrderBy(s => s.Code).LastOrDefault();

            var order = new OrderModel
            {
                Code = lastOrder != null ? (lastOrder.Code + 1) : 1,
                CustomerName = customerName,
                FoodId = int.Parse(foodId),
            };

            //insert db
            OrderListModel.OrderModels.Add(order);
            await Clients.All.SendAsync("GetOrder", order);

        }

        public async Task OrderReady(string code)
        {
            await Clients.All.SendAsync("OrderReady", code);
        }

        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("CommutingUsers", ++OnlineUsers.OnlineUser).Wait();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            OnlineUsers.OnlineUser--;
            Clients.All.SendAsync("CommutingUsers", OnlineUsers.OnlineUser).Wait();
            return base.OnDisconnectedAsync(exception); 
        }
    }

}
