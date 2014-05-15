using System.Reflection;

namespace TextFieldApp
{
	public interface IPropertyCell
	{
		void UpdatePropertyInfo (PropertyInfo propertyInfo, object instance);
	}
	
}
