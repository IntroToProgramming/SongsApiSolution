using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsApi.Domain
{
    public class SongsDataContext : DbContext
    {
        public SongsDataContext(DbContextOptions<SongsDataContext> options): base(options)
        {

        }
        public DbSet<Song> Songs { get; set; }
    }
}
