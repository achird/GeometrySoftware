using System;
using System.Collections.Generic;
using System.Text;

namespace geometry.Core.SharedKernel.Domain
{
    /// <summary>
    /// Сущность домена
    /// </summary>
    public interface IEntity
    {
        long Id { get; }
    }
}
