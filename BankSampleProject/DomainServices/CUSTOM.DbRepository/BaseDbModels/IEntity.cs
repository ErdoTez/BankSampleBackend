using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.DbRepository.BaseDbModels
{
    public interface IEntity<TKey> : IEntityAudit
    {
        TKey Id { get; set; }
    }
}
