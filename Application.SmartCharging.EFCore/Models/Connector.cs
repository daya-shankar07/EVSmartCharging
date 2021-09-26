using System;
using System.Collections.Generic;

#nullable disable

namespace Application.SmartCharging.EFCore.Models
{
    public partial class Connector
    {
        public int Id { get; set; }
        public Guid CstationId { get; set; }
        public int? MaxCurrent { get; set; }

        public virtual Cstation Cstation { get; set; }
    }
}
