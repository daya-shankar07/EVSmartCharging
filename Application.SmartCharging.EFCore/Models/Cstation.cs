using System;
using System.Collections.Generic;

#nullable disable

namespace Application.SmartCharging.EFCore.Models
{
    public partial class Cstation
    {
        public Cstation()
        {
            Connectors = new HashSet<Connector>();
        }

        public Guid StationId { get; set; }
        public Guid GroupId { get; set; }
        public string Name { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Connector> Connectors { get; set; }
    }
}
