
using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using Alpha.Common.Models;

using Alpha.Common.Utilities;

using Alpha.Service.Interfaces;

using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using System.Data;

namespace Alpha.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IConfiguration _configuration;

        public BookService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET BOOKS WITH PAGINATION
        public async Task<List<Books>> GetBooks(
        int pageNumber,
        int pageSize)
        {
            List<Books> books = new List<Books>();

            try
            {
                using SqlConnection con =
                    new SqlConnection(
                        _configuration.GetConnectionString(
                            "DefaultConnection"));

                using SqlCommand cmd =
                    new SqlCommand(
                        AppConstants.GetBooks,
                        con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                await con.OpenAsync();

                SqlDataReader reader =
                    await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    books.Add(new Books
                    {
                        BookId =
                        Guid.Parse(
                            reader["BookId"].ToString()!),

                        Title =
                        reader["Title"].ToString()!,

                        Author =
                        reader["Author"].ToString()!,

                        Price =
                        Convert.ToDecimal(
                            reader["Price"])
                    });
                }

                // PAGINATION
                books = books
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return books;
        }

        public async Task<Books> GetBookById(Guid bookId)
        {
            Books book = new Books();

            try
            {
                using SqlConnection con =
                    new SqlConnection(
                        _configuration.GetConnectionString(
                            "DefaultConnection"));

                using SqlCommand cmd =
                    new SqlCommand(
                        AppConstants.GetBookById,
                        con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    Parameters.BookId,
                    bookId);

                await con.OpenAsync();

                SqlDataReader reader =
                    await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    book.BookId =
                    Guid.Parse(
                        reader["BookId"].ToString()!);

                    book.Title =
                    reader["Title"].ToString()!;

                    book.Author =
                    reader["Author"].ToString()!;

                    book.Price =
                    Convert.ToDecimal(
                        reader["Price"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return book;
        }

        public async Task<bool> InsertBook(Books book)
        {
            try
            {
                using SqlConnection con =
                    new SqlConnection(
                        _configuration.GetConnectionString(
                            "DefaultConnection"));

                using SqlCommand cmd =
                    new SqlCommand(
                        AppConstants.InsertBook,
                        con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    Parameters.Title,
                    book.Title);

                cmd.Parameters.AddWithValue(
                    Parameters.Author,
                    book.Author);

                cmd.Parameters.AddWithValue(
                    Parameters.Price,
                    book.Price);

                await con.OpenAsync();

                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateBook(Books book)
        {
            try
            {
                using SqlConnection con =
                    new SqlConnection(
                        _configuration.GetConnectionString(
                            "DefaultConnection"));

                using SqlCommand cmd =
                    new SqlCommand(
                        AppConstants.UpdateBook,
                        con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    Parameters.BookId,
                    book.BookId);

                cmd.Parameters.AddWithValue(
                    Parameters.Title,
                    book.Title);

                cmd.Parameters.AddWithValue(
                    Parameters.Author,
                    book.Author);

                cmd.Parameters.AddWithValue(
                    Parameters.Price,
                    book.Price);

                await con.OpenAsync();

                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBook(Guid bookId)
        {
            try
            {
                using SqlConnection con =
                    new SqlConnection(
                        _configuration.GetConnectionString(
                            "DefaultConnection"));

                using SqlCommand cmd =
                    new SqlCommand(
                        AppConstants.DeleteBook,
                        con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    Parameters.BookId,
                    bookId);

                await con.OpenAsync();

                await cmd.ExecuteNonQueryAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

