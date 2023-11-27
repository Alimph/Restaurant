using System.Reflection.Metadata.Ecma335;

namespace SignalRSmple.Models
{
    public static class OrderListModel
    {
        public static List<OrderModel> OrderModels { get; set; }
    }
    public class OrderModel
    {
        public int Code { get; set; }
        public int FoodId { get; set; }
        public string CustomerName { get; set; }
        public bool IsReady { get; set; }
    }

    public static class OnlineUsers
    {
        public static int OnlineUser { get; set; }
    }
}