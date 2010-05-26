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

		protected static object ConvertModel<T>(TableRow row, string actualProperty)
		{
			return ConvertModel<T>(row, actualProperty, typeof(T).Name);
		}

		protected static object ConvertModel<T>(TableRow row, string actualProperty, string expectedProperty)
		{
			return actualProperty == expectedProperty
					? (object)ScenarioContext.Current.InstanceById<T, int>(Convert.ToInt32(row[actualProperty]))
					: null;
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

		protected static object GetParentList(string parentTypeName, string parentId, string parentProperty)
		{
			var parent = ScenarioContext.Current.GetInstanceByIdToken(parentTypeName, parentId);
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
