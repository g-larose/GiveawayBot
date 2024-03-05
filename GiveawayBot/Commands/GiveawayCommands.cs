using GiveawayBot.Services;
using Guilded.Base;
using Guilded.Base.Embeds;
using Guilded.Commands;
using Guilded.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveawayBot.Commands
{
    public class GiveawayCommands: CommandModule
    {
        [Command(Aliases = [ "giveaway", "ga" ])]
        [Description("giveaway command")]
        public async Task Giveaway(CommandEvent invokator, string command = "")
        {
            var authorId = invokator.Message.CreatedBy;
            var serverId = invokator.Message.ServerId;
            var author = await invokator.ParentClient.GetMemberAsync((HashId)serverId!, authorId);
            var embed = new Embed();
           

            //var permissions = await invokator.ParentClient.GetMemberPermissionsAsync((HashId)serverId!, authorId);
            if (author.Name.Trim().Equals("Kyniant") || author.Name.Trim().Equals("ACGroupholder12") || author.Name.Equals("async<RogueLabs>"))
            {
                if (command is null || command.Equals(""))
                {
                    await invokator.ReplyAsync($"{author.Name} I was expecting a command but found []. example commands are [ \"start\", \"stop\", \"pause\" ]");
                }
                else
                {
                    switch (command)
                    {
                        case "start":
                            {
                                var members = await invokator.ParentClient.GetMembersAsync((HashId)serverId!);
                               
                                var giveawayProvider = new GiveawayServiceProvider();
                                var picked = giveawayProvider.PickRandomWuinner(members);
                                embed.SetTitle($"{author.Name} started Giveaway!");
                                embed.SetDescription($"and the winner is **{picked}**");
                                embed.SetFooter($"{invokator.ParentClient.Name}");
                                embed.SetTimestamp(DateTime.Now);
                                await invokator.ReplyAsync(embed);
                            }
                            
                            break;
                        case "stop":
                            {
                                embed.SetTitle("");
                                await invokator.ReplyAsync(embed);
                            }
                            
                            break;
                        case "pause":
                            await invokator.ReplyAsync($"{author.Name} you used [pause] command");
                            break;
                        default:
                            await invokator.ReplyAsync($"{author.Name} sorry, I don't recognise that command");
                            break;

                    }
                }
            }
            else
            {
                await invokator.ReplyAsync($"{author.Name} it seems you **do not** have the correct permissions to run this command, command **ignored**");
            }
        }
    }
}
