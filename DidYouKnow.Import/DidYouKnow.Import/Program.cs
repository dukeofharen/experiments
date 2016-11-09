using DidYouKnow.Web.Business.Implementations;
using DidYouKnow.Web.Business.Interfaces;
using DidYouKnow.Web.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidYouKnow.Import
{
	class Program
	{
		static void Main(string[] args)
		{
			Process();
		}

		private static void Process()
		{
			using (IWebDriver driver = new ChromeDriver())
			{
				List<string> urls = new List<string>();

				//uselessfacts.net
				////urls.Add("http://uselessfacts.net/interesting-facts/");
				////urls.Add("http://uselessfacts.net/interesting-facts-2/");
				////urls.Add("http://uselessfacts.net/interesting-facts-3/");
				////urls.Add("http://uselessfacts.net/interesting-facts-4/");
				////urls.Add("http://uselessfacts.net/interesting-facts-5/");
				////urls.Add("http://uselessfacts.net/animal-facts/");
				////urls.Add("http://uselessfacts.net/food-facts/");
				////urls.Add("http://uselessfacts.net/sport-facts/");
				////urls.Add("http://uselessfacts.net/chuck-norris-facts/");
				//string format = "http://uselessfacts.net/page/{0}";
				////foreach (string url in urls)
				//for (int i = 79; i <= 85; i++)
				//{
				//	driver.Navigate().GoToUrl(string.Format(format, i));
				//	IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".entry-content > div"));
				//	foreach (IWebElement element in elements)
				//	{
				//		string text = element.Text;
				//		Task task = AddFact(text, "http://uselessfacts.net", 6);
				//		task.Wait();
				//	}
				//}

				//http://www.cs.cmu.edu/~bingbin/
				//driver.Navigate().GoToUrl("http://www.cs.cmu.edu/~bingbin/");
				//IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("font > p"));
				//HandleElements(elements, 6, "http://www.cs.cmu.edu/~bingbin/");

				//http://www.did-you-knows.com
				//string format = "http://www.did-you-knows.com/?page={0}";
				//for (int i = 0; i <= 52; i++)
				//{
				//	driver.Navigate().GoToUrl(string.Format(format, i));
				//	IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.ClassName("dykText"));
				//	HandleElements(elements, 6, "http://www.did-you-knows.com", "Did you know ");
				//}

				//http://www.didyouknow.cd/
				//urls.Add("http://www.didyouknow.cd/spectacles.htm");
				//urls.Add("http://www.didyouknow.cd/predictions.htm");
				//urls.Add("http://www.didyouknow.cd/melba.htm");
				//urls.Add("http://www.didyouknow.cd/beer.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/animals.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/art.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/body.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/food.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/inventions.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/money.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/movies.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/music.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/politics.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/sports.htm");
				//urls.Add("http://www.didyouknow.cd/fastfacts/statistics.htm");
				//foreach (string url in urls)
				//{
				//	driver.Navigate().GoToUrl(url);
				//	IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector(".page-content > p"));
				//	HandleElements(elements, 6, "http://www.didyouknow.cd/");
				//}

				//http://www.begent.org/funfact.htm
				//driver.Navigate().GoToUrl("http://www.begent.org/funfact.htm");
				//IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("td > p"));
				//HandleElements(elements, 6, "http://www.begent.org/funfact.htm");

				//http://bootstrike.com/LaughterHell/Misc/miscs13.php
				//driver.Navigate().GoToUrl("http://bootstrike.com/LaughterHell/Misc/miscs13.php");
				//IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("ol > li"));
				//HandleElements(elements, 6, "http://bootstrike.com/LaughterHell/Misc/miscs13.php");

				//http://www.thefactsite.com/2011/07/top-100-random-funny-facts.html
				//driver.Navigate().GoToUrl("http://www.thefactsite.com/2011/07/top-100-random-funny-facts.html");
				//IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.CssSelector("ol > li"));
				//HandleElements(elements, 6, "http://www.thefactsite.com/2011/07/top-100-random-funny-facts.html");

				//http://www.djtech.net/humor/useless_facts.htm
				string[] lines = File.ReadAllLines("djtech.txt");
				foreach (string line in lines)
				{
					if (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line))
					{
						Task task = AddFact(line, "http://www.djtech.net/humor/useless_facts.htm", 6);
						task.Wait();
					}
				}
			}
		}
		
		private static void HandleElements(IReadOnlyCollection<IWebElement> elements, int categoryId, string source, string preText = "")
		{
			foreach (IWebElement element in elements)
			{
				string text = element.Text;
				if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
				{
					try
					{
						if (!string.IsNullOrEmpty(preText))
						{
							text = string.Format("{0}{1}", preText, text);
						}
						Console.WriteLine(string.Format("Fact '{0}'", text));
						Task task = AddFact(text, source, 6);
						task.Wait();
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}
			}
		}

		private static async Task AddFact(string factName, string source, int categoryId)
		{
			IFactManager factManager = new FactManager();
			ICategoryManager categoryManager = new CategoryManager();

			Fact fact = new Fact()
			{
				FactName = factName,
				Created = DateTime.Now,
				LastModified = DateTime.MinValue,
				Source = source
			};

			await factManager.AddFact(fact, await categoryManager.GetCategory(categoryId));
		}
	}
}
