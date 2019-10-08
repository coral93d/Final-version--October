using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AspNetRoleBasedSecurity
{
    [Authorize(Roles = "Admin")]
    public class NotificationHub : Hub
    {
      //  public void Hello()
      //  {
       //     Clients.All.hello();
      //  }
      //  [HubMethodName("sendMessages")]
       // public static void SendMessages()
       // {
           // IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
           // context.Clients.All.updateMessages();
      //  }
    }
}