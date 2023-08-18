using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ASP.NETCoreBlazor.Components
{
    public class ModelAttributeGetHelper : ComponentBase
    {
        protected string GetDisplayName(object model, string propertyName)
        {
            var propertyInfo = model.GetType().GetProperty(propertyName);
            var displayAttribute = propertyInfo?.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? propertyName;
        }

        protected string GetRequiredErrorMessage(object model, string propertyName)
        {
            var propertyInfo = model.GetType().GetProperty(propertyName);
            var displayAttribute = propertyInfo?.GetCustomAttribute<RequiredAttribute>();
            return displayAttribute?.ErrorMessage ?? propertyName;
        }
    }
}
