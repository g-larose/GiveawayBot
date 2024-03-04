using Guilded.Base;
using Guilded.Commands;
using Guilded.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveawayBot.Commands
{
    public class ModCommands: CommandModule
    {
        [Command(Aliases = [ "purge", "delete", "remove" ])]
        [Description("delete a set amount of messages from a channel")]
        public async Task Purge(CommandEvent invokator, uint amount = 0)
        {
            var authorId = invokator.Message.CreatedBy;
            var serverId = invokator.ServerId;
            var author = await invokator.ParentClient.GetMemberAsync((HashId)serverId!, authorId);
            var permissions = await invokator.ParentClient.GetMemberPermissionsAsync((HashId)serverId, authorId);
            var channelId = invokator.Message.ChannelId;

            if (!permissions.Contains(Permission.ManageMessages))
            {
                await invokator.ReplyAsync($"{author.Name} it seems you **do not** have the permissions needed to run this command.");
            }
            else
            {
                if (amount <= 100)
                {
                    var messages = await invokator.ParentClient.GetMessagesAsync(channelId, false, amount, null, null);
                    foreach (var m in messages)
                    {
                        await invokator.ParentClient.DeleteMessageAsync(channelId, m.Id);
                        await Task.Delay(200);
                    }
                }
                else
                {
                    await invokator.ReplyAsync($"{author.Name} the limit is 100 or less. you can set the limit to 1 - 100 by using the [Set] command");
                }
                
            }
        }
    }
}
