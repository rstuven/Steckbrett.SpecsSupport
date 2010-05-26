using System;
using System.Reflection;
using AutoMapper;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport
{
	public static class MappingExpressionExtensions
	{
		public static IMappingExpression<TableRow, TDestination> ConvertFromTableRow<TDestination>(
			this IMappingExpression<TableRow, TDestination> exp)
		{
			exp.ConvertFromTableRow(p => p, (r, p) => r[p]);
			return exp;
		}

		public static IMappingExpression<TableRow, TDestination> ConvertFromTableRow<TDestination>(
			this IMappingExpression<TableRow, TDestination> exp,
			Func<TableRow, string, object> getter)
		{
			exp.ConvertFromTableRow(p => p, getter);
			return exp;
		}


		public static IMappingExpression<TableRow, TDestination> ConvertFromTableRow<TDestination>(
			this IMappingExpression<TableRow, TDestination> exp,
			Func<string, string> propertyNameMapper,
			Func<TableRow, string, object> getter)
		{
			const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			foreach (var property in typeof(TDestination).GetProperties(bindingFlags))
			{
				if (!property.CanWrite)
				{
					continue;
				}
				var propertyName = property.Name;
				propertyName = propertyNameMapper(propertyName);
				exp.ForMember(propertyName, cfg => cfg.MapFrom(
					row =>
					{
						try
						{
							return getter(row, propertyName);
						}
						catch { }
						return null;
					}));
			}
			return exp;
		}
	}
}