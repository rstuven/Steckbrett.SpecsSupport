using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoPoco.Engine;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Steps
{
	[Binding]
	public class AutoPocoSteps : ModelBindingBase
	{
		private const string ARandomInstancesOf =
			@"^a random instance of (\w+)$";
		[Given(ARandomInstancesOf)]
		[When(ARandomInstancesOf)]
		public static void DoARandomInstancesOf(string typeName)
		{
			DoRandomInstancesOf(1, typeName);
		}

		private const string RandomInstancesOf = 
			@"^(\d+) random instances? of (\w+)$";
		[Given(RandomInstancesOf)]
		[When(RandomInstancesOf)]
		public static IList<object> DoRandomInstancesOf(int count, string typeName)
		{
			var type = FeatureContext.Current.GetTypeByName(typeName);

			var listMethod = typeof (IGenerationSession).GetMethod("List");
			var listTypeMethod = listMethod.MakeGenericMethod(type);
			var collectionContext = listTypeMethod.Invoke(GenerationSession, new object[] { count });

			var collectionContextGenericType =
				typeof (ICollectionContext<,>)
					.MakeGenericType(
						type,
						typeof (IList<>).MakeGenericType(type));

			var getMethod = collectionContextGenericType.GetMethod("Get");
			var list = (IEnumerable)getMethod.Invoke(collectionContext, null);

			var result = list.Cast<object>().ToList();

			var all = ScenarioContext.Current.InstancesOf(type);
			foreach (var item in result)
			{
				all.Add(item);
			}

			return result;
		}

		private const string RandomInstancesAddedTo =
			@"^(\d+) random instances? added to (\w+) of (\w+) (.+)$";
		[Given(RandomInstancesAddedTo)]
		[When(RandomInstancesAddedTo)]
		public static void DoRandomInstancesAddedTo(int count, string parentProperty, string parentTypeName, string parentId)
		{
			var list = GetParentList(parentTypeName, parentId, parentProperty);
			var listTypeName = GetListTypeName(list);
			var children = DoRandomInstancesOf(count, listTypeName);
			AddToParentList(list, children);
		}

		private const string RandomInstancesOfAddedTo =
			@"^(\d+) random instances? of (\w+) added to (\w+) of (\w+) (.+)$";
		[Given(RandomInstancesOfAddedTo)]
		[When(RandomInstancesOfAddedTo)]
		public static void DoRandomInstancesOfAddedTo(int count, string listTypeName, string parentProperty, string parentTypeName, string parentId)
		{
			var list = GetParentList(parentTypeName, parentId, parentProperty);
			var children = DoRandomInstancesOf(count, listTypeName);
			AddToParentList(list, children);
		}

		private const string RandomInstancesPassedTo =
			@"^(\d+) random instances? passed to (\w+) of (\w+) (.+)$";
		[Given(RandomInstancesPassedTo)]
		[When(RandomInstancesPassedTo)]
		public static void DoRandomInstancesPassedTo(int count, string parentMethod, string parentTypeName, string parentId)
		{
			var parent = ScenarioContext.Current.GetInstanceByIdToken(parentTypeName, parentId);
			var argumentTypeName = GetArgumentTypeName(parent.GetType(), parentMethod);
			var children = DoRandomInstancesOf(count, argumentTypeName);
			PassToParentMethod(parent, parentMethod, children);
		}

		private const string RandomInstancesOfPassedTo =
			@"^(\d+) random instances? of (\w+) passed to (\w+) of (\w+) (.+)$";
		[Given(RandomInstancesOfPassedTo)]
		[When(RandomInstancesOfPassedTo)]
		public static void DoRandomInstancesOfPassedTo(int count, string argumentTypeName, string parentMethod, string parentTypeName, string parentId)
		{
			var parent = ScenarioContext.Current.GetInstanceByIdToken(parentTypeName, parentId);
			var children = DoRandomInstancesOf(count, argumentTypeName);
			PassToParentMethod(parent, parentMethod, children);
		}
	}
}