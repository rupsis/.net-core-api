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
    [Route("api/authorcollections")]
    public class AuthorCollectionController : Controller
    {

        private ILibraryRepository _libraryRepository;

        public AuthorCollectionController(ILibraryRepository libraryRepository)
        {
            _libraryRepository = libraryRepository;
        }

        [HttpPost]
        public IActionResult CreateAuthorCollection([FromBody]
        IEnumerable<Core.Models.AuthorForCreation> authorCollection)
        {

            if (authorCollection == null)
            {
                return BadRequest();
            }

            var authorEntities = Mapper.Map<IEnumerable<API.Entities.Author>>(authorCollection);

           
            foreach (var author in authorEntities)
            {
                _libraryRepository.AddAuthor(author);
            }
            
            if (!_libraryRepository.Save())
            {
                throw new Exception("Creating an author failed on save");
            }


            // Need to return location header and 201 status
            return Ok();
        }
    }
}
