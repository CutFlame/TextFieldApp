using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public static class ReflectionExtensions
	{
		public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> original, Func<T, T, bool> equalsMethod = null)
		{
			if (equalsMethod == null)
			{
				equalsMethod = (one, two) => Equals (one, two);
			}
			var visited = new List<T> ();
			foreach(var item in original)
			{
				T itemCopy = item;
				if(!visited.Any (possibleDuplicate => equalsMethod (itemCopy, possibleDuplicate)))
				{
					visited.Add (item);
					yield return item;
				}
			}
		}

		public static IEnumerable<T> GetStaticInstances<T> () where T : class
		{
			return typeof(T).GetStaticInstances<T> ();
		}

		public static IEnumerable<T> GetStaticInstances<T> (this Type type) where T : class
		{
			return from info in type.GetProperties (BindingFlags.Public | BindingFlags.Static)
			       where info.GetMethod != null && info.PropertyType == typeof(T)
			       select info.GetValue (null) as T;

			//Get all properties
			var properties = type.GetProperties (BindingFlags.Public | BindingFlags.Static);

			//Get all properties that match expected type
			var matchingProperties = properties.Where (info => info.PropertyType == typeof(T) && info.GetMethod != null);

			//Get all values from properties
			var values = matchingProperties.Select (info => info.GetValue (null) as T);

			return values;
		}

	}
	
}
