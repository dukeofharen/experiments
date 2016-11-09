using Book.Data.Exceptions;
using Book.Data.Helpers;
using Book.Data.Interfaces;
using Book.Data.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Storage
{
	public class StorageBookManager : IBookManager
	{
		private List<Line> _lines = new List<Line>();

		public StorageBookManager()
		{
			this.Initialize();
		}

		public Task<bool> AddLine(double amount, DateTime dateTime, string accountName, string description, string category)
		{

			Line line = new Line()
			{
				Id = this.GetNewId(),
				AccountName = accountName,
				Amount = amount,
				Category = category,
				DateTime = dateTime,
				Description = description
			};
			this._lines.Add(line);
			string json = SerializeHelpers.Serialize<Line>(line);
			File.WriteAllText(Path.Combine(Executor.BookPath, string.Format("{0}.json", line.Id)), json);

			return Task.FromResult(true);
		}

		public Task<bool> UpdateLine(int id, double amount, DateTime dateTime, string accountName, string description, string category)
		{
			Line line = this._lines.Where(l => l.Id == id).FirstOrDefault();
			if (line == null)
			{
				throw new BookNotFoundException(string.Format("line with id {0}", id));
			}
			line.Amount = amount;
			line.DateTime = dateTime;
			line.AccountName = accountName;
			line.Description = description;
			line.Category = category;
			return Task.FromResult(true);
		}

		public Task<Line> GetLine(int id)
		{
			Line line = this._lines.Where(l => l.Id == id).FirstOrDefault();
			if (line == null)
			{
				throw new BookNotFoundException(string.Format("line with id {0}", id));
			}
			return Task.FromResult(line);
		}

		public Task<Line[]> GetLines(DateTime? from = null, DateTime? to = null)
		{
			var query = this._lines.AsQueryable();
			if (from.HasValue)
			{
				query = query.Where(l => l.DateTime >= from.Value);
			}
			if (to.HasValue)
			{
				query = query.Where(l => l.DateTime <= to.Value);
				query.OrderBy(l => l.DateTime);
			}
			return Task.FromResult(query.ToArray());
		}

		public Task DeleteLine(int id)
		{
			return Task.Run(() =>
			{
				Line line = this._lines.Where(l => l.Id == id).FirstOrDefault();
				if (line == null)
				{
					throw new BookNotFoundException(string.Format("line with id {0}", id));
				}
				File.Delete(Path.Combine(Executor.BookPath, string.Format("{0}.json", id)));
				this._lines.Remove(line);
			});
		}

		private void Initialize()
		{
			

			string[] files = Directory.GetFiles(Executor.BookPath, "*.json");
			foreach (string file in files)
			{
				Line line = SerializeHelpers.Deserialize<Line>(File.ReadAllText(file));
				if (line != null)
				{
					this._lines.Add(line);
				}
			}
		}

		private int GetNewId()
		{
			if (this._lines.Count == 0)
			{
				return 1;
			}
			return this._lines.Max(l => l.Id) + 1;
		}


		public Task<string[]> GetCategories()
		{
			return Task.FromResult(this._lines.Select(l => l.Category).Distinct().ToArray());
		}
	}
}
