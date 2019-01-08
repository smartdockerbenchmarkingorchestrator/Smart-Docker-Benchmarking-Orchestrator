using Docker.Benchmarking.Orchestrator.Core.Interfaces;
using Hangfire.Storage;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Benchmarking.Orchestrator.Web.Hubs
{
    public class AWSHub : Hub
    {
        private readonly IAWSResourcesService _resourcesService;
        public AWSHub(IAWSResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }

        public async Task SubscribeTestTemplate(string stackName, Guid credentialsId)
        {
            if (string.IsNullOrEmpty(stackName)) throw new ArgumentNullException("");

            if (credentialsId == Guid.Empty) throw new ArgumentNullException("");

            await Groups.AddToGroupAsync(Context.ConnectionId, stackName);

            var ip = await _resourcesService.GetIpForDeployedStack(stackName, credentialsId);
            await Clients.Group(stackName).SendAsync("progress", ip);
        }

        //private async Task PerformBackgroundJob(string jobId)
        //{
        //    for (int i = 0; i <= 100; i += 5)
        //    {
        //        await _hubContext.Clients.Group(jobId).SendAsync("progress", i);

        //        await Task.Delay(100);
        //    }
        //}

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
