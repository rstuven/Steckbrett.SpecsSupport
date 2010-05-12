using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Steps
{
	[Binding]
	public class AutoMapperSteps : ModelBindingBase
	{
		private const string TheFollowingInstancesOf = 
			@"^the following instances? of (\w+):$";
		[Given(TheFollowingInstancesOf)]
		[When(TheFollowingInstancesOf)]
		public static IList<object> DoTheFollowingInstancesOf(string typeName, Table table)
		{
			var type = GetTypeByName(typeName);

			// Don't forget this is evaluated in a deferred way.
			var rows = table.Rows
				.Select(row => Mapper.Map(row, typeof (TableRow), type));

			var all = InstancesOf(type);
			var result = new List<object>();

			foreach (var item in rows)
			{
				all.Add(item);
				result.Add(item);
			}

			return result;
		}

		private const string InstancesOfFromFile =
			@"^instances? of (\w+)\s+from file (.+)$";
		[Given(InstancesOfFromFile )]
		[When(InstancesOfFromFile )]
		public static void DoInstancesOfFromFile(string typeName, string file)
		{
			DoInstancesOfFromFileSkipTake(0, 0, typeName, file);
		}

		private const string InstancesOfFromFileSkip =
			@"^skip ([\d\w<>]+) instances? of (\w+)\s+from file (.+)$";
		[Given(InstancesOfFromFileSkip)]
		[When(InstancesOfFromFileSkip)]
		public static void DoInstancesOfFromFileSkip(int skip, string typeName, string file)
		{
			DoInstancesOfFromFileSkipTake(skip, 0, typeName, file);
		}

		private const string InstancesOfFromFileTake =
			@"^take ([\d\w<>]+) instances? of (\w+)\s+from file (.+)$";
		[Given(InstancesOfFromFileTake)]
		[When(InstancesOfFromFileTake)]
		public static void DoInstancesOfFromFileTake(int take, string typeName, string file)
		{
			DoInstancesOfFromFileSkipTake(0, take, typeName, file);
		}

		private const string InstancesOfFromFileSkipTake =
			@"^skip ([\d\w<>]+) and take ([\d\w<>]+) instances? of (\w+)\s+from file (.+)$";
		[Given(InstancesOfFromFileSkipTake)]
		[When(InstancesOfFromFileSkipTake)]
		public static IList<object> DoInstancesOfFromFileSkipTake(int skip, int take, string typeName, string file)
		{
			Table table;

			using (var reader = File.OpenText(file))
			{
				var header = reader.ReadLine();
				var headers = GetColumns(header);

				table = new Table(headers);

				var rows = GetRows(reader);
				if (skip > 0) rows = rows.Skip(skip);
				if (take > 0) rows = rows.Take(take);

				foreach (var row in rows)
				{
					table.AddRow(row);
				}
			}

			return DoTheFollowingInstancesOf(typeName, table);
		}

		private static IEnumerable<string[]> GetRows(StreamReader reader)
		{
			while (!reader.EndOfStream)
			{
				var row = reader.ReadLine();
				var values = GetColumns(row);
				if (values != null)
				{
					yield return values;
				}
			}
		}

		private static string[] GetColumns(string header)
		{
			var parts = header.Split('|');
			if (parts.Length > 2)
			{
				return parts
					.Skip(1)
					.Take(parts.Length - 2)
					.Select(x => x.Trim())
					.ToArray();
			}
			return null;
		}

		private const string TheFollowingInstancesOfAddedTo = 
			@"^the following instances? of (\w+) added to (\w+) of (\w+) (\d+):$";
		[Given(TheFollowingInstancesOfAddedTo)]
		[When(TheFollowingInstancesOfAddedTo)]
		public static void DoTheFollowingInstancesOfAddedTo(string listTypeName, string parentProperty, string parentTypeName, int parentId, Table table)
		{
			var list = GetParentList(parentTypeName, parentId, parentProperty);
			var children = DoTheFollowingInstancesOf(listTypeName, table);
			AddToParentList(list, children);
		}

		private const string TheFollowingInstancesAddedTo =
			@"^the following instances? added to (\w+) of (\w+) (\d+):$";
		[Given(TheFollowingInstancesAddedTo)]
		[When(TheFollowingInstancesAddedTo)]
		public static void DoTheFollowingInstancesAddedTo(string parentProperty, string parentTypeName, int parentId, Table table)
		{
			var list = GetParentList(parentTypeName, parentId, parentProperty);
			var listTypeName = GetListTypeName(list);
			var children = DoTheFollowingInstancesOf(listTypeName, table);
			AddToParentList(list, children);
		}

		private const string TheFollowingInstancesOfPassedTo =
			@"^the following instances? of (\w+) passed to (\w+) of (\w+) (\d+):$";
		[Given(TheFollowingInstancesOfPassedTo)]
		[When(TheFollowingInstancesOfPassedTo)]
		public static void DoTheFollowingInstancesOfPassedTo(string argumentTypeName, string parentMethod, string parentTypeName, int parentId, Table table)
		{
			var parent = GetParent(parentTypeName, parentId);
			var children = DoTheFollowingInstancesOf(argumentTypeName, table);
			PassToParentMethod(parent, parentMethod, children);
		}

		private const string TheFollowingInstancesPassedTo =
			@"^the following instances? passed to (\w+) of (\w+) (\d+):$";
		[Given(TheFollowingInstancesPassedTo)]
		[When(TheFollowingInstancesPassedTo)]
		public static void DoTheFollowingInstancesPassedTo(string parentMethod, string parentTypeName, int parentId, Table table)
		{
			var parent = GetParent(parentTypeName, parentId);
			var argumentTypeName = GetArgumentTypeName(parent.GetType(), parentMethod);
			var children = DoTheFollowingInstancesOf(argumentTypeName, table);
			PassToParentMethod(parent, parentMethod, children);
		}

		private const string InstancesOfAddedToFromFile =
			@"^instances? of (\w+) added to (\w+) of (\w+)\s+from file (.+)$";
		[Given(InstancesOfAddedToFromFile)]
		[When(InstancesOfAddedToFromFile)]
		public static void DoInstancesOfAddedToFromFile(string itemsTypeName, string parentProperty, string parentTypeName, string file)
		{
			var	itemsProperty = parentTypeName;
			var items = DoInstancesOfFromFileSkipTake(0, 0, itemsTypeName, file);
			var propertyInfo = GetTypeByName(itemsTypeName).GetProperty(itemsProperty);

			var groups = items.GroupBy(o => propertyInfo.GetValue(o, null));
			foreach (var @group in groups)
			{
				var parentId = GetInstanceId<int>(@group.Key);
				var list = GetParentList(parentTypeName, parentId, parentProperty);
				AddToParentList(list, @group.ToList());
			}
		}

		private const string InstancesOfPassedToFromFile =
			@"^instances? of (\w+) passed to (\w+) of (\w+) (\d+)\s+from file (.+)$";
		[Given(InstancesOfPassedToFromFile)]
		[When(InstancesOfPassedToFromFile)]
		public static void DoInstancesOfPassedToFromFile(string itemsTypeName, string parentMethod, string parentTypeName, int parentId, string file)
		{
			var parent = GetParent(parentTypeName, parentId);
			var children = DoInstancesOfFromFileSkipTake(0, 0, itemsTypeName, file);
			PassToParentMethod(parent, parentMethod, children);
		}

		private const string InstancesPassedToFromFile =
			@"^instances? passed to (\w+) of (\w+) (\d+)\s+from file (.+)$";
		[Given(InstancesPassedToFromFile)]
		[When(InstancesPassedToFromFile)]
		public static void DoInstancesPassedToFromFile(string parentMethod, string parentTypeName, int parentId, string file)
		{
			var parent = GetParent(parentTypeName, parentId);
			var argumentTypeName = GetArgumentTypeName(parent.GetType(), parentMethod);
			var children = DoInstancesOfFromFileSkipTake(0, 0, argumentTypeName, file);
			PassToParentMethod(parent, parentMethod, children);
		}
	
	
	}
}