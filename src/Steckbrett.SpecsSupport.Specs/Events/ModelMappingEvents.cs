using System;
using AutoMapper;
using AutoPoco;
using AutoPoco.Configuration;
using AutoPoco.Conventions;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using Steckbrett.SpecsSupport.Specs.Model;
using Steckbrett.SpecsSupport.Steps;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Specs.Events
{
	[Binding]
	public class ModelMappingEvents : ModelBindingBase
	{
		[BeforeFeature("model_mapping")]
		static void ModelConfiguration()
		{
			FeatureContext.Current.AddModelNamespace("Steckbrett.SpecsSupport.Specs.Model");

			FeatureContext.Current.SetModelIdExtractor(o => ((ModelBase)o).Id);
		}

		[BeforeFeature("model_mapping")]
		static void TableMappingConfiguration()
		{
			// Int32
			Mapper.CreateMap<String, Int32>()
				.ConvertUsing(Convert.ToInt32);

			// Decimal
			Mapper.CreateMap<String, Decimal>()
				.ConvertUsing(Convert.ToDecimal);

			// Customer
			Mapper.CreateMap<TableRow, Customer>()
				.ConvertFromTableRow(
					(r, p) =>
					ConvertModel<Customer>(r, p, "Parent") ??
					r[p]);

			// Order
			Mapper.CreateMap<TableRow, Order>()
				.ForMember(o => o.Details, x => x.Ignore())
				.ConvertFromTableRow(
					(r, p) =>
					ConvertModel<Customer>(r, p) ??
					r[p]);

			// Detail
			Mapper.CreateMap<TableRow, Detail>()
				.ConvertFromTableRow();


			// Group
			Mapper.CreateMap<TableRow, Group>()
				.ForMember(o => o.Customers, x => x.Ignore())
				.ConvertFromTableRow();

		}

		[BeforeFeature("model_mapping")]
		static void GeneratorConfiguration()
		{
			GenerationSessionFactory = AutoPocoContainer.Configure(x =>
			{
				x.Conventions(c =>
				{
					c.Register<DefaultTypeConvention>();
					c.Register<ModelConvention>();
				});
				x.AddFromAssemblyContainingType<Customer>();
				x.Include<Customer>()
					.Setup(c => c.FirstName).Use<FirstNameSource>()
					.Setup(c => c.LastName).Use<LastNameSource>();
				x.Include<Detail>()
					.Setup(c => c.Product).Use<LastNameSource>();
			});
		}

		class ModelConvention : ITypePropertyConvention
		{
			public void Apply(ITypePropertyConventionContext context)
			{
				if (context.Member.PropertyInfo.Name == "Id" &&
					typeof(ModelBase).IsAssignableFrom(context.Member.PropertyInfo.DeclaringType))
				{
					context.SetSource<ModelIdSource>();
				}
			}
		}

		class ModelIdSource : DatasourceBase<int>
		{
			private int id;

			public override int Next(IGenerationSession session)
			{
				return ++id;
			}
		}

	}
}
