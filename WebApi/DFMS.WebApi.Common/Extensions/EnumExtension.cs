using KrzaqTools.Extensions;
using System;

namespace DFMS.Shared.Extensions
{
    public static class EnumExtension
    {
        public static string ToCamelCase(this Enum @enum) => @enum.ToString().ToCamelCase();
    }
}
