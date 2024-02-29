using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MaskedLog
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaskSensitiveDataAttribute : Attribute
    {
        public int FirstVisibleCharacters { get; }
        public int LastVisibleCharacters { get; }

        public MaskSensitiveDataAttribute(int firstVisibleCharacters, int lastVisibleCharacters)
        {
            FirstVisibleCharacters = firstVisibleCharacters;
            LastVisibleCharacters = lastVisibleCharacters;
        }
    }

    public static class SensitiveDataMasking
    {
        public static class Logger<T>
        {
            public static string MaskSensitiveData(T logProperties)
            {
                StringBuilder maskedLog = new StringBuilder();

                foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var maskAttr = prop.GetCustomAttributes(typeof(MaskSensitiveDataAttribute), true).FirstOrDefault() as MaskSensitiveDataAttribute;
                    if (maskAttr != null)
                    {
                        string data = prop.GetValue(logProperties)?.ToString() ?? "";
                        maskedLog.Append(MaskSensitiveData(data, maskAttr.FirstVisibleCharacters, maskAttr.LastVisibleCharacters));
                    }
                    else
                    {
                        maskedLog.Append(prop.GetValue(logProperties)?.ToString());
                    }
                    maskedLog.Append(" ");
                }

                return maskedLog.ToString().Trim();
            }

            private static string MaskSensitiveData(string data, int firstVisibleCharacters, int lastVisibleCharacters)
            {
                if (string.IsNullOrEmpty(data))
                    return data;

                int length = data.Length;

                if (length <= firstVisibleCharacters + lastVisibleCharacters)
                    return new string('*', length);

                if (firstVisibleCharacters == 0)
                    return new string('*', length - lastVisibleCharacters) + data.Substring(length - lastVisibleCharacters);

                if (lastVisibleCharacters == 0)
                    return data.Substring(0, firstVisibleCharacters) + new string('*', length - firstVisibleCharacters);

                return data.Substring(0, firstVisibleCharacters) +
                       new string('*', length - firstVisibleCharacters - lastVisibleCharacters) +
                       data.Substring(length - lastVisibleCharacters);
            }
        }
    }
}


