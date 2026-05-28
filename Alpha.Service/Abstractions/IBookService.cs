
using System;

using System.Collections.Generic;

using System.Threading.Tasks;

using Alpha.Common.Models;

namespace Alpha.Service.Interfaces
{
    public interface IBookService
    {
        Task<List<Books>> GetBooks(
            int pageNumber,
            int pageSize);

        Task<Books> GetBookById(Guid bookId);

        Task<bool> InsertBook(Books book);

        Task<bool> UpdateBook(Books book);

        Task<bool> DeleteBook(Guid bookId);
    }
}

