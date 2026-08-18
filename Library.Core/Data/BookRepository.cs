using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Library.Core.Models;

namespace Library.Core.Data
{
    public class BookRepository
    {
        private readonly string _connectionString;

        public BookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Book> GetAll()
        {
            var books = new List<Book>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.Book_GetAll", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = (int)reader["Id"],
                            Title = reader["Title"].ToString(),
                            Author = reader["Author"].ToString(),
                            PublishYear = reader["PublishYear"] == DBNull.Value ? (int?)null : (int)reader["PublishYear"],
                            ISBN = reader["ISBN"] == DBNull.Value ? null : reader["ISBN"].ToString(),
                            Genre = reader["Genre"] == DBNull.Value ? null : reader["Genre"].ToString(),
                            Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                            Contents = reader["Contents"] == DBNull.Value ? null : reader["Contents"].ToString()
                        });
                    }
                }
            }

            return books;
        }

        public Book GetById(int id) 
        { 
            using (var connection = new SqlConnection(_connectionString)) 
            using (var command = new SqlCommand("dbo.Book_GetById", connection)) 
            { 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id; 
                
                connection.Open(); 
                using (var reader = command.ExecuteReader()) 
                { 
                    if (!reader.Read())
                        return null; 
                    
                    return new Book 
                    { 
                        Id = (int)reader["Id"], 
                        Title = reader["Title"].ToString(), 
                        Author = reader["Author"].ToString(), 
                        PublishYear = reader["PublishYear"] == DBNull.Value ? (int?)null : (int)reader["PublishYear"], 
                        ISBN = reader["ISBN"] == DBNull.Value ? null : reader["ISBN"].ToString(), 
                        Genre = reader["Genre"] == DBNull.Value ? null : reader["Genre"].ToString(), 
                        Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(), 
                        Contents = reader["Contents"] == DBNull.Value ? null : reader["Contents"].ToString() 
                    }; 
                } 
            } 
        }

        public int Insert(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.Book_Insert", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = book.Title;
                command.Parameters.Add("@Author", SqlDbType.NVarChar, 200).Value = book.Author;
                command.Parameters.Add("@PublishYear", SqlDbType.Int).Value = (object)book.PublishYear ?? DBNull.Value;
                command.Parameters.Add("@ISBN", SqlDbType.NVarChar, 20).Value = (object)book.ISBN ?? DBNull.Value;
                command.Parameters.Add("@Genre", SqlDbType.NVarChar, 100).Value = (object)book.Genre ?? DBNull.Value;
                command.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = (object)book.Description ?? DBNull.Value;
                command.Parameters.Add("@Contents", SqlDbType.Xml).Value = (object)book.Contents ?? DBNull.Value;

                connection.Open();

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Book book)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.Book_Update", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Id", SqlDbType.Int).Value = book.Id;
                command.Parameters.Add("@Title", SqlDbType.NVarChar, 200).Value = book.Title;
                command.Parameters.Add("@Author", SqlDbType.NVarChar, 200).Value = book.Author;
                command.Parameters.Add("@PublishYear", SqlDbType.Int).Value = (object)book.PublishYear ?? DBNull.Value;
                command.Parameters.Add("@ISBN", SqlDbType.NVarChar, 20).Value = (object)book.ISBN ?? DBNull.Value;
                command.Parameters.Add("@Genre", SqlDbType.NVarChar, 100).Value = (object)book.Genre ?? DBNull.Value;
                command.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = (object)book.Description ?? DBNull.Value;
                command.Parameters.Add("@Contents", SqlDbType.Xml).Value = (object)book.Contents ?? DBNull.Value;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("dbo.Book_Delete", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public List<Chapter> GetContents(int bookId)
        {
            var result = new List<Chapter>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("Book_GetContents", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", bookId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Chapter
                        {
                            Number = reader.GetInt32(reader.GetOrdinal("Number")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Page = reader.GetInt32(reader.GetOrdinal("Page"))
                        });
                    }
                }
            }

            return result;
        }
    }
}