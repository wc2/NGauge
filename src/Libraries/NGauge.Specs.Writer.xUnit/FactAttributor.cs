using System;
using Xunit;

namespace NGauge.Specs.Writer.xUnit
{
    public sealed class FactAttributor : IGetInvariantTestAttributor
    {
        Type IGetInvariantTestAttributor.GetAttribute()
        {
            return typeof(FactAttribute);
        }
    }
}
