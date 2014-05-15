using MonoTouch.UIKit;

namespace TextFieldApp
{
	public class TextFieldTraits : IUITextInputTraits
	{
		#region IUITextInputTraits implementation

		public UITextAutocapitalizationType AutocapitalizationType { get; set; }

		public UITextAutocorrectionType AutocorrectionType { get; set; }

		public UIKeyboardType KeyboardType { get; set; }

		public UIKeyboardAppearance KeyboardAppearance { get; set; }

		public UIReturnKeyType ReturnKeyType { get; set; }

		public bool EnablesReturnKeyAutomatically { get; set; }

		public bool SecureTextEntry { get; set; }

		#endregion

		public UITextFieldViewMode ClearButtonMode { get; set; }

		public UITextBorderStyle BorderStyle { get; set; }

		public UITextFieldViewMode LeftViewMode { get; set; }

		public string Placeholder { get; set; }

		public UITextFieldViewMode RightViewMode { get; set; }

		public UITextSpellCheckingType SpellCheckingType { get; set; }

		public bool Enabled { get; set; }

		public bool Highlighted { get; set; }

		public UIControlContentHorizontalAlignment HorizontalAlignment { get; set; }
		
		public bool Selected { get; set; }

		public UIControlContentVerticalAlignment VerticalAlignment { get; set; }

		public UIViewContentMode ContentMode { get; set; }

		public void CopyAllValuesFrom(UITextField input)
		{
			AutocapitalizationType = input.AutocapitalizationType;
			AutocorrectionType = input.AutocorrectionType;
			EnablesReturnKeyAutomatically = input.EnablesReturnKeyAutomatically;
			KeyboardAppearance = input.KeyboardAppearance;
			KeyboardType = input.KeyboardType;
			ReturnKeyType = input.ReturnKeyType;
			SecureTextEntry = input.SecureTextEntry;
			ClearButtonMode = input.ClearButtonMode;
			BorderStyle = input.BorderStyle;
			LeftViewMode = input.LeftViewMode;
			Placeholder = input.Placeholder;
			RightViewMode = input.RightViewMode;
			SpellCheckingType = input.SpellCheckingType;
			Enabled = input.Enabled;
			Highlighted = input.Highlighted;
			HorizontalAlignment = input.HorizontalAlignment;
			Selected = input.Selected;
			VerticalAlignment = input.VerticalAlignment;
			ContentMode = input.ContentMode;
		}

		public void CopyAllValuesTo(UITextField output)
		{
			output.AutocapitalizationType = AutocapitalizationType;
			output.AutocorrectionType = AutocorrectionType;
			output.EnablesReturnKeyAutomatically = EnablesReturnKeyAutomatically;
			output.KeyboardAppearance = KeyboardAppearance;
			output.KeyboardType = KeyboardType;
			output.ReturnKeyType = ReturnKeyType;
			output.SecureTextEntry = SecureTextEntry;
			output.ClearButtonMode = ClearButtonMode;
			output.BorderStyle = BorderStyle;
			output.LeftViewMode = LeftViewMode;
			output.Placeholder = Placeholder;
			output.RightViewMode = RightViewMode;
			output.SpellCheckingType = SpellCheckingType;
			output.Enabled = Enabled;
			output.Highlighted = Highlighted;
			output.HorizontalAlignment = HorizontalAlignment;
			output.Selected = Selected;
			output.VerticalAlignment = VerticalAlignment;
			output.ContentMode = ContentMode;
		}

	}
}
