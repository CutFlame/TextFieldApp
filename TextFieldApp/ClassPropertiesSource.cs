using System;
using System.Linq;
using System.Reflection;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace TextFieldApp
{
	public class ClassPropertiesSource<T> : BasePropertiesSource<T>
	{
		readonly Type[] _types;
		readonly Dictionary<Type, PropertyInfo[]> _properties;

		public ClassPropertiesSource(T instance) : base(instance)
		{
			_types = CreateTypesArray (typeof(T));
			_properties = CreatePropertiesDictionary (_types);
		}

		public override int NumberOfSections (UITableView tableView)
		{
			return _types.Length;
		}

		public override string TitleForHeader (UITableView tableView, int section)
		{
			var type = _types [section];
			return type.Name;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			var type = _types [section];
			return _properties[type].Length;
		}

		protected override PropertyInfo GetPropertyInfoFromIndexPath (NSIndexPath indexPath)
		{
			var type = _types [indexPath.Section];
			var propertyInfo = _properties [type] [indexPath.Row];
			return propertyInfo;
		}

	}
}
