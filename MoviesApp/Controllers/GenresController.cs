using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.DTOs;
using MoviesApp.Model;
using MoviesApp.Services;

namespace MoviesApp.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGeneresService _genresservic;
        public GenresController(IGeneresService genresservic)
        {
            _genresservic = genresservic;
        }
        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var geners=await _genresservic.GetAll();
            return Ok(geners);

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GeneresDTO userdto)
        {
            var genre=new Genre();
            genre.Name=userdto.Name;

            await _genresservic.AddGenre(genre);
            return Ok(genre);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(byte id,[FromBody] GeneresDTO userdto)
        {
            var genre = await _genresservic.GetById(id);
            if (genre == null)
                return NotFound($"no genre found with  id ,{id}");
            genre.Name=userdto.Name;
            _genresservic.Update(genre);
            return Ok(genre);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id) 
        {
            var genre = await _genresservic.GetById(id);
            if (genre == null)
                return NotFound($"no genre found with  id ,{id}");
            _genresservic.Delete(genre);
            return Ok();

        }

    }
}
