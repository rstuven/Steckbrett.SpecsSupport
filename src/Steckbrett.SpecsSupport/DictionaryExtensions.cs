using System;
using System.Collections.Generic;

namespace Steckbrett.SpecsSupport
{
	internal static class DictionaryExtensions
	{
		public static TResult GetValueOrDefault<TKey, TValue, TResult>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TResult> defaultValue)
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
	}
}