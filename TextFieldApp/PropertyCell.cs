using System;
using System.Reflection;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;

namespace TextFieldApp
{
	public abstract class PropertyCell<T> : UITableViewCell, IPropertyCell
	{
		protected PropertyCell(IntPtr handle) : base(handle)
		{
		}

		public void Update (PropertyInfo propertyInfo, object instance)
		{
			UpdateNameAndTypeLabels (propertyInfo, propertyInfo.GetMethod == null || propertyInfo.SetMethod == null);

			if (!HasGetMethod (propertyInfo, instance as NSObject))
			{
				UpdateLabelsForError ();
				return;
			}

			T value;
			if (TryGetValue (propertyInfo, instance, out value))
			{
				UpdateLabelsForValue (value);
			}
			else
			{
				UpdateLabelsForError ();
			}

		}

		bool HasGetMethod (PropertyInfo propertyInfo, NSObject instance)
		{
			if(instance == null)
			{
				return true;
			}
			var selector = CreateSelector (propertyInfo.Name);
			if(instance.RespondsToSelector (selector))
			{
				return true;
			}
			if(propertyInfo.GetMethod == null || propertyInfo.GetGetMethod (true) == null)
			{
				return false;
			}

			return true;
			//return propertyInfo.GetMethod == null || propertyInfo.GetGetMethod (true) == null;
		}

		Selector CreateSelector (string name)
		{
			var newFirstLetter = char.ToLower (name [0]);
			string selectorName = newFirstLetter + name.Substring (1);
			return new Selector (selectorName);
		}

		static bool TryGetValue (PropertyInfo propertyInfo, object instance, out T value)
		{
			bool success = true;
			value = default(T);
			try
			{
				object obj = propertyInfo.GetValue (instance);
				if (obj != null)
				{
					value = (T)obj;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine (ex);
				success = false;
			}
			return success;
		}

		protected abstract void UpdateNameAndTypeLabels (PropertyInfo propertyInfo, bool readOnly);

		protected abstract void UpdateLabelsForError ();

		protected abstract void UpdateLabelsForValue (T value);

	}
}
