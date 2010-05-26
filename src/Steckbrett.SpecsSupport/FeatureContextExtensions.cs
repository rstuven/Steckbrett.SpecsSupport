using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport
{
	public static class FeatureContextExtensions
	{
		public static void AddModelNamespace(this FeatureContext context, string ns)
		{
			context.TypeNamePrefixes().Add(ns + ".");
		}

		public static IList<string> TypeNamePrefixes(this FeatureContext context)
		{
			return context.GetValueOrDefault(
				"TypeNamePrefixes",
				() => new List<string>(new[] {""}));
		}

		public static IDictionary<Type, Delegate> ModelIdExtractors(this FeatureContext context)
		{
			return context.GetValueOrDefault(
				"ModelIdExtractors",
				() => new Dictionary<Type, Delegate>());
		}

		public static TKey GetInstanceId<TKey>(this FeatureContext context, object entity)
		{
			var extractor = context.GetModelIdExtractor<TKey>();
			return extractor(entity);
		}

		public static Func<object, TKey> GetModelIdExtractor<TKey>(this FeatureContext context)
		{
			return context.ModelIdExtractors().GetValueOrDefault<Type, Delegate, Func<object, TKey>>(typeof(TKey), null);
		}

		public static void SetModelIdExtractor<TKey>(this FeatureContext context, Func<object, TKey> extractor)
		{
			context.ModelIdExtractors()[typeof(TKey)] = extractor;
		}

		public static Type GetTypeByName(this FeatureContext context, string typeName)
		{
			var type =
				(
					from a in AppDomain.CurrentDomain.GetAssemblies()
					from tnp in context.TypeNamePrefixes()
					let t = a.GetType(tnp + typeName)
					where t != null
					select t
				).FirstOrDefault();

			if (type == null)
			{
				throw new ArgumentException(string.Format("Type \"{0}\" not found", typeName), "typeName");
			}
			return type;
		}

	}
}