using System;
using System.Reflection;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public partial class BooleanPropertyCell : UITableViewCell, IPropertyCell
	{
		public static new string ReuseIdentifier = "BooleanPropertyCellReuseIdentifier";

		public BooleanPropertyCell (IntPtr handle) : base (handle)
		{
		}

		#region IPropertyCell implementation

		public void UpdatePropertyInfo (PropertyInfo propertyInfo, object instance)
		{
			PropertyNameLabel.Text = propertyInfo.Name;
			PropertyTypeLabel.Text = propertyInfo.PropertyType.Name;
			bool error = false;
			bool value = false;
			try
			{
				value = (bool)propertyInfo.GetValue (instance);
			}
			catch
			{
				error = true;
			}

			PropertyValueSwitch.Enabled = false;
			if(error)
			{
				PropertyValueSwitch.TintColor = UIColor.Red;
				PropertyValueSwitch.OnTintColor = UIColor.Red;
				PropertyValueSwitch.ThumbTintColor = UIColor.Red;
			}
			else
			{
				PropertyValueSwitch.TintColor = null;
				PropertyValueSwitch.OnTintColor = null;
				PropertyValueSwitch.ThumbTintColor = null;
				PropertyValueSwitch.On = value;
			}
		}

		#endregion
	}
}
