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

            var authorCollectionToReturn = Mapper.Map<IEnumerable<Core.Models.Author>>(authorEntities);

            // create a comma seperated string of the collection ids
            var idsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id));

            // return the route of the newly created collection
            return CreatedAtRoute("GetAuthorCollection", new { ids = idsAsString }, authorCollectionToReturn);
        }

        [HttpGet("({ids})", Name = "GetAuthorCollection")]
        public IActionResult GetAuthorCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntites = _libraryRepository.GetAuthors(ids);

            if(ids.Count() != authorEntites.Count())
            {
                return NotFound();
            }

            var authorsToReturn = Mapper.Map<IEnumerable<Core.Models.Author>>(authorEntites);
            return Ok(authorsToReturn);
        }
    }
}
