using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalLibrary.API.Repositories;

namespace PersonalLibrary.API.Controllers
{
    public class AuthorsController : Controller
    {
        private LibraryRepository _libraryRepository;

        public AuthorsController(LibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _libraryRepository.GetAuthors();
            return new JsonResult(authorsFromRepo);
        }
    }
}
