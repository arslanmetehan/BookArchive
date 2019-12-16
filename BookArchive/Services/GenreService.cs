using BookArchive.Entities;
using BookArchive.Extensions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookArchive.Services
{
	public class GenreService
	{

		public BookGenre AddGenre(string genreName,string connString)
		{
			using (var Connection = new NpgsqlConnection(connString))
			{
				Connection.Open();
				var sql = "insert into \"BookGenre\" (\"Name\") values('" + genreName + "') returning \"ID\"";
				NpgsqlCommand cmd = new NpgsqlCommand(sql, Connection);
				int id = (int)cmd.ExecuteScalar();

				Connection.Close();
				BookGenre newGenre = new BookGenre();
				newGenre.Id = id;
				newGenre.Name = genreName;
				return newGenre;

			}
		}
		public List<BookGenre> GetAllGenres(string connString)
		{
			using (var Connection = new NpgsqlConnection(connString))
			{
				Connection.Open();
				NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT * FROM \"BookGenre\"", Connection);

				var myTable = new DataTable();
				da.Fill(myTable);
				List<BookGenre> bookGenres = new List<BookGenre>();
				foreach (DataRow row in myTable.Rows)
				{
					BookGenre bookGenre = new BookGenre();
					bookGenre.Id = row["Id"].ToString().ToInt();
					bookGenre.Name = row["Name"].ToString();

					bookGenres.Add(bookGenre);

				}

				Connection.Close();
				return bookGenres;
			}
		}
		public void DeleteGenre(int willBeDeletedId, string connString)
		{
			using (var Connection = new NpgsqlConnection(connString))
			{
				Connection.Open();

				var sql = "DELETE FROM \"BookGenre\" WHERE \"ID\"=" + willBeDeletedId + "; ";
				NpgsqlCommand cmd = new NpgsqlCommand(sql, Connection);
				cmd.ExecuteNonQuery();

				Connection.Close();
			}

		}
		public void UpdateGenre(int willBeUpdatedId, string name,string connString)
		{
			using (var Connection = new NpgsqlConnection(connString))
			{
				Connection.Open();
				var sql = "update \"BookGenre\" set \"Name\" ='"+name+"' WHERE \"ID\"=" + willBeUpdatedId + "; ";
				NpgsqlCommand cmd = new NpgsqlCommand(sql, Connection);
				cmd.ExecuteNonQuery();
				Connection.Close();
			}

		}

	}
}
