using System;
using System.Reflection;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public partial class BooleanPropertyCell : PropertyCell<bool>
	{
		public static new string ReuseIdentifier = "BooleanPropertyCellReuseIdentifier";

		public BooleanPropertyCell (IntPtr handle) : base (handle)
		{
		}

		#region IPropertyCell implementation

		protected override void UpdateNameAndTypeLabels (PropertyInfo propertyInfo, bool readOnly)
		{
			PropertyNameLabel.Text = propertyInfo.Name;
			PropertyTypeLabel.Text = propertyInfo.PropertyType.Name;
			UIColor color = readOnly ? UIColor.Gray : UIColor.Black;
			PropertyNameLabel.TextColor = color;
			PropertyTypeLabel.TextColor = color;
			PropertyValueSwitch.Enabled = false;
		}

		protected override void UpdateLabelsForError ()
		{
			PropertyValueSwitch.TintColor = UIColor.Red;
			PropertyValueSwitch.OnTintColor = UIColor.Red;
			PropertyValueSwitch.ThumbTintColor = UIColor.Red;
		}

		protected override void UpdateLabelsForValue (bool value)
		{
			PropertyValueSwitch.TintColor = null;
			PropertyValueSwitch.OnTintColor = null;
			PropertyValueSwitch.ThumbTintColor = null;
			PropertyValueSwitch.On = value;
		}

		#endregion
	}
}
