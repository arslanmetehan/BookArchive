using BookArchive.Entities;
using BookArchive.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BookArchive.Extensions
{
	public static class HelperMethods
	{
		

		public static int ToInt(this string word)
		{
			int result = 0;
			try
			{
				result = Convert.ToInt32(word);
			}
			catch(Exception exp)
			{
				return 0;
			}
			return result;
		}
		public static bool IsThereBookId(int choosenId, List<Book> books)
		{
			
			foreach (var book in books)
			{
				if(book.Id == choosenId)
				{
					return false;
				}
			}
			return true;
		}
		public static bool IsThereGenreId(int choosenId, List<BookGenre> genres)
		{
			
			foreach (var genre in genres)
			{
				if (genre.Id == choosenId)
				{
					return false;
				}
			}
			return true;
		}
		public static bool CanBeDeleted(int willBeDeletedId,string connString)
		{
			BookService bookService = new BookService();
		
			var books = bookService.GetAllBooks(connString);
			foreach(var book in books)
			{
				if(book.GenreId==willBeDeletedId)
				{
					return false;
				}
			}
			return true;
		}
		public static string[] ReadLinesFromFile()
		{
			using (FileStream fileRes = File.OpenRead("idpassword.txt"))
			{
				string[] idAndPassword = new string[] { };
				idAndPassword = File.ReadAllLines("idpassword.txt");

				return idAndPassword;
			}


		}

	}

}
