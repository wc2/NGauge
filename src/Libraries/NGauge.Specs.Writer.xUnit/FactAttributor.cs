using System;
using Xunit;

namespace NGauge.Specs.Writer.xUnit
{
    internal sealed class FactAttributor : IGetInvariantTestAttributor
    {
        Type IGetInvariantTestAttributor.GetAttribute()
        {
            return typeof(FactAttribute);
        }
    }
}
