using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsApi.Domain
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string RecommendedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddedToInventory { get; set; }
    }
}
