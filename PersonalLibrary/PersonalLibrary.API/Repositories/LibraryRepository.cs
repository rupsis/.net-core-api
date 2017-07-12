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
            return _libraryContext.Authors.OrderBy(a => a.FirstName).ThenBy(a => a.LastName);
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            return _libraryContext.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();
        }

        public Book GetBookForAuthor(Guid authorId, Guid bookId)
        {
            return _libraryContext.Books.Where(b => b.AuthorId == authorId && b.Id == bookId).FirstOrDefault();
        }

        public IEnumerable<Book> GetBooksForAuthor(Guid authorId)
        {
            return _libraryContext.Books.Where(b => b.AuthorId == authorId).OrderBy(b => b.Title).ToList();
        }

        public bool Save()
        {
            return (_libraryContext.SaveChanges() >= 0);
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
