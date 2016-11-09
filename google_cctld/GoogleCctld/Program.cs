using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleCctld
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, long> dict = new Dictionary<string, long>();
			List<string> randomSearchTerms = new List<string>()
			{
				"php array", "php list", "php mysql", "php mongodb", "c# xunit", "entity framework", "orm classes c#", "java maven", "travel europe", "ruby",
				"python", "haskell", "reddit", "nu.nl", "wikipedia", "australia", "canada", "usa", "france", "belgium", "nigeria", "south africa", "israel"
			};
			Random random = new Random();

			Console.WriteLine("Starting Google search...");
			using (IWebDriver driver = new ChromeDriver())
			{
				driver.Navigate().GoToUrl("http://www.google.nl");

				List<string> cctlds = File.ReadLines("tlds.txt").Select(c => c.ToLowerInvariant()).OrderBy(c => Guid.NewGuid()).ToList<string>();

				int total = cctlds.Count;
				int i = 0;
				Console.WriteLine(string.Format("Going to parse {0} ccTLDs!", total));

				foreach (string cctld in cctlds)
				{
					IWebElement searchBox = driver.FindElement(By.Name("q"));
					Console.WriteLine(string.Format("Parsing ccTLD '{0}', {1} ccTLD(s) left", cctld, total - i));

					if (string.IsNullOrEmpty(cctld))
					{
						continue;
					}

					long number = 0;
					string query = string.Format("site:.{0}", cctld);
					int sleepTime = random.Next(1, 5) * random.Next(600, 1000);

					//do a random search
					if (random.Next(0, 10) == 8)
					{
						Console.WriteLine("Doing a random search...");
						searchBox.Clear();
						searchBox.SendKeys(randomSearchTerms[random.Next(0, randomSearchTerms.Count - 1)]);
						//searchBox.Submit();
						Thread.Sleep(sleepTime);
					}

					Console.WriteLine(string.Format("It's sleepy time for {0} milliseconds.", sleepTime));

					Thread.Sleep(sleepTime);
					searchBox.Clear();
					searchBox.SendKeys(query);
					searchBox.Submit();

					WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
					wait.Until((d) =>
					{
						return d.Title.ToLower().StartsWith(query);
					});

					try
					{
						IWebElement resultsDiv = driver.FindElement(By.Id("resultStats"));
						string[] array = Regex.Split(resultsDiv.Text, @"[^0-9\.]+").Where(c => c != "." && c.Trim() != "").ToArray<string>();
						if (array.Length > 0)
						{
							long.TryParse(array[0].Replace(".", string.Empty), out number);
						}
					}
					catch
					{
						number = 0;
					}
					Console.WriteLine(string.Format("ccTLD {0} has {1} result(s)", cctld, number));

					dict.Add(cctld, number);
					i++;
					Console.WriteLine();
				}

				Dictionary<string, long> newDict = new Dictionary<string, long>();
				foreach (KeyValuePair<string, long> pair in dict.OrderByDescending(key => key.Value))
				{
					newDict.Add(pair.Key, pair.Value);
				}

				StringBuilder builder = new StringBuilder();
				foreach (KeyValuePair<string, long> pair in newDict)
				{
					builder.Append(string.Format("{0}|{1}{2}", pair.Key, pair.Value, Environment.NewLine));
				}
				File.WriteAllText("result.txt", builder.ToString());

				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
			}
		}
	}
}
