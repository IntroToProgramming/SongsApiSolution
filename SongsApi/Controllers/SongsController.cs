using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SongsApi.Domain;
using SongsApi.Models.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace SongsApi.Controllers
{
    public class SongsController : ControllerBase
    {
        private SongsDataContext _context;
        private IMapper _mapper;
        private MapperConfiguration _config;

        public SongsController(SongsDataContext context, IMapper mapper, MapperConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
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
  
            var song = _mapper.Map<Song>(request);
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            // 3 - Return:
            //     - 201 Created Status Code
            //     - Give them a copy of the newly created resource.
            //     - Add a Location header with the URL of the newly created resource. 
            //          Location: http://localhost:1337/songs/5

            var response = _mapper.Map<GetSongResponse>(song);

            return CreatedAtRoute("songs#getasong", new { id = response.Id }, response);
        }

        [HttpGet("/songs")]
        public async Task<ActionResult> GetAllSongs()
        {
            var response = new GetSongsResponse();

            var data = await _context.GetActiveSongs()
                .ProjectTo<SongSummaryItem>(_config)
                .OrderBy(song => song.Title).ToListAsync();

            response.Data = data;

            return Ok(response);
        }
        
        [HttpGet("/songs/{id:int}", Name ="songs#getasong")]
        public async Task<ActionResult> GetASong(int id)
        {

            var response = await _context.GetActiveSongs()
                .Where(s => s.Id == id)
                .ProjectTo<GetSongResponse>(_config)
                .SingleOrDefaultAsync(); // A song or null

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
