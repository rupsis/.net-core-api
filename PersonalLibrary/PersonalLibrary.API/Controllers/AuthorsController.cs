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
    public class AuthorsController : Controller
    {
        private ILibraryRepository _libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpGet("api/authors")]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _libraryRepository.GetAuthors();

            var authors = Mapper.Map<IEnumerable<Core.Models.Author>>(authorsFromRepo);
                
            return new JsonResult(authors);
        }
    }
}
