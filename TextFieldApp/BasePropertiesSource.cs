using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public abstract class BasePropertiesSource<T> : UITableViewSource
	{
		public Action OnChange;

		protected readonly T _instance;

		protected List<UIColor> _colorList = new List<UIColor> ();

		protected BasePropertiesSource(T instance)
		{
			_instance = instance;

			_colorList = CreateColorList ();

		}

		List<UIColor> CreateColorList ()
		{
			var colorValues = ReflectionExtensions.GetStaticInstances<UIColor> ();

			//Eliminate duplicates
			var colorList = colorValues.RemoveDuplicates (ColorsMatch);

			return colorList.ToList ();
		}

		protected static Type[] CreateTypesArray (Type type)
		{
			var types = new List<Type> ();
			while (type != null && type != typeof(object))
			{
				types.Add (type);
				type = type.BaseType;
			}
			return types.ToArray ();
		}

		protected static Dictionary<Type, PropertyInfo[]> CreatePropertiesDictionary (IEnumerable<Type> types)
		{
			const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
			var dict = new Dictionary<Type, PropertyInfo[]> ();
			foreach (var type in types)
			{
				var properties = CreatePropertiesArray (type, bindingFlags);
				dict.Add (type, properties);
			}
			return dict;
		}

		protected static PropertyInfo[] CreatePropertiesArray (Type type, BindingFlags bindingFlags)
		{
			var properties = type.GetProperties (bindingFlags).ToList ();

			//Sort the properties by name
			properties.Sort ((one, two) => string.Compare (one.Name, two.Name, StringComparison.OrdinalIgnoreCase));

			return properties.ToArray ();
		}

		protected abstract PropertyInfo GetPropertyInfoFromIndexPath (NSIndexPath indexPath);

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var propertyInfo = GetPropertyInfoFromIndexPath (indexPath);
			var reuseIdentifier = GetCellReuseIdentifierForProperty (propertyInfo);
			var cell = tableView.DequeueReusableCell (reuseIdentifier);
			try
			{
				var iProperty = (IPropertyCell)cell;
				iProperty.Update (propertyInfo, _instance);
			}
			catch(Exception ex)
			{
				Console.WriteLine (ex);
			}
			finally
			{
			}
			return cell;
		}

		string GetCellReuseIdentifierForProperty(PropertyInfo propertyInfo)
		{
			if (propertyInfo.PropertyType == typeof(UIColor))
			{
				return ColorPropertyCell.ReuseIdentifier;
			}
			if (propertyInfo.PropertyType == typeof(bool))
			{
				return BooleanPropertyCell.ReuseIdentifier;
			}
			if (propertyInfo.PropertyType == typeof(string))
			{
				return StringPropertyCell.ReuseIdentifier;
			}
			return BasicPropertyCell.ReuseIdentifier;
		}

		public override NSIndexPath WillSelectRow (UITableView tableView, NSIndexPath indexPath)
		{
			var propertyInfo = GetPropertyInfoFromIndexPath (indexPath);
			if (propertyInfo.SetMethod == null)
			{
				return null;
			}
			return indexPath;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);
			var propertyInfo = GetPropertyInfoFromIndexPath (indexPath);
			ToggleValue (propertyInfo);
			tableView.ReloadRows (new []{indexPath}, UITableViewRowAnimation.Fade);
			CallOnChange ();
		}

		protected void ToggleValue (PropertyInfo propertyInfo)
		{
			if (propertyInfo.PropertyType.IsEnum)
			{
				IncrementEnumValue (propertyInfo);
			}
			else if (propertyInfo.PropertyType == typeof(bool))
			{
				IncrementBoolValue (propertyInfo);
			}
			else if (propertyInfo.PropertyType == typeof(UIColor))
			{
				IncrementColorValue (propertyInfo);
			}
		}

		void IncrementEnumValue (PropertyInfo propertyInfo)
		{
			var values = propertyInfo.PropertyType.GetEnumValues ();
			var currentValue = propertyInfo.GetValue (_instance);
			var currentIndex = Array.IndexOf (values, currentValue);
			var nextIndex = (currentIndex + 1) % values.Length;
			var nextValue = values.GetValue (nextIndex);
			propertyInfo.SetValue (_instance, nextValue);
		}

		void IncrementBoolValue (PropertyInfo propertyInfo)
		{
			bool currentValue = (bool)propertyInfo.GetValue (_instance);
			propertyInfo.SetValue (_instance, !currentValue);
		}

		void IncrementColorValue (PropertyInfo propertyInfo)
		{
			var currentValue = propertyInfo.GetValue (_instance) as UIColor;
			if (currentValue == null)
			{
				throw new Exception ("Color property was not a color!");
			}

			UIColor newValue;
			int index = _colorList.FindIndex (color => ColorsMatch (currentValue, color));
			if (0 <= index && index < _colorList.Count)
			{
				int newIndex = (index + 1) % _colorList.Count;
				newValue = _colorList [newIndex];
			}
			else
			{
				newValue = UIColor.Black;
			}
			propertyInfo.SetValue (_instance, newValue);
		}

		bool ColorsMatch (UIColor one, UIColor two)
		{
			var oneComp = one.CGColor.Components;
			var twoComp = two.CGColor.Components;
			return oneComp.SequenceEqual (twoComp);
		}

		protected void CallOnChange ()
		{
			var handler = OnChange;
			if (handler != null)
			{
				handler ();
			}
		}

	}
	
}
