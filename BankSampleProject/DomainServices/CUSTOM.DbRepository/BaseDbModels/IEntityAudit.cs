using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOM.DbRepository.BaseDbModels
{
    public interface IEntityAudit
    {
        long CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        long UpdatedBy { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
