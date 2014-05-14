// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace TextFieldApp
{
	[Register ("StringPropertyCell")]
	partial class StringPropertyCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel PropertyNameLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel PropertyTypeLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField PropertyValueField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (PropertyNameLabel != null) {
				PropertyNameLabel.Dispose ();
				PropertyNameLabel = null;
			}

			if (PropertyTypeLabel != null) {
				PropertyTypeLabel.Dispose ();
				PropertyTypeLabel = null;
			}

			if (PropertyValueField != null) {
				PropertyValueField.Dispose ();
				PropertyValueField = null;
			}
		}
	}
}
