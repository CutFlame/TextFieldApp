using System;
using System.Reflection;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public partial class ColorPropertyCell : UITableViewCell, IPropertyCell
	{
		public static new string ReuseIdentifier = "ColorPropertyCellReuseIdentifier";

		public ColorPropertyCell (IntPtr handle) : base (handle)
		{
		}

		#region IPropertyCell implementation

		public void UpdatePropertyInfo (PropertyInfo propertyInfo, object instance)
		{
			PropertyNameLabel.Text = propertyInfo.Name;
			PropertyTypeLabel.Text = propertyInfo.PropertyType.Name;
			bool error = false;
			UIColor value = null;
			try
			{
				value = propertyInfo.GetValue (instance) as UIColor;
			}
			catch
			{
				error = true;
			}

			if(error)
			{
				PropertyValueLabel.Text = "ERROR";
				PropertyValueLabel.TextColor = UIColor.Red;
				PropertyValueLabel.BackgroundColor = UIColor.Clear;
			}
			else if (value == null)
			{
				PropertyValueLabel.Text = "null";
				PropertyValueLabel.TextColor = UIColor.LightGray;
				PropertyValueLabel.BackgroundColor = UIColor.Clear;
			}
			else
			{
				PropertyValueLabel.Text = string.Empty;
				PropertyValueLabel.BackgroundColor = value;
				PropertyValueLabel.TextColor = UIColor.DarkGray;
			}
		}

		#endregion
	}
}
