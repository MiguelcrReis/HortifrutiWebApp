using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HortifrutiWebApp.Models.Enums
{
    public static class EnumExtensions
    {
        public static string GetDisplayAttributeFrom(Enum enumValue, Type enumType)
        {
            MemberInfo info = enumType.GetMember(enumValue.ToString()).First();

            string displayName;
            if (info != null && info.CustomAttributes.Any())
            {
                DisplayAttribute nameAttr = info.GetCustomAttribute<DisplayAttribute>();
                displayName = nameAttr != null ? nameAttr.Name : enumValue.ToString();
            }
            else
            {
                displayName = enumValue.ToString();
            }
            return displayName;
        }
    }
}
