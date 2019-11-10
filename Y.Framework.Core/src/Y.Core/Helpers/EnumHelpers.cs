using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel;

namespace Y.Core
{
    public static class EnumHelpers
    {

        public static IEnumerable<T> GetAllEnumValues<T>()
        {
            foreach (var value in Enum.GetValues(typeof(T)).Cast<T>())
            {
                yield return value;
            }
        }

       
        public static T GetEnumValue<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str, true);
        }
    }
}
