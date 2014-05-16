using System.Reflection;

namespace TextFieldApp
{
	public interface IPropertyCell
	{
		void Update (PropertyInfo propertyInfo, object instance);
	}
	
}
