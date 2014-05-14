using System;
using System.Linq;
using System.Reflection;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public interface IPropertyCell
	{
		void UpdatePropertyInfo (PropertyInfo propertyInfo, object instance);
	}

	public class ClassPropertiesSource<T> : UITableViewSource
	{
		public Action OnChange;

		Type _type;

		PropertyInfo[] _properties;

		T _instance;

		public ClassPropertiesSource(T instance)
		{
			_instance = instance;
			_type = typeof(T);
			var properties = _type.GetProperties (BindingFlags.Instance | BindingFlags.Public).ToList();
			properties.Sort ((one, two) => string.Compare (one.Name, two.Name, StringComparison.OrdinalIgnoreCase));
			_properties = properties.ToArray ();
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			return _type.Name;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _properties.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var propertyInfo = _properties [indexPath.Row];
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
			var propertyInfo = _properties [indexPath.Row];
			if (propertyInfo.SetMethod == null)
			{
				return null;
			}
			return indexPath;
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);
			var propertyInfo = _properties [indexPath.Row];
			ToggleValue (propertyInfo);
			tableView.ReloadRows (new []{indexPath}, UITableViewRowAnimation.Fade);
			CallOnChange ();
		}

		void ToggleValue (PropertyInfo propertyInfo)
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

		void CallOnChange ()
		{
			var handler = OnChange;
			if (handler != null)
			{
				handler ();
			}
		}
	}
}
