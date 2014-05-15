using System;
using MonoTouch.UIKit;

namespace TextFieldApp
{
	public partial class TextFieldAppViewController : UIViewController
	{
		ClassPropertiesSource<TextFieldTraits> _source;
		readonly TextFieldTraits _textFieldTraits;

		static bool UserInterfaceIdiomIsPhone
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public TextFieldAppViewController (IntPtr handle) : base (handle)
		{
			_textFieldTraits = new TextFieldTraits ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_source = new ClassPropertiesSource<TextFieldTraits> (_textFieldTraits);
			TableView.Source = _source;
			_source.OnChange = HandleOnChange;

			_textFieldTraits.CopyAllValuesFrom (TextField);
			//TextField.EditingChanged += HandleEditingChanged;
			TextField.ShouldReturn = HandleShouldReturn;
		}

		void HandleEditingChanged (object sender, EventArgs e)
		{
			_textFieldTraits.CopyAllValuesFrom (TextField);
			TableView.ReloadData ();
		}

		void HandleOnChange ()
		{
			bool isFirstResponder = TextField.IsFirstResponder;

			if(isFirstResponder)
			{
				TextField.ResignFirstResponder ();
			}

			_textFieldTraits.CopyAllValuesTo (TextField);
			TableView.ReloadData ();

			if(isFirstResponder)
			{
				TextField.BecomeFirstResponder ();
			}
		}

		bool HandleShouldReturn (UITextField textField)
		{
			textField.ResignFirstResponder ();
			_textFieldTraits.CopyAllValuesFrom (TextField);
			TableView.ReloadData ();
			return true;
		}

		protected override void Dispose (bool disposing)
		{
			if(disposing)
			{
				TextField.EditingChanged -= HandleEditingChanged;
			}
			base.Dispose (disposing);
		}
	}
}

