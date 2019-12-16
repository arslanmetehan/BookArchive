using BookArchive.Extensions;
using BookArchive.Helpers;
using BookArchive.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookArchive
{
	public class Program
	{
		static void Main(string[] args)
		{
			GenreService genreService = new GenreService();
			BookService bookService = new BookService();
			GenreHelper genreHelper = new GenreHelper();
			BookHelper bookHelper = new BookHelper();
			string[] idAndPass = HelperMethods.ReadLinesFromFile();
			string id = idAndPass[0];
			string password = idAndPass[1];
			string connString = "Server=127.0.0.1;port=5433;User Id="+ id + ";Password="+password+";Database=BookGenre;";
			
			do
			{
				Console.Clear();
				Console.WriteLine("1 - Kitap Listele");
				Console.WriteLine("2 - Kitap Sil");
				Console.WriteLine("3 - Kitap Ekle");
				Console.WriteLine("4 - Kitap Güncelle");
				Console.WriteLine("5 - Tür Listele");
				Console.WriteLine("6 - Tür Sil");
				Console.WriteLine("7 - Tür Ekle");
				Console.WriteLine("8 - Tür Güncelle");
				Console.WriteLine("0 - Çıkış");

				var userSelection = Console.ReadKey();
				if (userSelection.Key == ConsoleKey.D1)
				{

					var books = bookService.GetAllBooks(connString);
					bookHelper.ListBooks(books);
					var willBeDeletedBookId = Console.ReadLine().ToInt();
				}
				else if (userSelection.Key == ConsoleKey.D2)
				{
					var idCheckBook = false;
					var willBeDeletedBookId = 0;
					do
					{
						Console.WriteLine("Choose a book for delete");
						var books = bookService.GetAllBooks(connString);
						bookHelper.ListBooks(books);
						willBeDeletedBookId = Console.ReadLine().ToInt();
						idCheckBook = HelperMethods.IsThereBookId(willBeDeletedBookId, books);
						if (idCheckBook == true)
						{
							Console.WriteLine("Non ID you have chosen !");
							Console.WriteLine("Please try again.");

						}
					}
					while (idCheckBook);
					bookService.DeleteBook(willBeDeletedBookId,connString);
					Console.WriteLine("The Book you choose has been deleted !");
				}
				else if (userSelection.Key == ConsoleKey.D3)
				{
					Console.WriteLine("Enter Book Genre");
					Console.WriteLine("");
					var genres = genreService.GetAllGenres(connString);
					genreHelper.ListGenres(genres);
					var newBookGenreId = Console.ReadLine().ToInt();

					Console.WriteLine("Enter Book Title");
					var newBookTitle = Console.ReadLine();
					Console.WriteLine("Enter Author Name");
					var newAuthor = Console.ReadLine();
					bookService.AddBook(newBookGenreId, newBookTitle, newAuthor,connString);
					Console.WriteLine("New Book Added !");
				}
				else if(userSelection.Key == ConsoleKey.D4)
				{
					bool idCheckBook = false;
					var willBeUpdatedBookId = 0;
					do
					{
						Console.WriteLine("Choose a book for update");
						var books = bookService.GetAllBooks(connString);
						bookHelper.ListBooks(books);
						willBeUpdatedBookId = Console.ReadLine().ToInt();
						idCheckBook = HelperMethods.IsThereBookId(willBeUpdatedBookId, books);

						if (idCheckBook == true)
						{
							Console.WriteLine("Non ID you have chosen !");
							Console.WriteLine("Please try again.");

						}
					}
					while (idCheckBook);
					bool idCheckGenre = false;
					var willBeUpdatedGenreId = 0;
					do
					{
						
						Console.WriteLine("Choose a genre");
						var genres = genreService.GetAllGenres(connString);
						genreHelper.ListGenres(genres);
						willBeUpdatedGenreId = Console.ReadLine().ToInt();
						idCheckGenre = HelperMethods.IsThereGenreId(willBeUpdatedGenreId, genres);
						if (idCheckGenre == false)
						{
							
							Console.WriteLine("Non ID you have chosen !");
							Console.WriteLine("Please try again.");
							
						}
						
					}
					while (idCheckGenre);

					Console.WriteLine("Enter new title");
					var newTitle = Console.ReadLine();
					Console.WriteLine("Enter new author");
					var newAuthor = Console.ReadLine();

					bookService.UpdateBook(willBeUpdatedBookId, willBeUpdatedGenreId, newTitle, newAuthor,connString);
					Console.WriteLine("Update has been done successfully !");
				}
				else if (userSelection.Key == ConsoleKey.D5)
				{
					var genres = genreService.GetAllGenres(connString);
					genreHelper.ListGenres(genres);

				}
				else if (userSelection.Key == ConsoleKey.D6)
				{
					var idCheckGenre = false;
					var canBeDeleted = false;
					var willBeDeletedGenreId = 0;
					do
					{
						Console.WriteLine("Choose a genre for delete");
						var genres = genreService.GetAllGenres(connString);
						genreHelper.ListGenres(genres);
						willBeDeletedGenreId = Console.ReadLine().ToInt();
						idCheckGenre = HelperMethods.IsThereGenreId(willBeDeletedGenreId, genres);
						if (idCheckGenre == true)
						{
							Console.WriteLine("Non ID you have chosen !");
							Console.WriteLine("Please try again.");

						}
						canBeDeleted=HelperMethods.CanBeDeleted(willBeDeletedGenreId,connString);
						if(canBeDeleted==false)
						{
							Console.WriteLine("The Genre you selected could not be deleted because there are books of that genre");
							Console.WriteLine("Please try again!");
						}
					}
					while (idCheckGenre || !canBeDeleted);
					genreService.DeleteGenre(willBeDeletedGenreId,connString);
					Console.WriteLine("The Genre you choose has been deleted !");
				}
				else if(userSelection.Key == ConsoleKey.D7)
				{
					Console.WriteLine("Enter Genre Name");
					var newGenreName = Console.ReadLine();
					genreService.AddGenre(newGenreName,connString);
					Console.WriteLine("New Genre Added !");
				}
				else if(userSelection.Key == ConsoleKey.D8)
				{
					bool idCheckGenre = false;
					var willBeUpdatedGenreId = 0;
					do
					{
						Console.WriteLine("Choose a genre for update");
						var genres = genreService.GetAllGenres(connString);
						genreHelper.ListGenres(genres);
						willBeUpdatedGenreId = Console.ReadLine().ToInt();
						idCheckGenre = HelperMethods.IsThereGenreId(willBeUpdatedGenreId, genres);

						if (idCheckGenre == true)
						{
							Console.WriteLine("Non ID you have chosen !");
							Console.WriteLine("Please try again.");

						}
					}
					while (idCheckGenre);
					Console.WriteLine("Enter a genre name");
					var genresNewName = Console.ReadLine();
					genreService.UpdateGenre(willBeUpdatedGenreId, genresNewName,connString);
					Console.WriteLine("Update has been done successfully !");
				}
				Console.ReadKey();
			}
			while (true);
			

			
			
			
		}
	}
}
