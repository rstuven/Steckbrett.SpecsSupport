using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport
{
	public static class ScenarioContextExtensions
	{
		public static IList<Type> InstanceTypes(this ScenarioContext context)
		{
			return context.GetValueOrDefault(
				"InstanceTypes",
				() => new List<Type>());
		}

		public static IList<object> InstancesOf(this ScenarioContext context, Type type)
		{
			var typeName = type.FullName;
			object list;
			if (!ScenarioContext.Current.TryGetValue(typeName, out list))
			{
				list = new List<object>();
				context[typeName] = list;
				context.InstanceTypes().Add(type);
			}
			return list as List<object>;
		}

		public static IList<object> InstancesOf(this ScenarioContext context, string typeName)
		{
			var type = FeatureContext.Current.GetTypeByName(typeName);
			return context.InstancesOf(type);
		}

		public static IEnumerable<T> InstancesOf<T>(this ScenarioContext context)
		{
			return context.InstancesOf(typeof(T)).Cast<T>();
		}

		public static void AddInstance<T>(this ScenarioContext context, T instance)
		{
			context.InstancesOf(typeof(T)).Add(instance);
		}

		public static TEntity InstanceById<TEntity, TKey>(this ScenarioContext context, TKey id)
		{
			var instance = context.InstancesOf<TEntity>()
				.Where(o => FeatureContext.Current.GetInstanceId<TKey>(o).Equals(id))
				.FirstOrDefault();

			return instance;
		}

		public static TEntity InstanceById<TEntity>(this ScenarioContext context, int id)
		{
			return context.InstanceById<TEntity, int>(id);
		}

		public static object InstanceById<TKey>(this ScenarioContext context, Type type, TKey id)
		{
			var instance = context.InstancesOf(type)
				.Where(o => FeatureContext.Current.GetInstanceId<TKey>(o).Equals(id))
				.FirstOrDefault();
			return instance;
		}

		public static T GetInstanceByIdToken<T>(this ScenarioContext context, string idToken)
		{
			return (T)context.GetInstanceByIdToken(typeof(T).FullName, idToken);
		}

		public static object GetInstanceByIdToken(this ScenarioContext context, string typeName, string idToken)
		{
			var type = FeatureContext.Current.GetTypeByName(typeName);
			object parent = null;
			int id;
			if (int.TryParse(idToken, out id))
			{
				parent = context.InstanceById(type, id);
			}
			else if (idToken.StartsWith("last"))
			{
				parent = context.InstancesOf(type).LastOrDefault();
			}
			if (parent == null)
			{
				throw new InvalidOperationException(string.Format("Not found instance of {0} with id token '{1}'", type.FullName, idToken));
			}
			return parent;
		}
	}
}