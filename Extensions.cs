using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGTest
{
    public static class Extensions
    {
        public static void AddMultiple<T>(this List<T> list, T one, T two)
        {
            list.Add(one);
            list.Add(two);
        }

        public static void AddMultiple<T>(this List<T> list, T one, T two, T three)
        {
            list.Add(one);
            list.Add(two);
            list.Add(three);
        }

        public static void AddMultiple<T>(this List<T> list, T one, T two, T three, T four)
        {
            list.Add(one);
            list.Add(two);
            list.Add(three);
            list.Add(four);
        }
    }
}
