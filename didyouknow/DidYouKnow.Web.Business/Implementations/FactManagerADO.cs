using DidYouKnow.Web.Business.Interfaces;
using DidYouKnow.Web.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Web.Business.Implementations
{
	public class FactManagerADO : IFactManager
	{
		private ICategoryManager _categoryManager;

		public FactManagerADO(ICategoryManager categoryManager)
		{
			this._categoryManager = categoryManager;
		}

		public async Task<Fact> GetRandomFact(int categoryId = 0)
		{
			using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DidYouKnow"].ConnectionString))
			{
				connection.Open();
				MySqlCommand cmd = new MySqlCommand(string.Format("SELECT {0} FROM facts ORDER BY RAND() LIMIT 0,1", SelectPortion()), connection);
				Fact fact = null;
				using (MySqlDataReader rdr = cmd.ExecuteReader())
				{
					while (rdr.Read())
					{
						fact = ParseFact(rdr);
						fact.Category = await this._categoryManager.GetCategory((int)rdr[5]);
						break;
					}
				}
				return fact;
			}
		}

		public Task<Fact[]> GetFacts()
		{
			throw new NotImplementedException();
		}

		public Task AddFact(Fact fact, Category category)
		{
			throw new NotImplementedException();
		}

		private Fact ParseFact(MySqlDataReader reader)
		{
			return new Fact()
			{
				Id = (int)reader[0],
				FactName = (string)reader[1],
				Source = (string)reader[2],
				Created = (DateTime)reader[3],
				LastModified = (DateTime)reader[4]
			};
		}

		private string SelectPortion()
		{
			return "id, fact, source, created, last_modified, category_id";
		}
	}
}
