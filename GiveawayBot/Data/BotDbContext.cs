using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiveawayBot.Data
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BotDbContext()
        {
        }


    }
}
