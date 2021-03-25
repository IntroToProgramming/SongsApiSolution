using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsApi.Controllers
{
    public class SongsController : ControllerBase
    {
        private SongsDataContext _context;

        public SongsController(SongsDataContext context)
        {
            _context = context;
        }

        [HttpGet("/songs")]
        public async Task<ActionResult> GetAllSongs()
        {
            var response = await _context.Songs
                .Where(song => song.IsActive == true)
                .OrderBy(song => song.Title).ToListAsync();

            return Ok(response);
        }
        [HttpGet("/songs2")]
        public ActionResult GetAllSongs2()
        {
            var response =  _context.Songs
                .Where(song => song.IsActive == true)
                .OrderBy(song => song.Title).ToListAsync();

            return Ok(response);
        }
    }
}
