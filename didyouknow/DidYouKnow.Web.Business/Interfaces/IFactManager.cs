using DidYouKnow.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Web.Business.Interfaces
{
	public interface IFactManager
	{
		Task<Fact> GetRandomFact(int categoryId = 0);
		Task<Fact[]> GetFacts();
		Task AddFact(Fact fact, Category category);
	}
}
