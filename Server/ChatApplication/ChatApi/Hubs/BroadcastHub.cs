using ChatApi.Helpers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApi.Hubs
{
    public class BroadcastHub : Hub<IHubClient>
    {
        //public override async Task OnConnectedAsync()
        
        //{
        //    //ClaimTypes.Name

        //    // Context.User.FindFirst(ClaimTypes.Email)?.Value;
        //    var name = Context.User.FindFirst(ClaimTypes.Name)?.Value;


        //    ConnectedUser.Ids[Context.User.FindFirst(ClaimTypes.Name)?.Value] = Context.ConnectionId;
        //}
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{

        //    ConnectedUser.Ids.Remove(Context.User.Identity.Name);
        //}

    }
}
