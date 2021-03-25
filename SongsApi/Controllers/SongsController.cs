using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsApi.Domain;
using SongsApi.Models.Songs;
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


        [HttpPost("/songs")]
        public async Task<ActionResult> AddASong([FromBody] PostSongRequest request)
        {
            // 1. Validate the Entity
            //    - If not valid, send a 400 with or without some details about what they did wrong.
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400
            }
            // 2 - modify the domain - save it to the database.
            var song = new Song
            {
                Title = request.Title,
                Artist = request.Artist,
                RecommendedBy = request.RecommendedBy,
                IsActive = true,
                AddedToInventory = DateTime.Now
            };
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            // 3 - Return:
            //     - 201 Created Status Code
            //     - Give them a copy of the newly created resource.
            //     - Add a Location header with the URL of the newly created resource. 
            //          Location: http://localhost:1337/songs/5
            var response = new GetSASongResponse
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                RecommendedBy = song.RecommendedBy
            };

            return CreatedAtRoute("songs#getasong", new { id = response.Id }, response);
        }

        [HttpGet("/songs")]
        public async Task<ActionResult> GetAllSongs()
        {
            var response = new GetSongsResponse();

            var data = await _context.Songs
                .Where(song => song.IsActive == true)
                .Select(song => new SongSummaryItem
                {
                    Id = song.Id,
                    Title = song.Title,
                    Artist = song.Artist,
                    RecommendedBy = song.RecommendedBy
                })
                .OrderBy(song => song.Title).ToListAsync();

            response.Data = data;

            return Ok(response);
        }
        
        [HttpGet("/songs/{id:int}", Name ="songs#getasong")]
        public async Task<ActionResult> GetASong(int id)
        {

            var response = await _context.Songs
                .Where(s => s.IsActive && s.Id == id)
                .Select(s => new GetSASongResponse
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    RecommendedBy = s.RecommendedBy
                }).SingleOrDefaultAsync(); // A song or null

            if(response == null)
            {
                return NotFound();
            } else
            {
                return Ok(response);
            }
        }
        
        
    }
}
