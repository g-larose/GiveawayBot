using GiveawayBot.Commands;
using GiveawayBot.Models;
using Guilded;
using Guilded.Base;
using Guilded.Commands;
using Guilded.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GiveawayBot
{
    public class Bot
    {
        private static readonly string? json = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "config.json"));
        private static readonly string? token = JsonSerializer.Deserialize<ConfigJson>(json!)!.Token!;
        private static readonly string? prefix = JsonSerializer.Deserialize<ConfigJson>(json!)!.Prefix!;
        private static readonly string? timePattern = "hh:mm:ss tt";

        public async Task RunAsync()
        {
            await using var client = new GuildedBotClient(token!)
                .AddCommands(new GiveawayCommands(), prefix!)
                .AddCommands(new ModCommands(), prefix!);

            client.Prepared
                .Subscribe(async me =>
                {
                    var time = DateTime.Now.ToString(timePattern);
                    var date = DateTime.Now.ToShortDateString();
                    Console.WriteLine($"[{date}][{time}][INFO]  [{me.ParentClient.Name}] listening for events...");
                    var defaultChannelGuid = new Guid("8c7de58d-fad1-46fb-8c60-1ea38ad68206");
                    //await me.ParentClient.CreateMessageAsync(defaultChannelGuid, $"{me.ParentClient.Name} is online");
                    var serverId = new HashId("R4mvm6Ml");
                   
                    var membersList = await me.ParentClient.GetMembersAsync(serverId);
                    
                });

            await client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
