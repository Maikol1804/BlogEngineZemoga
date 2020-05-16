using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace BlogEngine.Transverse.Enumerator
{
    public static class BasicEnums
    {

        public enum State
        {
            [Description("Ok")]
            Ok = 1,
            [Description("Error")]
            Error = 2
        }

        public enum RolTypes
        {
            [Description("Writer")]
            Writer = 1,
            [Description("Editor")]
            Editor = 2
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        if (memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }
            return null;
        }
    }
}
