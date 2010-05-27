using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Steps
{
	[Binding]
	public class AssertionSteps
	{
		[Then(@"^(\d+) instances? of (.*) should exist$")]
		public void ThenIShouldHaveExpectedInstancesOf(int expectedCount, string typeName)
		{
			var instances = ScenarioContext.Current.InstancesOf(typeName);
			var count = instances.Count();

			if (count != expectedCount)
			{
				throw new AssertionException("Expected {0} instances of {1} but was {2}", expectedCount, typeName, count);
			}
		}

		[Then(@"^an instance of (.+) with (\w+) (.+) should exist$")]
		public void ThenItShouldExistAnInstanceOfWithPropertyValue(string typeName, string propertyName, object value)
		{
			var instance = GetInstanceByPropertyValue(typeName, propertyName, value);

			if (instance == null)
			{
				throw new AssertionException("Not found instance of {0} with {1} {2}", typeName, propertyName, value);
			}
		}

		[Then(@"^an instance of (.+) with (\w+) (.+) should not exist$")]
		public void ThenItShouldNotExistAnInstanceOfWithId(string typeName, string propertyName, object value)
		{
			var instance = GetInstanceByPropertyValue(typeName, propertyName, value);

			if (instance != null)
			{
				throw new AssertionException("Found instance of {0} with {1} {2}", typeName, propertyName, value);
			}
		}

		private static object GetInstanceByPropertyValue(string typeName, string propertyName, object value)
		{
			var type = FeatureContext.Current.GetTypeByName(typeName);
			var propertyInfo = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
			if (propertyInfo == null)
			{
				throw new ArgumentException(String.Format("Property {0} not found in {1}", propertyName, typeName), "propertyName");
			}
			var property = propertyInfo.GetGetMethod();

			value = Convert.ChangeType(value, property.ReturnType);

			var instance = ScenarioContext.Current
				.InstancesOf(type)
				.FirstOrDefault(o => value.Equals(property.Invoke(o, new object[0])));
			
			return instance;
		}

		[Then(@"^(\d+) of the following instances? of (\w+) should exist:$")]
		public static void ThenNOfTheFollowingInstancesOfShouldExist(int count, string typeName, Table table)
		{
			if (MatchedInstances(typeName, table).Count() != count)
			{
				throw new AssertionException("There is a example instance of {0} that does not exist", typeName);
			}
		}

		[Then(@"^all of the following instances? of (\w+) should exist:$")]
		public static void ThenAllOfTheFollowingInstancesOfShouldExist(string typeName, Table table)
		{
			if (MatchedInstances(typeName, table).Count() != table.RowCount)
			{
				throw new AssertionException("There is a example instance of {0} that does not exist", typeName);
			}
		}

		[Then(@"^any of the following instances? of (\w+) should exist:$")]
		public static void ThenAnyOfTheFollowingInstancesOfShouldExist(string typeName, Table table)
		{
			if (!MatchedInstances(typeName, table).Any())
			{
				throw new AssertionException("There is no matched instance of {0}", typeName);
			}
		}

		[Then(@"^any of the following instances? of (\w+) should not exist:$")]
		[Then(@"^all of the following instances? of (\w+) should not exist:$")]
		public static void ThenAnyOrAllOfTheFollowingInstancesOfShouldNotExist(string typeName, Table table)
		{
			if (MatchedInstances(typeName, table).Any())
			{
				throw new AssertionException("There is a matched instance of {0}", typeName);
			}
		}

		private static IEnumerable<object> MatchedInstances(string typeName, Table table)
		{
			var type = FeatureContext.Current.GetTypeByName(typeName);

			var instances = ScenarioContext.Current.InstancesOf(type);

			var examples = table.Rows
				.Select(row => Mapper.Map(row, typeof (TableRow), type))
				.ToList();

			foreach (var example in examples)
			{
				foreach (var instance in instances)
				{
					var match = true;
					foreach (var header in table.Header)
					{
						var property = type.GetProperty(header).GetGetMethod();

						var exampleValue = property.Invoke(example, new object[0]);
						var instanceValue = property.Invoke(instance, new object[0]);

						if (!exampleValue.Equals(instanceValue))
						{
							match = false;
							break;
						}
					}
					if (match)
					{
						yield return instance;
					}
				}
			}
		}
	}
}
