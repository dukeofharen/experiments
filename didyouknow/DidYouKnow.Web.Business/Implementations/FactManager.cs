using DidYouKnow.Web.Business.Interfaces;
using DidYouKnow.Web.Data;
using DidYouKnow.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DidYouKnow.Web.Business.Implementations
{
	public class FactManager : IFactManager
	{
		private Random _random;

		public FactManager()
		{
			this._random = new Random();
		}

		public Task<Fact> GetRandomFact(int categoryId = 0)
		{
			return Task.Run(() =>
			{
				using (var ctx = new DYDContext())
				{
					var query = ctx.Facts
								.Include(f => f.Category);
					if (categoryId != 0)
					{
						query = query.Where(f => f.Category.Id == categoryId);
					}
					Fact[] facts = query.ToArray();
					Fact fact = facts[_random.Next(facts.Length)];
					return fact;
				}
			});
		}

		public Task<Fact[]> GetFacts()
		{
			return Task.Run(() =>
			{
				using (var ctx = new DYDContext())
				{
					return ctx.Facts.ToArray();
				}
			});
		}


		public Task AddFact(Fact fact, Category category)
		{
			return Task.Run(() =>
			{
				using (var ctx = new DYDContext())
				{
					Category existingCategory = ctx.Categories.Where(c => c.Id == category.Id).FirstOrDefault();
					fact.Category = existingCategory;
					ctx.Facts.Add(fact);
					ctx.SaveChanges();
				}
			});
		}
	}
}
