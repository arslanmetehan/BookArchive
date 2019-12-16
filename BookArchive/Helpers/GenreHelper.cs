using BookArchive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookArchive.Helpers
{
	class GenreHelper
	{
		public void ListGenres(List<BookGenre> genres)
		{
			
			foreach (var genre in genres)
			{
				Console.WriteLine(genre.Id + " - " + genre.Name);
			}
		}
	}
}
