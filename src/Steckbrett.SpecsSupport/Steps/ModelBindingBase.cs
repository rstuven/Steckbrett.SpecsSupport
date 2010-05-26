using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoPoco.Engine;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Steps
{
	public abstract class ModelBindingBase : BindingBase
	{
		protected static IGenerationSessionFactory GenerationSessionFactory
		{
			get { return (IGenerationSessionFactory)FeatureContext.Current["GenerationSessionFactory"]; }
			set { FeatureContext.Current["GenerationSessionFactory"] = value; }
		}

		protected static IGenerationSession GenerationSession
		{
			get { return (IGenerationSession)ScenarioContext.Current["GenerationSession"]; }
			set { ScenarioContext.Current["GenerationSession"] = value; }
		}

		protected static void AddModelNamespace(string ns)
		{
			TypeNamePrefixes.Add(ns + ".");
		}

		protected static IList<string> TypeNamePrefixes
		{
			get
			{
				return GetValueOrDefault(
					FeatureContext.Current,
					"TypeNamePrefixes",
					() => new List<string>(new[] {""}));
			}
		}

		protected static IDictionary<Type, Delegate> ModelIdExtractors
		{
			get
			{
				return GetValueOrDefault(
					FeatureContext.Current,
					"ModelIdExtractors",
					() => new Dictionary<Type, Delegate>());
			}
		}

		protected static TKey GetInstanceId<TKey>(object entity)
		{
			var extractor = GetModelIdExtractor<TKey>();
			return extractor(entity);
		}

		public static Func<object, TKey> GetModelIdExtractor<TKey>()
		{
			return GetValueOrDefault<Type, Delegate, Func<object, TKey>>(ModelIdExtractors, typeof(TKey), null);
		}

		public static void SetModelIdExtractor<TKey>(Func<object, TKey> extractor)
		{
			ModelIdExtractors[typeof (TKey)] = extractor;
		}

		private static TResult GetValueOrDefault<TKey, TValue, TResult>(IDictionary<TKey, TValue> dictionary, TKey key, Func<TResult> defaultValue)
			where TResult : TValue
		{
			if (dictionary == null) return default(TResult);

			TValue result;
			if (!dictionary.TryGetValue(key, out result))
			{
				result = defaultValue();
				dictionary[key] = result;
			}
			return (TResult)result;
		}


		protected static IList<Type> InstanceTypes
		{
			get
			{
				return GetValueOrDefault(
					ScenarioContext.Current,
					"InstanceTypes",
					() => new List<Type>());
			}
		}

		protected static IList<object> InstancesOf(Type type)
		{
			var typeName = type.FullName;
			object list;
			if (!ScenarioContext.Current.TryGetValue(typeName, out list))
			{
				list = new List<object>();
				ScenarioContext.Current[typeName] = list;
				InstanceTypes.Add(type);
			}
			return list as List<object>;
		}

		protected static IEnumerable<T> InstancesOf<T>()
		{
			return InstancesOf(typeof (T)).Cast<T>();
		}

		protected static void AddInstance<T>(T instance)
		{
			InstancesOf(typeof (T)).Add(instance);
		}

		protected static TEntity InstanceById<TEntity, TKey>(TKey id)
		{
			var instance = InstancesOf<TEntity>()
				.Where(o => GetInstanceId<TKey>(o).Equals(id))
				.FirstOrDefault();

			return instance;
		}

		protected static TEntity InstanceById<TEntity>(int id)
		{
			return InstanceById<TEntity, int>(id);
		}

		protected static object InstanceById<TKey>(Type type, TKey id)
		{
			var instance = InstancesOf(type)
				.Where(o => GetInstanceId<TKey>(o).Equals(id))
				.FirstOrDefault();
			return instance;
		}

		protected static object ConvertModel<T>(TableRow row, string actualProperty)
		{
			return ConvertModel<T>(row, actualProperty, typeof(T).Name);
		}

		protected static object ConvertModel<T>(TableRow row, string actualProperty, string expectedProperty)
		{
			return actualProperty == expectedProperty
					? (object)InstanceById<T, int>(Convert.ToInt32(row[actualProperty]))
					: null;
		}

		protected static Type GetTypeByName(string typeName)
		{
			var type =
				(
					from a in AppDomain.CurrentDomain.GetAssemblies()
					from tnp in TypeNamePrefixes
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

		protected static string GetListTypeName(object list)
		{
			return list.GetType().GetGenericArguments()[0].FullName;
		}

		protected static string GetArgumentTypeName(Type type, string methodName)
		{
			var method = GetMethod(type, methodName);
			var parameter = method.GetParameters().FirstOrDefault();
			if (parameter == null)
			{
				throw new ArgumentException(string.Format("Method '{0}' does not have parameters", methodName), "methodName");
			}
			return parameter.ParameterType.FullName;
		}

		protected static MethodInfo GetMethod(Type type, string methodName)
		{
			var method = type.GetMethod(methodName);
			if (method == null)
			{
				throw new ArgumentException(string.Format("Method '{0}' not found in {1}", methodName, type.FullName), "methodName");
			}
			return method;
		}


		protected static T GetInstanceByIdToken<T>(string idToken)
		{
			return (T) GetInstanceByIdToken(typeof (T).FullName, idToken);
		}

		protected static object GetInstanceByIdToken(string typeName, string idToken)
		{
			var type = GetTypeByName(typeName);
			object parent = null;
			int id;
			if (int.TryParse(idToken, out id))
			{
				parent = InstanceById(type, id);
			}
			else if (idToken.StartsWith("last"))
			{
				parent = InstancesOf(type).LastOrDefault();
			}
			if (parent == null)
			{
				throw new InvalidOperationException(string.Format("Not found instance of {0} with id token '{1}'", type.FullName, idToken));
			}
			return parent;
		}

		protected static object GetParentList(string parentTypeName, string parentId, string parentProperty)
		{
			var parent = GetInstanceByIdToken(parentTypeName, parentId);
			var property = parent.GetType().GetProperty(parentProperty).GetGetMethod();
			var list = property.Invoke(parent, new object[0]);
			return list;
		}

		protected static void AddToParentList(object list, IList<object> children)
		{
			var add = typeof(IList).GetMethod("Add");
			foreach (var child in children)
			{
				add.Invoke(list, new[] { child });
			}
		}

		protected static void PassToParentMethod(object parent, string methodName, IList<object> children)
		{
			var method = GetMethod(parent.GetType(), methodName);
			foreach (var child in children)
			{
				method.Invoke(parent, new[] { child });
			}
		}
	}

}
