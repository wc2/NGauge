using System;
using Xunit;

namespace Gauge.NCrunch.Specs.Writer.xUnit
{
    public sealed class FactAttributor : IGetInvariantTestAttributor
    {
        Type IGetInvariantTestAttributor.GetAttribute()
        {
            return typeof(FactAttribute);
        }
    }
}
