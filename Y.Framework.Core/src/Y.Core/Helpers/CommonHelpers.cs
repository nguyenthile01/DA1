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
using System.Security.Cryptography;

namespace Y.Core
{
    public static class CommonHelpers
    {

        private static readonly string[] VietnameseSigns = new[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        public static string RemoveVietnameseSign(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }
        public static string ToSlug(this string str)
        {
            return RemoveVietnameseSign(str).RemoveSpecialCharacters();
        }

        public static string RemoveSpecialCharacters(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 500 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }
        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static string ToCleanVietnameseSign(this string str)
        {
            return RemoveVietnameseSign(str);
        }
        public static bool IsNotNullOrEmpty(this string model)
        {
            return !string.IsNullOrEmpty(model);
        }

        public static bool IsNotNull<T>(this T model)
        {
            return model != null;
        }
        //public static bool IsNullOrEmpty(this string model)
        //{
        //    return string.IsNullOrEmpty(model);
        //}
        public static string ToLowerFirstCharacter(this string model)
        {
            if (model.IsNullOrEmpty())
                return "";
            return Char.ToLowerInvariant(model[0]) + model.Substring(1);
        }

        public static string ToDateString(this DateTime? model, string format = "MMM/dd/yyyy")
        {
            return model == null ? "" : model.Value.ToString(format);
        }
        public static string ToDateString(this DateTime model)
        {
            return model == null ? "" : string.Format("{0:dd/MMM/yyyy}", model);
        }

        public static string ToBase64(this string model)
        {
            if (model.IsNullOrEmpty())
                return null;
            byte[] encodedBytes = System.Text.Encoding.Unicode.GetBytes(model);
            return Convert.ToBase64String(encodedBytes);
        }

        public static string FromBase64(this string model)
        {
            if (model.IsNullOrEmpty())
                return null;
            return Encoding.Unicode.GetString(Convert.FromBase64String(model));
        }

        public static DateTime? ToExact(this string model, string format)
        {
            if (model.IsNullOrEmpty())
                return null;
            DateTime.TryParseExact(model, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out var result);
            if (result == DateTime.MinValue)
                return null;
            return result;
        }

        public static List<int> ToNumbers(this string model, string splitter = ",")
        {
            if (model.IsNullOrEmpty())
                return null;
            return model.Split(splitter, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
        }

        public static bool HasData(this IEnumerable<object> list)
        {
            return (list != null && list.Any());
        }

        public static DateTime ToDateTime(this double unixTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds((long)unixTime)
                .DateTime.ToLocalTime();
        }

        public static bool HasData<T>(this IEnumerable<T> list)
        {
            return (list != null && list.Any());
        }

        public static List<string> GetAllPublicConstantValues(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Select(x => (string)x.GetRawConstantValue())
                .ToList();
        }
        public static string ToHumanString(this string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(
                input,
                "([^^])([A-Z])",
                "$1 $2"
            );
        }

        public static string Join(this IEnumerable<string> model, string separator)
        {
            if (model == null)
            {
                return "";
            }

            return string.Join(separator, model);
        }

        public static DateTime GetPreviousWeekDay(this DateTime currentDate, DayOfWeek dow, int weekCount = 0)
        {
            int currentDay = (int)currentDate.DayOfWeek, gotoDay = (int)dow;
            return currentDate.AddDays(-7).AddDays(gotoDay - currentDay).AddDays(-7 * weekCount);
        }

        public static DateTime GetNextWeekDay(this DateTime currentDate, DayOfWeek dow, int weekCount = 0)
        {
            int currentDay = (int)currentDate.DayOfWeek, gotoDay = (int)dow;
            return currentDate.AddDays(7).AddDays(gotoDay - currentDay).AddDays(7 * weekCount);
        }

        public static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomNumberString(int length)
        {
            var random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Hmac256(this string message, string key)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(key);

            HMACSHA256 hmacsha1 = new HMACSHA256(keyByte);

            byte[] messageBytes = encoding.GetBytes(message);

            byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);
            return ByteToString(hashmessage).ToLower();
        }

        public static string Sha256(this string message)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(message);

            SHA256 sha256 = SHA256.Create();

            byte[] hashmessage = sha256.ComputeHash(keyByte);
            return ByteToString(hashmessage).ToLower();
        }
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }
    }

    public static class IQueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");

        private static readonly FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");

        private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        }
        public static string GetEnumDescription<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            GeneralPropertyComparer<T, TKey> comparer = new GeneralPropertyComparer<T, TKey>(property);
            return items.Distinct(comparer);
        }
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> sequence, int subsetSize)
        {
            return TakeRandom(sequence, subsetSize, new Random());
        }
      

        /// <summary>
        /// Returns a sequence of a specified size of random elements from the original sequence
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence</typeparam>
        /// <param name="sequence">The sequence from which to return random elements</param>
        /// <param name="subsetSize">The size of the random subset to return</param>
        /// <param name="rand">A random generator used as part of the selection algorithm</param>
        /// <returns>A random sequence of elements in random order from the original sequence</returns>

        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> sequence, int subsetSize, Random rand)
        {
            if (rand == null) throw new ArgumentNullException("rand");
            if (sequence == null) throw new ArgumentNullException("sequence");
            if (subsetSize < 0) throw new ArgumentOutOfRangeException("subsetSize");

            return TakeRandomImpl(sequence, subsetSize, rand);
        }

        private static IEnumerable<T> TakeRandomImpl<T>(IEnumerable<T> sequence, int subsetSize, Random rand)
        {
            // The simplest and most efficient way to return a random subet is to perform 
            // an in-place, partial Fisher-Yates shuffle of the sequence. While we could do 
            // a full shuffle, it would be wasteful in the cases where subsetSize is shorter
            // than the length of the sequence.
            // See: http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle

            var seqArray = sequence.ToArray();
            if (seqArray.Length < subsetSize)
                subsetSize = seqArray.Length;

            var m = 0;                // keeps track of count items shuffled
            var w = seqArray.Length;  // upper bound of shrinking swap range
            var g = w - 1;            // used to compute the second swap index

            // perform in-place, partial Fisher-Yates shuffle
            while (m < subsetSize)
            {
                var k = g - rand.Next(w);
                var tmp = seqArray[k];
                seqArray[k] = seqArray[m];
                seqArray[m] = tmp;
                ++m;
                --w;
            }

            // yield the random subet as a new sequence
            for (var i = 0; i < subsetSize; i++)
                yield return seqArray[i];
        }

    }

    public class GeneralPropertyComparer<T, TKey> : IEqualityComparer<T>
    {
        private Func<T, TKey> expr { get; set; }
        public GeneralPropertyComparer(Func<T, TKey> expr)
        {
            this.expr = expr;
        }
        public bool Equals(T left, T right)
        {
            var leftProp = expr.Invoke(left);
            var rightProp = expr.Invoke(right);
            if (leftProp == null && rightProp == null)
                return true;
            else if (leftProp == null ^ rightProp == null)
                return false;
            else
                return leftProp.Equals(rightProp);
        }
        public int GetHashCode(T obj)
        {
            var prop = expr.Invoke(obj);
            return (prop == null) ? 0 : prop.GetHashCode();
        }
       

    }
}
