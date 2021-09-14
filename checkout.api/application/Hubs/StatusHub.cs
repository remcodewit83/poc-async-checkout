using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Application.Hubs
{
    public class StatusHub: Hub
    {
        public Task JoinGroup(string orderId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, orderId);
        }
        public Task LeaveGroup(string orderId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, orderId);
        }
    }
}
