using System;
using System.Reflection;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public partial class ColorPropertyCell : PropertyCell<UIColor>
	{
		public static new string ReuseIdentifier = "ColorPropertyCellReuseIdentifier";

		public ColorPropertyCell (IntPtr handle) : base (handle)
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
			PropertyValueLabel.BackgroundColor = UIColor.Clear;
		}

		protected override void UpdateLabelsForValue (UIColor value)
		{
			if (value == null)
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
