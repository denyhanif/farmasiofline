/* ------------------------------------------------ */
/* Hub: MessageHub                                  */
/* ------------------------------------------------ */

/* Alfa Irawan                                      */
/* Siloam Software Engineering Team                 */
/* Sunday, 11 February 2018                         */
/* Version 2.0                                      */
/*                                                  */
/* Copyright @ 2018 Siloam                          */

/* ------------------------------------------------ */




using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models;


namespace Siloam.Service.EMRPharmacy.Hub
{
    
    
    public class MessageHub : Microsoft.AspNetCore.SignalR.Hub
    {

        public Task SendMessage(MessageNotifications messageNotification)
        {

            return Clients.All.InvokeAsync("Send", messageNotification);

        }
        
    }
    
}