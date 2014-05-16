using System;
using System.Reflection;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public partial class StringPropertyCell : PropertyCell<string>
	{
		public static new string ReuseIdentifier = "StringPropertyCellReuseIdentifier";

		public StringPropertyCell (IntPtr handle) : base (handle)
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
			PropertyValueField.Placeholder = "ERROR";
			PropertyValueField.AttributedPlaceholder = new NSAttributedString (PropertyValueField.Placeholder, null, UIColor.Red);
		}

		protected override void UpdateLabelsForValue (string value)
		{
			if (value == null)
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
