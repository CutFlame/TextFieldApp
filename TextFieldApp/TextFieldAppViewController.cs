using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;

namespace TextFieldApp
{
	public partial class TextFieldAppViewController : UIViewController
	{
		ClassPropertiesSource<UITextField> _source;

		TextFieldTraits _textFieldTraits;

		static bool UserInterfaceIdiomIsPhone
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public TextFieldAppViewController (IntPtr handle) : base (handle)
		{
			_textFieldTraits = new TextFieldTraits ();
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_source = new ClassPropertiesSource<UITextField> (TextField);
			_source.OnChange = HandleOnChange;
			TableView.Source = _source;

			TextField.EditingChanged += HandleEditingChanged;
			//ApplyChangesToTextField ();
		}

		void HandleEditingChanged (object sender, EventArgs e)
		{
			TableView.ReloadData ();
		}

		void HandleOnChange ()
		{
			bool isFirstResponder = TextField.IsFirstResponder;

			if(isFirstResponder)
			{
				TextField.ResignFirstResponder ();
			}

			//ApplyChangesToTextField ();
			TableView.ReloadData ();

			if(isFirstResponder)
			{
				TextField.BecomeFirstResponder ();
			}
		}

		void ApplyChangesToTextField()
		{
			TextField.AutocapitalizationType = _textFieldTraits.AutocapitalizationType;
			TextField.AutocorrectionType = _textFieldTraits.AutocorrectionType;
			TextField.EnablesReturnKeyAutomatically = _textFieldTraits.EnablesReturnKeyAutomatically;
			TextField.KeyboardAppearance = _textFieldTraits.KeyboardAppearance;
			TextField.KeyboardType = _textFieldTraits.KeyboardType;
			TextField.ReturnKeyType = _textFieldTraits.ReturnKeyType;
			TextField.SecureTextEntry = _textFieldTraits.SecureTextEntry;
			TextField.ClearButtonMode = _textFieldTraits.ClearButtonMode;
			TextField.BorderStyle = _textFieldTraits.BorderStyle;
			TextField.LeftViewMode = _textFieldTraits.LeftViewMode;
			TextField.Placeholder = _textFieldTraits.Placeholder;
			TextField.RightViewMode = _textFieldTraits.RightViewMode;
			TextField.SpellCheckingType = _textFieldTraits.SpellCheckingType;
			TextField.Enabled = _textFieldTraits.Enabled;
			TextField.Highlighted = _textFieldTraits.Highlighted;
			TextField.HorizontalAlignment = _textFieldTraits.HorizontalAlignment;
			TextField.Selected = _textFieldTraits.Selected;
			TextField.VerticalAlignment = _textFieldTraits.VerticalAlignment;
			TextField.ContentMode = _textFieldTraits.ContentMode;
		}

		#endregion
	}
}

