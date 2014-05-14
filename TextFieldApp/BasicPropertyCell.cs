using System;
using System.Reflection;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public partial class BasicPropertyCell : UITableViewCell, IPropertyCell
	{
		public static new string ReuseIdentifier = "BasicPropertyCellReuseIdentifier";

		public BasicPropertyCell (IntPtr handle) : base (handle)
		{
		}

		#region IPropertyCell implementation

		public void UpdatePropertyInfo (PropertyInfo propertyInfo, object instance)
		{
			PropertyNameLabel.Text = propertyInfo.Name;
			PropertyTypeLabel.Text = propertyInfo.PropertyType.Name;
			bool error = false;
			object value = null;
			try
			{
				value = propertyInfo.GetValue (instance);
			}
			catch
			{
				error = true;
			}

			if(error)
			{
				PropertyValueLabel.Text = "ERROR";
				PropertyValueLabel.TextColor = UIColor.Red;
			}
			else if (value == null)
			{
				PropertyValueLabel.Text = "null";
				PropertyValueLabel.TextColor = UIColor.LightGray;
			}
			else
			{
				PropertyValueLabel.Text = value.ToString ();
				PropertyValueLabel.TextColor = UIColor.DarkGray;
			}
		}

		#endregion
	}
}
