using Book.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Data.Interfaces
{
	public interface IBookManager
	{
		Task<bool> AddLine(double amount, DateTime dateTime, string accountName, string description, string category);
		Task<bool> UpdateLine(int id, double amount, DateTime dateTime, string accountName, string description, string category);
		Task<Line> GetLine(int id);
		Task<Line[]> GetLines(DateTime? from = null, DateTime? to = null);
		Task DeleteLine(int id);
		Task<string[]> GetCategories();
	}
}
