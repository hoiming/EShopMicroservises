using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record ProductId
    {
        public Guid Value { get; }


        private ProductId(Guid value)   => Value = value;   

        public static ProductId Of(Guid dbId)
        {
            ArgumentNullException.ThrowIfNull(dbId, nameof(dbId));

            if(dbId == Guid.Empty)
            {
                throw new DomainException("ProductId cannot be null");
            }

            return new ProductId(dbId);
        }
    }
}
