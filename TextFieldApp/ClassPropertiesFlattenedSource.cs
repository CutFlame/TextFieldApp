using System;
using System.Linq;
using System.Reflection;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public class ClassPropertiesFlattenedSource<T> : BasePropertiesSource<T>
	{
		readonly Type _type;
		readonly PropertyInfo[] _properties;

		public ClassPropertiesFlattenedSource(T instance) : base(instance)
		{
			_instance = instance;
			_type = typeof(T);
			const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			_properties = CreatePropertiesArray (_type, bindingFlags);
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
			
		protected override PropertyInfo GetPropertyInfoFromIndexPath (NSIndexPath indexPath)
		{
			return _properties [indexPath.Row];
		}
	}
}
