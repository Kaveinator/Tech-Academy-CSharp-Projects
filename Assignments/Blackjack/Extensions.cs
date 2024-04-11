using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack {

    public static class Extensions {
        public static void Do<T>(this IEnumerable<T> source, Action<T> action) {   // note: omitted arg/null checks
            foreach (T item in source) { action(item); }
        }
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {   // note: omitted arg/null checks
            foreach (T item in source) { action(item); }
        }

        public static IEnumerable<T> GetValues<T>() where T : Enum
            => Enum.GetValues(typeof(T)).Cast<T>();

        public static string[] GetNames<T>() where T : Enum
            => Enum.GetNames(typeof(T));
    }
}
