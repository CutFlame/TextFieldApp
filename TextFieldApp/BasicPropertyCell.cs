using System;
using System.Reflection;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public partial class BasicPropertyCell : PropertyCell<object>
	{
		public static new string ReuseIdentifier = "BasicPropertyCellReuseIdentifier";

		public BasicPropertyCell (IntPtr handle) : base (handle)
		{
		}

		#region implemented abstract members of PropertyCell

		protected override void UpdateNameAndTypeLabels (PropertyInfo propertyInfo, bool readOnly)
		{
			PropertyNameLabel.Text = propertyInfo.Name;
			PropertyTypeLabel.Text = propertyInfo.PropertyType.Name;
			UIColor color = readOnly ? UIColor.Gray : UIColor.Black;
			PropertyNameLabel.TextColor = color;
			PropertyTypeLabel.TextColor = color;
		}

		protected override void UpdateLabelsForError ()
		{
			PropertyValueLabel.Text = "ERROR";
			PropertyValueLabel.TextColor = UIColor.Red;
		}

		protected override void UpdateLabelsForValue (object value)
		{
			if (value == null)
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
