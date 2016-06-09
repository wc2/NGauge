using System;
using System.Collections.Generic;
using System.Linq;

namespace NGauge.Specs.Reader.Factories
{
    internal static class Extensions
    {
        public static IEnumerable<TOut> SelectOrEmpty<TIn, TOut>(this IEnumerable<TIn> @in,
            Func<TIn, TOut> factory)
        {
            return @in?.Select(factory) ?? Enumerable.Empty<TOut>();
        }
    }
}
