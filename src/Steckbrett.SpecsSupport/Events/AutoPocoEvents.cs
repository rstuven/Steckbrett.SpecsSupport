using Steckbrett.SpecsSupport.Steps;
using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Events
{
	[Binding]
	public class AutoPocoEvents : ModelBindingBase
	{
		[BeforeScenario("model_mapping")]
		public void InitializeGenerationSession()
		{
			if (GenerationSessionFactory != null)
			{
				GenerationSession = GenerationSessionFactory.CreateSession();
			}
		}
	}
}