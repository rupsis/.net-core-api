using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalLibrary.API.Entities;

namespace PersonalLibrary.API.Interfaces
{
    public interface ILibraryRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthor(Guid authorId);
        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        IEnumerable<Book> GetBooksForAuthor(Guid authorId);
        Book GetBookForAuthor(Guid authorId, Guid bookId);
        void AddBookForAuthor(Guid authorId, Book book);
        void UpdateBookForAuthor(Book book);
        void DeleteBookForAuthor(Book book);
        bool Save();
    }
}
