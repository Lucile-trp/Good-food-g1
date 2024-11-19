using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Host.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var displayNameAttribute = value.GetType()
                .GetField(value.ToString())
                ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayNameAttribute?.Name ?? value.ToString();
        }
    }
}
