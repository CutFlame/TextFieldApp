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
	}
}
