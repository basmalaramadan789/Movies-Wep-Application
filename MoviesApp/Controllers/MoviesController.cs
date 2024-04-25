using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.DTOs;
using MoviesApp.Model;
using MoviesApp.Services;
using System.Reflection.Metadata.Ecma335;

namespace MoviesApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGeneresService _generesService;
        private new List<string> _allowedExtention = new List<string> { ".jpeg",".png",".jpg"};
        private long _MaxAllowedSize = 1048576;
        public MoviesController(IMoviesService moviesService,IGeneresService generesService)
        {
            _moviesService= moviesService;
            _generesService = generesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies=await _moviesService.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var movie = await _moviesService.GetById(id);
            if (movie == null)
                return NotFound();
            return Ok(movie);
        }

        [HttpGet("GetByGenreID")]
        public async Task<IActionResult> GetByGenreID(byte genreid)
        {
            var movies = await _moviesService.GetAll(genreid);
            return Ok(movies);

        }



        [HttpPost]
       public  async Task<IActionResult> create([FromForm]MoviesDTO dto)
        {
            if (dto.Poster == null)
            {
                return BadRequest("Poster Is Reqiered!");
            }
            if (!_allowedExtention.Contains(Path.GetExtension(dto.Poster.FileName.ToLower())))
                return BadRequest("only :.jpeg.png,.jpg");
            if(dto.Poster.Length>_MaxAllowedSize)
                return BadRequest("max allowed size for poster is 1mg");
            var IsValidGenre = await _generesService.IsValidGenre(dto.GenreId);
            if(!IsValidGenre)
                return BadRequest("invalid Genre Id");

            using var DataStream=new MemoryStream();
            await dto.Poster.CopyToAsync(DataStream);

            var movie=new Movie();
            movie.GenreId= dto.GenreId;
            movie.Title= dto.Title;
            movie.year = dto.year;
            movie.Rate= dto.Rate;
            movie.StoreLine= dto.StoreLine;
            movie.Poster= DataStream.ToArray();
            _moviesService.AddMovie(movie);
         
            return Ok(movie);



        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] MoviesDTO dto)
        {
            var movie = await _moviesService.GetById(id);
            if (movie == null)
                return NotFound("id not found");
            var IsValidGenre = await _generesService.IsValidGenre(dto.GenreId);
            if (!IsValidGenre)
                return BadRequest("invalid Genre Id");

            if (dto.Poster != null)
            {

                if (!_allowedExtention.Contains(Path.GetExtension(dto.Poster.FileName.ToLower())))
                    return BadRequest("only :.jpeg.png,.jpg");
                if (dto.Poster.Length > _MaxAllowedSize)
                    return BadRequest("max allowed size for poster is 1mg");
                using var DataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(DataStream);
                movie.Poster = DataStream.ToArray();
            }
            movie.Title = dto.Title;
            movie.StoreLine= dto.StoreLine;
            movie.Rate = dto.Rate;
            movie.year = dto.year;
            movie.GenreId=dto.GenreId;
           _moviesService.Update(movie);
            return Ok(movie);


        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _moviesService.GetById(id);
            if (movie == null)
                return NotFound("id not found");

            _moviesService.Delete(movie);
            return Ok(movie);
        }



    }
    

}
