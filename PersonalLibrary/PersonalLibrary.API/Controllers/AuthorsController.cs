using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalLibrary.API.Repositories;
using PersonalLibrary.API.Interfaces;
using PersonalLibrary.API.Helpers;
using AutoMapper;

namespace PersonalLibrary.API.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet()]
        public IActionResult GetAuthors()
        {

            var authorsFromRepo = _libraryRepository.GetAuthors();

            var authors = Mapper.Map<IEnumerable<Core.Models.Author>>(authorsFromRepo);

            return Ok(authors);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetAuthor(Guid id)
        {
            
            var authorFromRepo = _libraryRepository.GetAuthor(id);


            if (authorFromRepo == null)
            {
                return NotFound();
            }

            var author = Mapper.Map<Core.Models.Author>(authorFromRepo);
            return Ok(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] Core.Models.AuthorForCreation author)
        {
            
            if(author == null)
            {
                return BadRequest();
            }

            var authorEntity = Mapper.Map<API.Entities.Author>(author);

            // this adds the author to the dbcontext (db session)
            _libraryRepository.AddAuthor(authorEntity);

            if (!_libraryRepository.Save())
            {
                System.Console.WriteLine("DUR DE HERP");
                throw new Exception("Creating an author failed on save");
            }

            var authorToReturn = Mapper.Map<Core.Models.Author>(authorEntity);

            // return the route name, value, and object 
            return CreatedAtRoute("GetAuthor", new { id = authorToReturn.Id}, authorToReturn);
        }
    }
}
