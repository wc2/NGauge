using System;

namespace Gauge.NCrunch.Specs.Writer.NUnit
{
    public sealed class TestCaseAttributor : IGetInvariantTestAttributor
    {
        Type IGetInvariantTestAttributor.GetAttribute()
        {
            throw new NotImplementedException();
        }
    }
}
