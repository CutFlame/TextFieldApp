using System;
using System.Reflection;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public partial class StringPropertyCell : UITableViewCell, IPropertyCell
	{
		public static new string ReuseIdentifier = "StringPropertyCellReuseIdentifier";

		public StringPropertyCell (IntPtr handle) : base (handle)
		{
		}

		#region IPropertyCell implementation

		public void UpdatePropertyInfo (PropertyInfo propertyInfo, object instance)
		{
			PropertyNameLabel.Text = propertyInfo.Name;
			PropertyTypeLabel.Text = propertyInfo.PropertyType.Name;
			bool error = false;
			string value = null;
			try
			{
				value = propertyInfo.GetValue (instance) as string;
			}
			catch
			{
				error = true;
			}

			if (error)
			{
				PropertyValueField.Placeholder = "ERROR";
				PropertyValueField.AttributedPlaceholder = new NSAttributedString (PropertyValueField.Placeholder, null, UIColor.Red);

			}
			else if (value == null)
			{
				PropertyValueField.Placeholder = "null";
			}
			else
			{
				PropertyValueField.Placeholder = "string.Empty";
			}
			PropertyValueField.Text = value;
		}

		#endregion
	}
}
