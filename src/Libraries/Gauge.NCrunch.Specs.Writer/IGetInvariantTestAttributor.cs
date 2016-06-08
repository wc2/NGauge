using System;

namespace Gauge.NCrunch.Specs.Writer
{
    public interface IGetInvariantTestAttributor
    {
        Type GetAttribute();
    }
}
