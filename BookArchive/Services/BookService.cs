using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookArchive.Entities;
using Npgsql;
using BookArchive.Extensions;

namespace BookArchive.Services
{
	public class BookService
	{
		public List<Book> GetAllBooks(string connString)
		{
			using (var Connection = new NpgsqlConnection(connString))
			{
				Connection.Open();
				NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT * FROM \"Book\"", Connection);




				var myTable = new DataTable();
				da.Fill(myTable);
				List<Book> books = new List<Book>();
				foreach(DataRow row in myTable.Rows)
				{
					Book book = new Book();
					book.Id = row["Id"].ToString().ToInt();
					book.GenreId = row["GenreId"].ToString().ToInt();
					book.Title = row["Title"].ToString();
					book.Author = row["Author"].ToString();

					books.Add(book);
					
				}
				
				Connection.Close();
				return books;
			} 
		}
		public Book AddBook(int genreId,string title, string author,string connString)
		{
			using (var Connection = new NpgsqlConnection(connString))
			{
				Connection.Open();
				var sql = "insert into \"Book\" (\"GenreId\",\"Title\",\"Author\") values("+ genreId + ",'"+title+ "','" + author + "') returning \"Id\"";
				NpgsqlCommand cmd = new NpgsqlCommand(sql, Connection);
				int id = (int)cmd.ExecuteScalar();

				Connection.Close();
				Book newBook = new Book();
				newBook.Id = id;
				newBook.GenreId = genreId;
				newBook.Title = title;
				newBook.Author = author;
				return newBook;

			}
		}
		public void UpdateBook(int willBeUpdatedId,int genreId, string title, string author,string connString)
		{
			using (var Connection = new NpgsqlConnection(connString))
			{
				Connection.Open();
				var sql = "update \"Book\" set \"GenreId\" = " + genreId + ",\"Title\"='" + title + "',\"Author\"='" + author + "' WHERE \"Id\"=" + willBeUpdatedId+"; ";
				NpgsqlCommand cmd = new NpgsqlCommand(sql, Connection);
				cmd.ExecuteNonQuery();
				Connection.Close();
			}

		}
		public void DeleteBook(int willBeDeletedId,string connString)
		{
			using (var Connection = new NpgsqlConnection(connString))
			{
				Connection.Open();

				var sql = "DELETE FROM \"Book\" WHERE \"Id\"=" + willBeDeletedId + "; ";
				NpgsqlCommand cmd = new NpgsqlCommand(sql, Connection);
				cmd.ExecuteNonQuery();
				
				Connection.Close();
			}

		}

	}

}
