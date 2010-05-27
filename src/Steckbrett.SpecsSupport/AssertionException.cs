using System;

namespace Steckbrett.SpecsSupport
{
	///<summary>
	/// Unit test framework agnostic assertion exception.
	///</summary>
	public class AssertionException : Exception
	{
		public AssertionException(string message) : base(message) { }
		public AssertionException(string format, params object[] args) : base(string.Format(format, args)) { }
	}
}