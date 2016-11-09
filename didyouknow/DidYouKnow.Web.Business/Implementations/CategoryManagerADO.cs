using DidYouKnow.Web.Business.Interfaces;
using DidYouKnow.Web.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Web.Business.Implementations
{
	public class CategoryManagerADO : ICategoryManager
	{
		public Task<Category> GetCategory(int categoryId)
		{
			return Task.Run(() =>
			{
				using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DidYouKnow"].ConnectionString))
				{
					connection.Open();
					MySqlCommand cmd = new MySqlCommand(string.Format("SELECT {0} FROM categories WHERE id = {1}", SelectPortion(), categoryId), connection);
					Category category = null;
					using (MySqlDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							category = ParseCategory(rdr);
							break;
						}
					}
					return category;
				}
			});
		}

		public Task<Category[]> GetCategories()
		{
			return Task.Run(() =>
			{
				using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["DidYouKnow"].ConnectionString))
				{
					connection.Open();
					MySqlCommand cmd = new MySqlCommand(string.Format("SELECT {0} FROM categories", SelectPortion()), connection);
					List<Category> categories = new List<Category>();
					using (MySqlDataReader rdr = cmd.ExecuteReader())
					{
						while (rdr.Read())
						{
							categories.Add(ParseCategory(rdr));
						}
					}
					return categories.ToArray();
				}
			});
		}

		private Category ParseCategory(MySqlDataReader reader)
		{
			return new Category()
			{
				Id = (int)reader[0],
				Name = (string)reader[1],
				Description = (string)reader[2],
				Image = (string)reader[3],
				Created = (DateTime)reader[4],
				LastModified = (DateTime)reader[5]
			};
		}

		private string SelectPortion()
		{
			return "id, name, description, image, created, last_modified";
		}
	}
}
