using Guilded.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveawayBot.Services
{
    public class GiveawayServiceProvider
    {
        public string PickRandomWuinner(IList<MemberSummary> members)
        {
            var rnd = new Random();
            var index = rnd.Next(1, members.Count);
            var picked = members[index];
            return picked.Name;
        }
    }
}
