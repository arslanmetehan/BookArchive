using BookArchive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookArchive.Helpers
{
	class BookHelper
	{
		public void ListBooks(List<Book> books)
		{

			foreach (var book in books)
			{
				Console.WriteLine(book.Id + " - " + book.Title + " - " + book.Author);
			}
		}
	}
}
