using System.Collections.Generic;

namespace Hom4_part2
{
    public interface IValidator
    {
        void Validate(object value, object container);
    }
}
