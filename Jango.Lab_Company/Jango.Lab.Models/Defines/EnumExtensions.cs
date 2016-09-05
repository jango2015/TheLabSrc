using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jango.Lab.Models
{
    public static class Enums
    {
        public static Dictionary<string, int> EnumToDic(this Type enumType)
        {
            if (null != enumType)
            {
                var dic = new Dictionary<string, int>();
                foreach (var item in Enum.GetValues(enumType))
                {
                    dic.Add(Enum.GetName(enumType, item), (int)item);
                }
                return dic;
            }
            return null;
        }
    }
}
