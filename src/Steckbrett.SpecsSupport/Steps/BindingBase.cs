using TechTalk.SpecFlow;

namespace Steckbrett.SpecsSupport.Steps
{
	public abstract class BindingBase
	{
		protected void Given(string text)
		{
			TestRunnerManager.GetTestRunner()
				.Given(text);
		}

		protected void Given(string text, string multilineTextArg)
		{
			TestRunnerManager.GetTestRunner()
				.Given(text, multilineTextArg);
		}

		protected void Given(string text, string multilineTextArg, Table table)
		{
			TestRunnerManager.GetTestRunner()
				.Given(text, multilineTextArg, table);
		}

		protected void When(string text)
		{
			TestRunnerManager.GetTestRunner()
				.When(text);
		}

		protected void When(string text, string multilineTextArg)
		{
			TestRunnerManager.GetTestRunner()
				.When(text, multilineTextArg);
		}

		protected void When(string text, string multilineTextArg, Table table)
		{
			TestRunnerManager.GetTestRunner()
				.When(text, multilineTextArg, table);
		}

		protected void Then(string text)
		{
			TestRunnerManager.GetTestRunner()
				.Then(text);
		}

		protected void Then(string text, string multilineTextArg)
		{
			TestRunnerManager.GetTestRunner()
				.Then(text, multilineTextArg);
		}

		protected void Then(string text, string multilineTextArg, Table table)
		{
			TestRunnerManager.GetTestRunner()
				.Then(text, multilineTextArg, table);
		}

	}
}