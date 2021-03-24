using SongsApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsApi.Services
{
    public class HopeServerStatus : IProvideServerStatus
    {
        public GetStatusResponse GetMyStatus()
        {
            return new GetStatusResponse
            {
                Message = "Everything is operational.",
                LastChecked = DateTime.Now
            };
        }
    }
}
