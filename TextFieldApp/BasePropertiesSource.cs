using System;
using System.Linq;
using System.Reflection;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace TextFieldApp
{
	public abstract class BasePropertiesSource<T> : UITableViewSource
	{
		public Action OnChange;

		protected readonly T _instance;

		protected BasePropertiesSource(T instance)
		{
			_instance = instance;
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
			var iProperty = (IPropertyCell)cell;
			iProperty.UpdatePropertyInfo (propertyInfo, _instance);
			return cell;
		}

		string GetCellReuseIdentifierForProperty(PropertyInfo propertyInfo)
		{
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
		}

		protected void IncrementEnumValue (PropertyInfo propertyInfo)
		{
			var values = propertyInfo.PropertyType.GetEnumValues ();
			var currentValue = propertyInfo.GetValue (_instance);
			var currentIndex = Array.IndexOf (values, currentValue);
			var nextIndex = (currentIndex + 1) % values.Length;
			var nextValue = values.GetValue (nextIndex);
			propertyInfo.SetValue (_instance, nextValue);
		}

		protected void IncrementBoolValue (PropertyInfo propertyInfo)
		{
			bool currentValue = (bool)propertyInfo.GetValue (_instance);
			propertyInfo.SetValue (_instance, !currentValue);
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
