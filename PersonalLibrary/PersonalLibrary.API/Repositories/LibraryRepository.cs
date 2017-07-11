using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalLibrary.API.Entities;
using PersonalLibrary.API.Interfaces;

namespace PersonalLibrary.API.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private LibraryContext _libraryContext;

        public LibraryRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        public void AddAuthor(Author author)
        {
            author.Id = new Guid();

            _libraryContext.Authors.Add(author);
        }

        public void AddBookForAuthor(Guid authorId, Book book)
        {
            var author = GetAuthor(authorId);
            if(author != null)
            {
                // generate id if none is provided
                if (book.Id == null)
                {
                    book.Id = new Guid();
                }
                author.Books.Add(book);
            }
        }

        public bool AuthorExists(Guid authorId)
        {
            return _libraryContext.Authors.Any(a => a.Id == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            _libraryContext.Authors.Remove(author);
        }

        public void DeleteBook(Book book)
        {
            _libraryContext.Books.Remove(book);
        }

        public void DeleteBookForAuthor(Book book)
        {
            _libraryContext.Books.Remove(book);
        }

        public Author GetAuthor(Guid authorId)
        {
            return _libraryContext.Authors.FirstOrDefault(a => a.Id == authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            throw new NotImplementedException();
        }

        public Book GetBookForAuthor(Guid authorId, Guid bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooksForAuthor(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookForAuthor(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
