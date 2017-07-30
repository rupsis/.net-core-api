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

        [HttpGet("{id}")]
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
    }
}
