using System;

namespace Licenta.Commons.AI
{
    internal class ParameterAttribute : Attribute
    {
        public double DefaultValue { get; }
        public ParameterAttribute(double defaultValue = 0)
        {
            DefaultValue = defaultValue;
        }
    }
}
